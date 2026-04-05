namespace algorithms.image;

public static class ImageEqualizer
{
    public static Image Equalize(Image source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        var histogram = new Int32[256];
        var pixelCount = source.Width * source.Height;
        var data = source.Data;

        for (var i = 0; i < pixelCount; i++)
        {
            var color = ImagePixelAccess.GetPixelAtIndex(data, i);
            var luma = ImageMath.GetLumaByte(color);
            histogram[luma]++;
        }

        var cdf = new Int32[256];
        var cumulative = 0;
        for (var i = 0; i < 256; i++)
        {
            cumulative += histogram[i];
            cdf[i] = cumulative;
        }

        var cdfMin = cdf.First(x => x > 0);
        var total = pixelCount;
        var result = new Image(source.Width, source.Height);

        for (var i = 0; i < pixelCount; i++)
        {
            var index = i * 4;
            var r = data[index];
            var g = data[index + 1];
            var b = data[index + 2];
            var a = data[index + 3];

            var luma = (Int32)MathF.Round(0.299f * r + 0.587f * g + 0.114f * b);
            var newLuma = (cdf[luma] - cdfMin) / (Single)(total - cdfMin) * 255f;
            var scale = luma > 0 ? newLuma / luma : 0f;

            result.Data[index] = ImageMath.ClampToByte(r * scale);
            result.Data[index + 1] = ImageMath.ClampToByte(g * scale);
            result.Data[index + 2] = ImageMath.ClampToByte(b * scale);
            result.Data[index + 3] = a;
        }

        return result;
    }
}
