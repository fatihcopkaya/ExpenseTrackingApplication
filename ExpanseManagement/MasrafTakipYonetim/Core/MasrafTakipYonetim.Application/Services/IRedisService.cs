using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Services
{
    public interface ICacheService
    {
        //Task<string> GetStringAsync(string key);
        //Task<bool> SetStringAsync(string key, string value, TimeSpan? expiry = null);
        public Task<T> GetAsync<T>(string key);
        public Task SetAsync<T>(string key, T item, Action<CacheSettings> config);
        public Task RefreshAsync(string key);
        public Task RemoveAsync(string key);
    }
    public sealed class CacheSettings
    {
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
    }
}
