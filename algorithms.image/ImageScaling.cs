namespace algorithms.image;

public static class ImageScaling
{
    public static Image Scale(Image source, Int32 newWidth, Int32 newHeight)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (newWidth <= 0)
            throw new ArgumentOutOfRangeException(nameof(newWidth));
        if (newHeight <= 0)
            throw new ArgumentOutOfRangeException(nameof(newHeight));

        var result = new Image(newWidth, newHeight);
        var srcWidth = source.Width;
        var srcHeight = source.Height;
        var srcData = source.Data;

        var xScale = (Single)srcWidth / newWidth;
        var yScale = (Single)srcHeight / newHeight;

        for (var y = 0; y < newHeight; y++)
        {
            var srcY = (y + 0.5f) * yScale - 0.5f;
            var y0 = (Int32)MathF.Floor(srcY);
            var y1 = y0 + 1;
            var yWeight = srcY - y0;
            y0 = Math.Clamp(y0, 0, srcHeight - 1);
            y1 = Math.Clamp(y1, 0, srcHeight - 1);

            for (var x = 0; x < newWidth; x++)
            {
                var srcX = (x + 0.5f) * xScale - 0.5f;
                var x0 = (Int32)MathF.Floor(srcX);
                var x1 = x0 + 1;
                var xWeight = srcX - x0;
                x0 = Math.Clamp(x0, 0, srcWidth - 1);
                x1 = Math.Clamp(x1, 0, srcWidth - 1);

                var c00 = ImagePixelAccess.GetPixel(srcData, srcWidth, x0, y0);
                var c10 = ImagePixelAccess.GetPixel(srcData, srcWidth, x1, y0);
                var c01 = ImagePixelAccess.GetPixel(srcData, srcWidth, x0, y1);
                var c11 = ImagePixelAccess.GetPixel(srcData, srcWidth, x1, y1);

                var r = Bilinear(c00.R, c10.R, c01.R, c11.R, xWeight, yWeight);
                var g = Bilinear(c00.G, c10.G, c01.G, c11.G, xWeight, yWeight);
                var b = Bilinear(c00.B, c10.B, c01.B, c11.B, xWeight, yWeight);
                var a = Bilinear(c00.A, c10.A, c01.A, c11.A, xWeight, yWeight);

                var dstIndex = ((y * newWidth) + x) * 4;
                result.Data[dstIndex] = r;
                result.Data[dstIndex + 1] = g;
                result.Data[dstIndex + 2] = b;
                result.Data[dstIndex + 3] = a;
            }
        }

        return result;
    }

    private static byte Bilinear(byte c00, byte c10, byte c01, byte c11, Single xWeight, Single yWeight)
    {
        var top = c00 + (c10 - c00) * xWeight;
        var bottom = c01 + (c11 - c01) * xWeight;
        return ImageMath.ClampToByte(top + (bottom - top) * yWeight);
    }
}
