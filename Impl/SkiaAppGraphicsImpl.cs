using SkiaSharp;
using UT_GraphicsDemo.Impl;

class SkiaAppGraphicsImpl
{
    public void ApplyIcon(string iconName, SKColor color, SKCanvas canvas, Coordinate origin)
    {
        LoadImage("wwwroot/Images/Icon" + iconName + ".png", canvas, origin);
    }
    public void ApplyIcon(string iconName, SKColor color, SKCanvas canvas, Coordinate origin, Size newSize)
    {
        LoadImage("wwwroot/Images/Icon" + iconName + ".png", canvas, newSize, origin);
    }

    public void LoadImage(string imageUrl, SKCanvas canvas)
    {
        LoadImage(imageUrl, canvas, new Coordinate { X = 0, Y = 0 });
    }

    public void LoadImage(string imageUrl, SKCanvas canvas, Coordinate origin)
    {
        SKImage image = SKImage.FromEncodedData(imageUrl);
        SKBitmap bitmap = SKBitmap.FromImage(image);
        canvas.DrawBitmap(bitmap, new SKPoint(origin.X, origin.Y));
    }

    public void LoadImage(string imageUrl, SKCanvas canvas, Size newSize)
    {
        LoadImage(imageUrl, canvas, new Coordinate { X = 0, Y = 0 });
    }

    public void LoadImage(string imageUrl, SKCanvas canvas, Size newSize, Coordinate origin)
    {
        SKImage image = SKImage.FromEncodedData(imageUrl);
        SKBitmap bitmap = SKBitmap.FromImage(image);
        var scaled = bitmap.Resize(new SKImageInfo(newSize.Width, newSize.Height), SKFilterQuality.High);
        canvas.DrawBitmap(scaled, new SKPoint(origin.X, origin.Y));
    }

    /// Render Image.png image to given path
    public void RenderImage(SKImage image, string path)
    {
        RenderImage(image, path, "Image.png");
    }

    public void RenderImage(SKImage image, string path, string filename)
    {
        using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (MemoryStream mStream = new MemoryStream(data.ToArray()))
        {
            if (!Path.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            try
            {
                FileStream fs = File.Create(path + filename);
                var truc = new System.IO.BinaryWriter(fs);
                truc.Write(mStream.ToArray());
                Console.WriteLine("Attempting to write image...");
                truc.Close();
                fs.Close();
            }
            catch (System.IO.DirectoryNotFoundException dnfe)
            {
                System.Console.WriteLine("Directory " + path + " was not found.");
                System.Console.WriteLine(dnfe.Message);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("An exception occurred: \n\t" + e.Message);
            }
        }
    }

    public void SurroundZone(RectBoundaries bound, SKColor color, SKCanvas canvas)
    {
        using (SKPaint paint = new SKPaint())
        {
            paint.Color = color;
            paint.StrokeWidth = 3;
            paint.Style = SKPaintStyle.Stroke;
            paint.PathEffect = SKPathEffect.CreateDash(new float[] { 5.0f, 3.0f }, 0f);

            canvas.DrawRoundRect(
                new SKRoundRect(
                    new SKRect(bound.TopLeftCorner.X, bound.TopLeftCorner.Y,
                    bound.BottomRightCorner.X, bound.BottomRightCorner.Y),
                    8),
                paint);
        }
    }
}