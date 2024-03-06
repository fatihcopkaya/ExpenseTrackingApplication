using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using System;
using System.IO;
using SixLabors.ImageSharp.Drawing.Processing;

namespace MasrafTakipYonetim.Infrastructure.Helpers
{
    public class CaptchaHelper
    {
        private const int CaptchaLength = 6;

        public (string captchaValue, string captchaImageBase64) GenerateCaptcha()
        {
            var captchaText = GenerateRandomText(CaptchaLength);
            var captchaImage = GenerateImage(captchaText);
            var base64Image = Convert.ToBase64String(ImageToByteArray(captchaImage));

            return (captchaText, base64Image);
        }

        public bool VerifyCaptcha(string storedCaptcha, string enteredCaptcha)
        {
            return storedCaptcha.Equals(enteredCaptcha, StringComparison.OrdinalIgnoreCase);
        }

        private string GenerateRandomText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var captchaChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                captchaChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(captchaChars);
        }

        private Image<Rgba32> GenerateImage(string text)
        {
            var image = new Image<Rgba32>(200, 50);
            image.Mutate(x => x.BackgroundColor(Color.White)); // Rgba32.White değil, Color.White olarak kullanıldı.

            var font = SystemFonts.CreateFont("Arial", 20); // Arial fontunu manuel olarak belirliyoruz

            image.Mutate(x => x.DrawText(null, text, font, Color.Black, new PointF(10, 10)));

            return image;
        }

        private byte[] ImageToByteArray(Image<Rgba32> image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.SaveAsPng(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}



