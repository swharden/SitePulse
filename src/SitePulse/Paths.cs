namespace SitePulse;

public static class Paths
{
    public static readonly string AppFolder = GetAppFolder();

    private static string GetAppFolder()
    {
        string appFolder = "/app";

        if (!Directory.Exists(appFolder))
        {
            appFolder = Path.GetFullPath("./app");
        }

        if (!Directory.Exists(appFolder))
        {
            Directory.CreateDirectory(appFolder);
        }

        Console.WriteLine($"Application folder: {appFolder}");

        return appFolder;
    }
}
