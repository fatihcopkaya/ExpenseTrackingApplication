using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Services
{
    public interface ICryptographyService
    {
        string AESDecrypt(string field, string ivsalt);
        string AESDecrypt(string field);
        string AESDecrypt(string field, string password, string ivsalt);
        string AESEncrypt(string field, string ivsalt);
        string AESEncrypt(string field);
        string AESEncrypt(string field, string password, string ivsalt);
        string CreateHash(string hashData);
        string CreateHashBase(string hashData);
        string GeneratePassword(bool IncludeLowercase, bool IncludeUppercase, bool IncludeNumeric, bool IncludeSpecial, bool IncludeSpaces, int LengthOfPassword);
    }
}
