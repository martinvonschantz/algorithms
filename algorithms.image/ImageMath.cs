namespace algorithms.image;

internal static class ImageMath
{
    internal static byte ClampToByte(Single value)
    {
        if (value <= 0)
            return 0;
        if (value >= 255)
            return 255;
        return (byte)MathF.Round(value);
    }

    internal static byte GetLumaByte(Rgba32 color)
    {
        var luma = 0.299f * color.R + 0.587f * color.G + 0.114f * color.B;
        return ClampToByte(luma);
    }
}
