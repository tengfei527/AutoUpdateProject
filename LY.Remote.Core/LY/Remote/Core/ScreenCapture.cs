namespace LY.Remote.Core
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    public class ScreenCapture
    {
        private const int CURSOR_SHOWING = 1;
        public const int MinPixel = 0x690;

        public static MemoryStream Capture(long encodeValue)
        {
            Bitmap bitmap;
            MemoryStream stream3;
        Label_0037:
            bitmap = new Bitmap(GetPhysicalDisplaySize().Width, GetPhysicalDisplaySize().Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            Graphics graphics2 = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr hdc = graphics2.GetHdc();
            int num = WinAPI.BitBlt(graphics.GetHdc(), 0, 0, GetPhysicalDisplaySize().Width, GetPhysicalDisplaySize().Height, hdc, 0, 0, 0x40cc0020);
            graphics.ReleaseHdc();
            graphics2.ReleaseHdc();
            MemoryStream stream = new MemoryStream();
            bool flag = GetPhysicalDisplaySize().Width > 0x690;
            int num2 = 5;
        Label_0010:
            switch (num2)
            {
                case 0:
                    flag = encodeValue >= 100L;
                    num2 = 4;
                    goto Label_0010;

                case 1:
                case 7:
                    graphics.Dispose();
                    bitmap.Dispose();
                    stream.Seek(0L, SeekOrigin.Begin);
                    stream3 = stream;
                    num2 = 3;
                    goto Label_0010;

                case 2:
                    return stream3;

                case 3:
                    return stream3;

                case 4:
                    if (flag)
                    {
                        bitmap.Save(stream, ImageFormat.Jpeg);
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_0010;

                case 5:
                    if ((flag ? 0 : 1) != 0)
                    {
                    }
                    num2 = 0;
                    goto Label_0010;

                case 6:
                {
                    ImageCodecInfo info = GetEncoder(ImageFormat.Jpeg);
                    Encoder quality = Encoder.Quality;
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    EncoderParameter parameter = new EncoderParameter(quality, encodeValue);
                    encoderParams.Param[0] = parameter;
                    bitmap.Save(stream, info, encoderParams);
                    encoderParams.Dispose();
                    parameter.Dispose();
                    num2 = 7;
                    goto Label_0010;
                    bitmap.Save(stream, ImageFormat.Jpeg);
                    stream.Seek(0L, SeekOrigin.Begin);
                    MemoryStream stream2 = MakeThumbnail(stream, GetPhysicalDisplaySize().Width, GetPhysicalDisplaySize().Height, 0x690, encodeValue);
                    graphics.Dispose();
                    bitmap.Dispose();
                    stream3 = stream2;
                    num2 = 2;
                    goto Label_0010;
                }
            }
            goto Label_0037;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // This item is obfuscated and can not be translated.
        }

        public static Size GetPhysicalDisplaySize()
        {
            // This item is obfuscated and can not be translated.
        }

        public static MemoryStream MakeMaximum(MemoryStream stream, int originW, int originH, int toW)
        {
            // This item is obfuscated and can not be translated.
        }

        public static MemoryStream MakeThumbnail(MemoryStream stream, int originW, int originH, int toW, long encodeValue)
        {
            // This item is obfuscated and can not be translated.
        }
    }
}

