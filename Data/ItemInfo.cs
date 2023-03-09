using SkiaSharp;
using UT_GraphicsDemo.Impl;

public class ItemInfo
{

    public ItemInfo(RectBoundaries boundaries, SKColor color)
    {
        Boundaries = boundaries;
        Color = color;
    }

    public RectBoundaries Boundaries { get; set; }
    public SKColor Color { get; set; }
}