namespace algorithms.image;

internal static class ImagePixelAccess
{
    internal static Rgba32 GetPixel(byte[] data, Int32 width, Int32 x, Int32 y)
    {
        var index = ((y * width) + x) * 4;
        return new Rgba32(data[index], data[index + 1], data[index + 2], data[index + 3]);
    }

    internal static void SetPixel(byte[] data, Int32 width, Int32 x, Int32 y, Rgba32 color)
    {
        var index = ((y * width) + x) * 4;
        data[index] = color.R;
        data[index + 1] = color.G;
        data[index + 2] = color.B;
        data[index + 3] = color.A;
    }

    internal static Rgba32 GetPixelAtIndex(byte[] data, Int32 index)
    {
        var baseIndex = index * 4;
        return new Rgba32(data[baseIndex], data[baseIndex + 1], data[baseIndex + 2], data[baseIndex + 3]);
    }
}
