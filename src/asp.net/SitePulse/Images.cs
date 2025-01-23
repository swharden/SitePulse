namespace SitePulse;

public static class Images
{
    public static readonly byte[] MagentaPixel = Pixel(System.Drawing.Color.Magenta);

    public static byte[] Random(int width = 100, int height = 50)
    {
        RasterSharp.Image img = new(width, height);

        img.Fill(RasterSharp.Colors.Blue);

        img.DrawLine(
            x1: System.Random.Shared.Next(img.Width),
            y1: System.Random.Shared.Next(img.Height),
            x2: System.Random.Shared.Next(img.Width),
            y2: System.Random.Shared.Next(img.Height),
            color: RasterSharp.Colors.Yellow);

        return img.GetBitmapBytes();
    }

    public static byte[] Pixel(System.Drawing.Color color)
    {
        RasterSharp.Image img = new(1, 1);
        img.Fill(RasterSharp.Color.From(color));
        return img.GetBitmapBytes();
    }
}
