
using System.Runtime.InteropServices;

internal unsafe class Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    public enum BI
    {
        RGB = 0,
        RLE8 = 1,
        RLE4 = 2,
        BITFIELDS = 3,
        JPEG = 4,
        PNG = 5,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER
    {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public BI biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public RGBQUAD bmiColors;
    }
    public enum BOOL : int { FALSE = 0 }

    [DllImport("gdi32")]
    public static extern BOOL DeleteObject(IntPtr ho);
}
