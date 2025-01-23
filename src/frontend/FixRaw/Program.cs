System.Text.StringBuilder sb = new();
foreach (string filePath in Directory.GetFiles("../raw/", "*.txt"))
{
    sb.AppendLine(FixFile(filePath));
}
File.WriteAllText("out.txt", sb.ToString());

static string FixFile(string filePath)
{
    string[] lines = File.ReadAllLines(filePath);

    System.Text.StringBuilder sb = new();

    bool isZero = true;
    bool isRollover = false;
    DateTime lastDate = DateTime.MinValue;
    for (int i = 0; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] parts = line.Split(",");
        string timestampString = $"{parts[0]} {parts[1].Replace("-", ":")}";
        DateTime dt = DateTime.Parse(timestampString);

        if (isZero && dt.Hour != 12)
        {
            isZero = false;
        }

        if (isZero)
        {
            dt = dt.AddHours(-12);
        }

        if ((!isRollover) && (dt < lastDate))
        {
            isRollover = true;
        }

        if (isRollover)
        {
            dt = dt.AddHours(12);
        }

        lastDate = dt;

        string logLine = $"{dt:O},{parts[2]}";
        if (i % 100 == 0)
        {
            //Console.WriteLine(logLine);
        }

        sb.AppendLine(logLine);
    }

    return sb.ToString();
}