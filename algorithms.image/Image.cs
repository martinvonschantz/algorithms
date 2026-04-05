namespace algorithms.image;

public readonly struct Rgba32
{
    public readonly byte R;
    public readonly byte G;
    public readonly byte B;
    public readonly byte A;

    public Rgba32(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}

public class Image
{
    private readonly Int32 _width;
    private readonly Int32 _height;
    internal readonly byte[] Data;

    public Int32 Width => _width;
    public Int32 Height => _height;
    public ReadOnlySpan<byte> Pixels => Data;

    public Image(Int32 width, Int32 height)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height));

        _width = width;
        _height = height;
        Data = new byte[width * height * 4];
    }

    public Image(Int32 width, Int32 height, byte[] rgbaBytes, Boolean copy = true)
    {
        if (width <= 0)
            throw new ArgumentOutOfRangeException(nameof(width));
        if (height <= 0)
            throw new ArgumentOutOfRangeException(nameof(height));
        if (rgbaBytes is null)
            throw new ArgumentNullException(nameof(rgbaBytes));
        if (rgbaBytes.Length != width * height * 4)
            throw new ArgumentException("RGBA buffer size does not match width * height * 4.", nameof(rgbaBytes));

        _width = width;
        _height = height;
        Data = copy ? rgbaBytes.ToArray() : rgbaBytes;
    }

    public Rgba32 GetPixel(Int32 x, Int32 y)
    {
        ValidateCoordinates(x, y);
        return ImagePixelAccess.GetPixel(Data, _width, x, y);
    }

    public void SetPixel(Int32 x, Int32 y, Rgba32 color)
    {
        ValidateCoordinates(x, y);
        ImagePixelAccess.SetPixel(Data, _width, x, y, color);
    }

    public Image Clone()
    {
        return new Image(_width, _height, Data, copy: true);
    }

    private void ValidateCoordinates(Int32 x, Int32 y)
    {
        if ((UInt32)x >= (UInt32)_width)
            throw new ArgumentOutOfRangeException(nameof(x));
        if ((UInt32)y >= (UInt32)_height)
            throw new ArgumentOutOfRangeException(nameof(y));
    }
}
