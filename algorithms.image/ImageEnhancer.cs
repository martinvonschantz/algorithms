namespace algorithms.image;

public static class ImageEnhancer
{
    public static Image EnhanceColors(Image source, Single saturation = 1.2f, Single contrast = 1.1f)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (saturation <= 0)
            throw new ArgumentOutOfRangeException(nameof(saturation));
        if (contrast <= 0)
            throw new ArgumentOutOfRangeException(nameof(contrast));

        var result = new Image(source.Width, source.Height);
        var data = source.Data;
        var pixelCount = source.Width * source.Height;

        for (var i = 0; i < pixelCount; i++)
        {
            var index = i * 4;
            var r = data[index];
            var g = data[index + 1];
            var b = data[index + 2];
            var a = data[index + 3];

            var luma = (0.299f * r) + (0.587f * g) + (0.114f * b);
            var rSat = luma + (r - luma) * saturation;
            var gSat = luma + (g - luma) * saturation;
            var bSat = luma + (b - luma) * saturation;

            var rCon = (rSat - 128f) * contrast + 128f;
            var gCon = (gSat - 128f) * contrast + 128f;
            var bCon = (bSat - 128f) * contrast + 128f;

            result.Data[index] = ImageMath.ClampToByte(rCon);
            result.Data[index + 1] = ImageMath.ClampToByte(gCon);
            result.Data[index + 2] = ImageMath.ClampToByte(bCon);
            result.Data[index + 3] = a;
        }

        return result;
    }
}
