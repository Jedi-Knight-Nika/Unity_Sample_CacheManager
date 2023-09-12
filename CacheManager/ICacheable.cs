using System;

namespace Nikolla_L
{
    public interface ICacheable
    {
        string GetCacheCategory();
        string CacheEvict();
        bool IsFree();
        void SetFree(bool free);
    }
}

