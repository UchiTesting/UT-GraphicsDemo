using SkiaSharp;

namespace UT_GraphicsDemo.Data
{
    public class SkiaSharpDemoService
    {
        SKSurface _surface;
        SKImageInfo _fullImageInfo;

        SKCanvas _canvas;

        private void InitSurface()
        {

            try
            {
                _fullImageInfo = new SKImageInfo(320, 240);
                _surface = SKSurface.Create(_fullImageInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InitSurfaceComposite()
        {

            try
            {
                _fullImageInfo = new SKImageInfo(640, 480);
                _surface = SKSurface.Create(_fullImageInfo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RenderImage(SKImage image)
        {

        }

        public async Task DrawSomething()
        {
            await Task.Run(() =>
            {
                InitSurface();

                try
                {
                    _canvas = _surface.Canvas;
                    _canvas.Clear(SKColors.PapayaWhip);

                    using (SKPaint paint = new SKPaint())
                    {
                        paint.Color = SKColors.Coral;
                        paint.StrokeWidth = 3;
                        paint.Style = SKPaintStyle.Stroke;
                        paint.PathEffect = SKPathEffect.CreateDash(new float[] { 5.0f, 3.0f }, 0f);
                        _canvas.DrawCircle(150, 150, 30, paint);
                        paint.Color = SKColors.Chartreuse;
                        _canvas.DrawRoundRect(
                            new SKRoundRect(
                                new SKRect(150, 30, 210, 100),
                                8),
                            paint);
                        paint.Color = SKColors.OrangeRed;
                        _canvas.DrawRect(new SKRect(5, 5, 100, 100), paint);

                        SKImage image = SKImage.FromEncodedData(@"wwwroot/Images/Icon.png");
                        SKBitmap bm = SKBitmap.FromImage(image);
                        var scaled = bm.Resize(new SKImageInfo(20, 20), SKFilterQuality.High);
                        _canvas.DrawBitmap(scaled, new SKPoint(60, 30));

                        SKSurface.Create(new SKImageInfo { Width = 20, Height = 20, ColorType = SKImageInfo.PlatformColorType, AlphaType = SKAlphaType.Premul });
                    }

                    var separator = Path.DirectorySeparatorChar;
                    var path = "wwwroot" + separator
                        + "Images" + separator
                        + "Output" + separator;

                    new SkiaAppGraphicsImpl().RenderImage(_surface.Snapshot(), path);
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }

        public async Task DrawSomethingRefactored()
        {
            await Task.Run(() =>
            {
                InitSurfaceComposite();

                try
                {
                    _canvas = _surface.Canvas;
                    _canvas.Clear(SKColors.PapayaWhip);

                    var graphics = new SkiaAppGraphicsImpl();

                    var separator = Path.DirectorySeparatorChar;
                    var path = "wwwroot" + separator
                        + "Images" + separator
                        + "Output" + separator;

                    graphics.LoadImage("wwwroot/Images/Base.png", _canvas);

                    // 155,24 275, 175
                    var person1TopLeftCoord = new Impl.Coordinate { X = 155, Y = 24 };
                    var person1BottomRightCoord = new Impl.Coordinate { X = 275, Y = 175 };

                    graphics.SurroundZone(new Impl.RectBoundaries { TopLeftCorner = person1TopLeftCoord, BottomRightCorner = person1BottomRightCoord }, SKColors.DarkSlateBlue, _canvas);

                    graphics.ApplyIcon(string.Empty, SKColors.White, _canvas,
                        new Impl.Coordinate { X = person1TopLeftCoord.X, Y = person1BottomRightCoord.Y + 10 },
                        new Impl.Size { Width = 20, Height = 20 });


                    graphics.RenderImage(_surface.Snapshot(), path, "Composite.png");
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }
    }
}
