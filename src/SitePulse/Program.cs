using SitePulse;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

app.MapGet("/", () => $"SitePulse, a privacy focused website analytics platform. {DateTime.UtcNow}");

app.MapGet("/test.bmp", () => Results.File(Images.Random(), "image/bmp"));

app.MapGet("/images/{filename}", (HttpContext context, string filename) =>
{
    string id = Path.GetFileNameWithoutExtension(filename);
    var now = DateTime.UtcNow;
    string logFilename = $"{now.Year}_{now.Month:00}_{now.Day:00}.log";
    string line = $"[{logFilename}] {DateTime.UtcNow:s},{context.Connection.RemoteIpAddress},{id}";
    string filePath = Path.Combine(Paths.AppFolder, logFilename);
    File.AppendAllLines(filePath, [line]);
    Console.WriteLine(line);
    return Results.File(Images.MagentaPixel, "image/bmp");
});


app.Run();