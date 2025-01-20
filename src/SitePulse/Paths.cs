using System.Diagnostics;

namespace SitePulse;

public static class Paths
{
    public static readonly string LogFolder = GetLogFolder();

    private static string GetLogFolder()
    {
        string logFolder = Path.GetFullPath("./logs");
        if (Debugger.IsAttached && !Directory.Exists(logFolder))
            Directory.CreateDirectory(logFolder);

        if (!Directory.Exists(logFolder))
            throw new DirectoryNotFoundException(logFolder);

        Console.WriteLine($"Log folder: {logFolder}");

        return logFolder;
    }
}
