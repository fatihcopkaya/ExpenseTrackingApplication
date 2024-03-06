using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Persistence.Manager
{
    public class CryptographyService : ICryptographyService
    {
        private readonly IConfiguration _configuration;
        private string appsecret;
        public CryptographyService(IConfiguration configuration)
        {
            _configuration = configuration;
            appsecret = configuration["Jwt:Secret"];

        }

        public string AESDecrypt(string field, string ivsalt)
        {
            return Cryptography.AESDecrypt(field, appsecret, ivsalt);
        }

        public string AESDecrypt(string field)
        {
            return Cryptography.AESDecrypt(field, appsecret, null);

        }
        public string AESDecrypt(string field, string password, string ivsalt)
        {
            return Cryptography.AESDecrypt(field, password, ivsalt);

        }

        public string AESEncrypt(string field, string ivsalt)
        {
            return Cryptography.AESEncrypt(field, appsecret, ivsalt);
        }

        public string AESEncrypt(string field)
        {
            return Cryptography.AESEncrypt(field, appsecret, null);
        }

        public string AESEncrypt(string field, string password, string ivsalt)
        {
            return Cryptography.AESDecrypt(field, password, ivsalt);

        }

        public string CreateHash(string hashData)
        {
            return Cryptography.CreateHash(hashData, appsecret);

        }

        public string CreateHashBase(string hashData)
        {
            return Cryptography.CreateHashBase(hashData);
        }

        public string GeneratePassword(bool IncludeLowercase, bool IncludeUppercase, bool IncludeNumeric, bool IncludeSpecial, bool IncludeSpaces, int LengthOfPassword)
        {
            return Cryptography.GeneratePassword(IncludeLowercase, IncludeUppercase, IncludeNumeric, IncludeSpecial, IncludeSpaces, LengthOfPassword);
        }
    }
}
