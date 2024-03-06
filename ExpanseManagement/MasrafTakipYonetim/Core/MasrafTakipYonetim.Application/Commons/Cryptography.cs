using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MasrafTakipYonetim.Application.Commons
{
    public class Cryptography
    {
        private static byte[] key = Encoding.UTF8.GetBytes("J@NcRfUjXn2r5u8x!A%D*G-KaPdSgVkY");
        private static byte[] iv = Encoding.UTF8.GetBytes("A%D*G-KaPdSgVkYp");

        public static string CreateHash(string hashData, string AppSecret)
        {
            hashData = AppSecret + hashData;
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();


            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashData);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            String hash = Convert.ToBase64String(inputbytes);
            return hash;
        }

        public static string AESDecrypt(string InputText, string Password, string ivsalt)
        {
            //using var aes = Aes.Create();
            //var encryptedData = Convert.FromBase64String(InputText);
            //var salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            //using var secretKey = new PasswordDeriveBytes(Password, salt);
            //aes.Key = secretKey.GetBytes(32);
            //var plainText = aes.DecryptCbc(encryptedData, secretKey.GetBytes(16));
            //var decryptedData = Encoding.Unicode.GetString(plainText, 0, plainText.Length);
            //return decryptedData;

            if (InputText == null)
                return null;

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(InputText);

            byte[] Salt = Encoding.UTF8.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            // Create a decryptor from the existing SecretKey bytes.
            byte[] key = SecretKey.GetBytes(32);
            byte[] IV = SecretKey.GetBytes(16);

            if (!String.IsNullOrEmpty(ivsalt))
            {
                while (ivsalt.Length < 16)
                    ivsalt += ivsalt;
                IV = Encoding.UTF8.GetBytes(ivsalt.Substring(0, 16));
            }

            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(key, IV);

            MemoryStream memoryStream = new MemoryStream(EncryptedData);

            // Create a CryptoStream. (always use Read mode for decryption).

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data

            // will be, allocate the buffer long enough to hold EncryptedData;

            // DecryptedData is never longer than EncryptedData.

            byte[] PlainText = new byte[EncryptedData.Length];
            // Start decrypting.

            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();

            cryptoStream.Close();
            // Convert decrypted data into a string. 

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            // Return decrypted string.   

            return DecryptedData;
        }


        public static string AESEncrypt(string InputText, string Password, string ivsalt)
        {
            if (InputText == null)
                return null;

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

            // We are using salt to make it harder to guess our key
            // using a dictionary attack.

            byte[] Salt = Encoding.UTF8.GetBytes(Password.Length.ToString());

            // The (Secret Key) will be generated from the specified 
            // password and salt.

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a encryptor from the existing SecretKey bytes.
            // We use 32 bytes for the secret key 
            // (the default Rijndael key length is 256 bit = 32 bytes) and
            // then 16 bytes for the IV (initialization vector),
            // (the default Rijndael IV length is 128 bit = 16 bytes)

            byte[] key = SecretKey.GetBytes(32);
            byte[] IV = SecretKey.GetBytes(16);
            if (!String.IsNullOrEmpty(ivsalt))
            {
                while (ivsalt.Length < 16)
                    ivsalt += ivsalt;
                IV = Encoding.UTF8.GetBytes(ivsalt.Substring(0, 16));
            }

            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(key, IV);

            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();

            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;

        }

        public static bool ValidateDate(string encryptedText, string value)
        {
            var decrtpt = Decrypt(encryptedText);
            var seperate = decrtpt.Split("!");
            var date = DateTime.ParseExact(seperate[1], "yyyy-MM-dd HH:mm:ss",
                                 CultureInfo.InvariantCulture);
            var minute = (DateTime.Now - date).Minutes;
            if (value.ToUpper() == seperate[0] && minute < 5)
            {
                return true;
            }
            return false;
        }
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string CreateCaptchaValue()
        {
            string captchaValue = RandomString(6);
            return captchaValue;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetCaptchaImageBase64(string captchaValue, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            string returnValue = string.Empty;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(200, 75))
            using (var gfx = Graphics.FromImage((System.Drawing.Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, 200);
                        x = rand.Next(0, 200);
                        y = rand.Next(0, 75);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captchaValue, new Font("Tahoma", 16), Brushes.Gray, 55, 35);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                returnValue = Convert.ToBase64String(mem.GetBuffer());
            }
            return returnValue;
        }

        public static string DateEncript(string plainText)
        {
            plainText += "!" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return Encrypt(plainText);
        }

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        byte[] encryptedBytes = msEncrypt.ToArray();
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
        }

        public static string CreateHashBase(string hashData)
        {
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] hashbytes = System.Text.Encoding.GetEncoding("ISO-8859-9").GetBytes(hashData);
            byte[] inputbytes = sha.ComputeHash(hashbytes);

            String hash = Convert.ToBase64String(inputbytes);
            return hash;
        }

        public static string GeneratePassword(bool IncludeLowercase, bool IncludeUppercase, bool IncludeNumeric, bool IncludeSpecial, bool IncludeSpaces, int LengthOfPassword)
        {
            const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 1;
            const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
            const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMERIC_CHARACTERS = "0123456789";
            const string SPECIAL_CHARACTERS = @"!#$%&*@\";
            const string SPACE_CHARACTER = " ";
            const int PASSWORD_LENGTH_MIN = 8;
            const int PASSWORD_LENGTH_MAX = 14;

            if (LengthOfPassword < PASSWORD_LENGTH_MIN || LengthOfPassword > PASSWORD_LENGTH_MAX)
            {
                return "Password length must be between 8 and 14.";
            }

            string CharacterSet = "";

            if (IncludeLowercase)
            {
                CharacterSet += LOWERCASE_CHARACTERS;
            }

            if (IncludeUppercase)
            {
                CharacterSet += UPPERCASE_CHARACTERS;
            }

            if (IncludeNumeric)
            {
                CharacterSet += NUMERIC_CHARACTERS;
            }

            if (IncludeSpecial)
            {
                CharacterSet += SPECIAL_CHARACTERS;
            }

            if (IncludeSpaces)
            {
                CharacterSet += SPACE_CHARACTER;
            }

            char[] Password = new char[LengthOfPassword];
            int CharacterSetLength = CharacterSet.Length;

            Random Random = new Random();
            for (int CharacterPosition = 0; CharacterPosition < LengthOfPassword; CharacterPosition++)
            {
                Password[CharacterPosition] = CharacterSet[Random.Next(CharacterSetLength - 1)];

                bool MoreThanTwoIdenticalInARow =
                    CharacterPosition > MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && Password[CharacterPosition] == Password[CharacterPosition - 1]
                    && Password[CharacterPosition - 1] == Password[CharacterPosition - 2];

                if (MoreThanTwoIdenticalInARow)
                {
                    CharacterPosition--;
                }

            }

            string finalpass = string.Join(null, Password);
            if (IncludeNumeric && !Regex.IsMatch(finalpass, @"(?=.*\d)"))
            {
                char ch = NUMERIC_CHARACTERS[Random.Next(NUMERIC_CHARACTERS.Length - 1)];
                int inx = Random.Next(finalpass.Length - 1);
                finalpass = finalpass.Substring(0, inx) + ch + finalpass.Substring(inx + 1);
            }

            if (IncludeLowercase && !Regex.IsMatch(finalpass, @"(?=.*[a-z])"))
            {
                char ch = LOWERCASE_CHARACTERS[Random.Next(LOWERCASE_CHARACTERS.Length - 1)];
                int inx = Random.Next(finalpass.Length - 1);
                finalpass = finalpass.Substring(0, inx) + ch + finalpass.Substring(inx + 1);
            }

            if (IncludeUppercase && !Regex.IsMatch(finalpass, @"(?=.*[A-Z])"))
            {
                char ch = UPPERCASE_CHARACTERS[Random.Next(UPPERCASE_CHARACTERS.Length - 1)];
                int inx = Random.Next(finalpass.Length - 1);
                finalpass = finalpass.Substring(0, inx) + ch + finalpass.Substring(inx + 1);
            }

            if (IncludeSpecial && !Regex.IsMatch(finalpass, @"(?=.*[!#$%&*@\])"))
            {
                char ch = SPECIAL_CHARACTERS[Random.Next(SPECIAL_CHARACTERS.Length - 1)];
                int inx = Random.Next(finalpass.Length - 1);
                finalpass = finalpass.Substring(0, inx) + ch + finalpass.Substring(inx + 1);
            }
            if (IncludeSpaces && finalpass.IndexOf(' ') < 0)
            {
                char ch = ' ';
                int inx = Random.Next(finalpass.Length - 1);
                finalpass = finalpass.Substring(0, inx) + ch + finalpass.Substring(inx + 1);
            }
            return finalpass;
        }



    }
}

    
