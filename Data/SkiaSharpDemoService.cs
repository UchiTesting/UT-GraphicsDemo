using SkiaSharp;
using System.Linq;
using UT_GraphicsDemo.Impl;

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

                    #region Per item treatment

                    IList<ItemInfo> info = PrepareItemInfo().ToList();

                    foreach (var item in info)
                    {
                        ProcessItemOnImage(graphics, item);
                    }
                    #endregion 

                    graphics.RenderImage(_surface.Snapshot(), path, "Composite.png");
                }
                catch (Exception)
                {

                    throw;
                }
            });
        }

        // private void NewMethod(SkiaAppGraphicsImpl graphics, Coordinate person1TopLeftCoord, Coordinate person1BottomRightCoord)
        private void ProcessItemOnImage(SkiaAppGraphicsImpl graphics, ItemInfo itemInfo)
        {
            graphics.SurroundZone(new Impl.RectBoundaries { TopLeftCorner = itemInfo.Boundaries.TopLeftCorner, BottomRightCorner = itemInfo.Boundaries.BottomRightCorner }, itemInfo.Color, _canvas);

            graphics.ApplyIcon(string.Empty, itemInfo.Color, _canvas,
                new Impl.Coordinate { X = itemInfo.Boundaries.TopLeftCorner.X, Y = itemInfo.Boundaries.BottomRightCorner.Y + 10 },
                new Impl.Size { Width = 20, Height = 20 });
        }

        private IEnumerable<ItemInfo> PrepareItemInfo()
        {


            var p1tl = new Impl.Coordinate { X = 155, Y = 24 };
            var p1br = new Impl.Coordinate { X = 275, Y = 175 };
            var p2tl = new Impl.Coordinate { X = 30, Y = 156 };
            var p2br = new Impl.Coordinate { X = 155, Y = 300 };
            var p3tl = new Impl.Coordinate { X = 325, Y = 140 };
            var p3br = new Impl.Coordinate { X = 425, Y = 275 };
            var p4tl = new Impl.Coordinate { X = 495, Y = 100 };
            var p4br = new Impl.Coordinate { X = 620, Y = 250 };

            IList<Impl.RectBoundaries> peopleBoundaries = new List<Impl.RectBoundaries>();

            peopleBoundaries.Add(new Impl.RectBoundaries { TopLeftCorner = p1tl, BottomRightCorner = p1br });
            peopleBoundaries.Add(new Impl.RectBoundaries { TopLeftCorner = p2tl, BottomRightCorner = p2br });
            peopleBoundaries.Add(new Impl.RectBoundaries { TopLeftCorner = p3tl, BottomRightCorner = p3br });
            peopleBoundaries.Add(new Impl.RectBoundaries { TopLeftCorner = p4tl, BottomRightCorner = p4br });
            // 155,24 275, 175
            // 30,156 155,300
            // 325,140 424,275
            // 495,100 620,250
            IList<SKColor> respectiveColors = new List<SKColor>();

            respectiveColors.Add(SKColors.DarkCyan);
            respectiveColors.Add(SKColors.OrangeRed);
            respectiveColors.Add(SKColors.YellowGreen);
            respectiveColors.Add(SKColors.Purple);

            IList<ItemInfo> itemInfo = new List<ItemInfo>();

            for (int i = 0; i < peopleBoundaries.Count; i++)
            {
                itemInfo.Add(new ItemInfo(peopleBoundaries.ElementAt(i), respectiveColors.ElementAt(i)));
            }

            return itemInfo;
        }
    }
}
