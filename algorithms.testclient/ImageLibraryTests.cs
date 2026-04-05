using algorithms.image;
using NUnit.Framework;

namespace algorithms.testclient;

[TestFixture]
public class ImageLibraryTests
{
    [Test]
    public void Image_Constructor_InvalidDimensions_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Image(0, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Image(1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Image(-1, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Image(1, -1));
    }

    [Test]
    public void Image_SetGetPixel_RoundTrips()
    {
        var image = new Image(2, 2);
        var color = new Rgba32(10, 20, 30, 40);

        image.SetPixel(1, 0, color);

        var read = image.GetPixel(1, 0);

        Assert.That(read.R, Is.EqualTo(10));
        Assert.That(read.G, Is.EqualTo(20));
        Assert.That(read.B, Is.EqualTo(30));
        Assert.That(read.A, Is.EqualTo(40));
    }

    [Test]
    public void Image_Clone_CopiesBuffer()
    {
        var image = new Image(1, 1);
        image.SetPixel(0, 0, new Rgba32(1, 2, 3, 4));

        var clone = image.Clone();

        image.SetPixel(0, 0, new Rgba32(9, 8, 7, 6));
        var clonePixel = clone.GetPixel(0, 0);

        Assert.That(clonePixel.R, Is.EqualTo(1));
        Assert.That(clonePixel.G, Is.EqualTo(2));
        Assert.That(clonePixel.B, Is.EqualTo(3));
        Assert.That(clonePixel.A, Is.EqualTo(4));
    }

    [Test]
    public void ImageScaling_Scale_SameSize_PreservesPixels()
    {
        var image = new Image(2, 2);
        image.SetPixel(0, 0, new Rgba32(1, 2, 3, 4));
        image.SetPixel(1, 0, new Rgba32(5, 6, 7, 8));
        image.SetPixel(0, 1, new Rgba32(9, 10, 11, 12));
        image.SetPixel(1, 1, new Rgba32(13, 14, 15, 16));

        var scaled = ImageScaling.Scale(image, 2, 2);

        for (var y = 0; y < 2; y++)
        {
            for (var x = 0; x < 2; x++)
            {
                var original = image.GetPixel(x, y);
                var copy = scaled.GetPixel(x, y);
                Assert.That(copy.R, Is.EqualTo(original.R));
                Assert.That(copy.G, Is.EqualTo(original.G));
                Assert.That(copy.B, Is.EqualTo(original.B));
                Assert.That(copy.A, Is.EqualTo(original.A));
            }
        }
    }

    [Test]
    public void ImageScaling_ScaleTrilinear_Upscale_MatchesScale()
    {
        var image = new Image(2, 2);
        image.SetPixel(0, 0, new Rgba32(10, 10, 10, 255));
        image.SetPixel(1, 0, new Rgba32(20, 20, 20, 255));
        image.SetPixel(0, 1, new Rgba32(30, 30, 30, 255));
        image.SetPixel(1, 1, new Rgba32(40, 40, 40, 255));

        var scaled = ImageScaling.Scale(image, 4, 4);
        var trilinear = ImageScaling.ScaleTrilinear(image, 4, 4);

        Assert.That(trilinear.Pixels.ToArray(), Is.EqualTo(scaled.Pixels.ToArray()));
    }

    [Test]
    public void ImageColorLimiter_LimitColors_WithSingleColor_UsesFirstPixel()
    {
        var image = new Image(2, 1);
        image.SetPixel(0, 0, new Rgba32(100, 110, 120, 130));
        image.SetPixel(1, 0, new Rgba32(200, 210, 220, 230));

        var limited = ImageColorLimiter.LimitColors(image, 1);

        var p0 = limited.GetPixel(0, 0);
        var p1 = limited.GetPixel(1, 0);

        Assert.That(p0.R, Is.EqualTo(150));
        Assert.That(p0.G, Is.EqualTo(160));
        Assert.That(p0.B, Is.EqualTo(170));
        Assert.That(p0.A, Is.EqualTo(180));
        Assert.That(p1.R, Is.EqualTo(150));
        Assert.That(p1.G, Is.EqualTo(160));
        Assert.That(p1.B, Is.EqualTo(170));
        Assert.That(p1.A, Is.EqualTo(180));
    }
}
