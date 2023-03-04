using SkiaSharp;

namespace UT_GraphicsDemo.Data
{
    public class SkiaSharpDemoService
    {
        SKSurface _surface;

        private void InitSurface()
        {

            try
            {
                SKImageInfo _imageInfo = new SKImageInfo(320,240);
                _surface = SKSurface.Create(_imageInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RenderImage() {
            using (SKImage image = _surface.Snapshot())
            using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (MemoryStream mStream = new MemoryStream(data.ToArray()))
            {
                var separator = Path.DirectorySeparatorChar;
                FileStream fs = File.Create("wwwroot"+separator
                    +"Images"+separator
                    +"Test.png");
                var truc = new System.IO.BinaryWriter(fs);
                truc.Write(mStream.ToArray());
                truc.Close();
                fs.Close();
            }
        }

        public async Task DrawSomething()
        {
            await Task.Run(() =>
            {
                InitSurface();

                try
                {
                    SKCanvas canvas = _surface.Canvas;
                    canvas.Clear(SKColors.Chocolate);

                    using (SKPaint paint = new SKPaint())
                    {
                        paint.Color = SKColors.Coral;
                        paint.StrokeWidth = 15;
                        paint.Style = SKPaintStyle.Stroke;
                        canvas.DrawCircle(50,50,30,paint);
                        canvas.DrawRoundRect(
                            new SKRoundRect(
                                new SKRect(10,10,10,10),
                                10),
                            paint);
                    }

                    RenderImage();
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }
    }
}
