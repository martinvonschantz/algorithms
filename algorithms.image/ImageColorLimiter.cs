namespace algorithms.image;

public static class ImageColorLimiter
{
    public static Image LimitColors(Image source, Int32 colorCount, Int32 maxIterations = 10)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));
        if (colorCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(colorCount));
        if (maxIterations <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxIterations));

        var pixelCount = source.Width * source.Height;
        var k = Math.Min(colorCount, pixelCount);
        var data = source.Data;

        var centroids = InitializeCentroids(data, k, pixelCount);
        var assignments = new Int32[pixelCount];

        for (var iteration = 0; iteration < maxIterations; iteration++)
        {
            var sumR = new Int64[k];
            var sumG = new Int64[k];
            var sumB = new Int64[k];
            var sumA = new Int64[k];
            var counts = new Int32[k];
            var changed = false;

            for (var i = 0; i < pixelCount; i++)
            {
                var color = ImagePixelAccess.GetPixelAtIndex(data, i);
                var closest = FindClosestCentroid(color, centroids);
                if (assignments[i] != closest)
                {
                    assignments[i] = closest;
                    changed = true;
                }

                sumR[closest] += color.R;
                sumG[closest] += color.G;
                sumB[closest] += color.B;
                sumA[closest] += color.A;
                counts[closest]++;
            }

            for (var c = 0; c < k; c++)
            {
                if (counts[c] == 0)
                    continue;

                centroids[c] = new Rgba32(
                    (byte)(sumR[c] / counts[c]),
                    (byte)(sumG[c] / counts[c]),
                    (byte)(sumB[c] / counts[c]),
                    (byte)(sumA[c] / counts[c]));
            }

            if (!changed)
                break;
        }

        var result = new Image(source.Width, source.Height);
        for (var i = 0; i < pixelCount; i++)
        {
            var centroid = centroids[assignments[i]];
            var dstIndex = i * 4;
            result.Data[dstIndex] = centroid.R;
            result.Data[dstIndex + 1] = centroid.G;
            result.Data[dstIndex + 2] = centroid.B;
            result.Data[dstIndex + 3] = centroid.A;
        }

        return result;
    }

    private static Rgba32[] InitializeCentroids(byte[] data, Int32 k, Int32 pixelCount)
    {
        var centroids = new Rgba32[k];
        var step = Math.Max(1, pixelCount / k);
        for (var i = 0; i < k; i++)
        {
            var index = Math.Min(i * step, pixelCount - 1);
            centroids[i] = ImagePixelAccess.GetPixelAtIndex(data, index);
        }
        return centroids;
    }

    private static Int32 FindClosestCentroid(Rgba32 color, Rgba32[] centroids)
    {
        var bestIndex = 0;
        var bestDistance = Int64.MaxValue;
        for (var i = 0; i < centroids.Length; i++)
        {
            var c = centroids[i];
            var dr = color.R - c.R;
            var dg = color.G - c.G;
            var db = color.B - c.B;
            var da = color.A - c.A;
            var distance = (Int64)dr * dr + (Int64)dg * dg + (Int64)db * db + (Int64)da * da;
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestIndex = i;
            }
        }
        return bestIndex;
    }
}
