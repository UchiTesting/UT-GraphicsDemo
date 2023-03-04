﻿using SkiaSharp;

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
                    +"Output"+separator
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
                    canvas.Clear(SKColors.PapayaWhip);

                    using (SKPaint paint = new SKPaint())
                    {
                        paint.Color = SKColors.Coral;
                        paint.StrokeWidth = 3;
                        paint.Style = SKPaintStyle.Stroke;
                        paint.PathEffect = SKPathEffect.CreateDash(new float[] {5.0f,3.0f},0f);
                        canvas.DrawCircle(150,150,30,paint);
                        paint.Color = SKColors.Chartreuse;
                        canvas.DrawRoundRect(
                            new SKRoundRect(
                                new SKRect(150,30,210,100),
                                8),
                            paint);
                        paint.Color = SKColors.OrangeRed;
                        canvas.DrawRect(new SKRect(5,5,100,100),paint);
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
