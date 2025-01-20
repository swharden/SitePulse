using SitePulse;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();

app.MapGet("/", () => $"SitePulse, a privacy focused website analytics platform. [{DateTime.UtcNow}]");

app.MapGet("/test.bmp", () => Results.File(Images.Random(), "image/bmp"));

app.MapGet("/pulse/{filename}", (HttpContext context, string filename) =>
{
    if (Path.GetExtension(filename) != ".bmp")
        return Results.NotFound();

    string id = Path.GetFileNameWithoutExtension(filename);
    var now = DateTime.UtcNow;
    string logFilename = $"{now.Year}_{now.Month:00}_{now.Day:00}.log";
    string line = $"[{logFilename}] {DateTime.UtcNow:s},{context.Connection.RemoteIpAddress},{id}";
    string filePath = Path.Combine(Paths.LogFolder, logFilename);
    File.AppendAllLines(filePath, [line]);
    Console.WriteLine(line);
    return Results.File(Images.MagentaPixel, "image/bmp");
});

app.MapGet("/logFolder", () => Paths.LogFolder);

app.MapGet("/logFiles/", () => Directory.GetFiles(Paths.LogFolder, "*.log"));

app.MapGet("/logFile/{filename}", (string filename) =>
{
    string path = Path.Combine(Paths.LogFolder, Path.GetFileName(filename));
    return File.Exists(path)
        ? Results.Text(File.ReadAllText(path))
        : Results.NotFound();
});

app.Run();