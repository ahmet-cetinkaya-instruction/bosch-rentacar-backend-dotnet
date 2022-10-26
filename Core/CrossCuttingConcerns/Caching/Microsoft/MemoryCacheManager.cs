﻿using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheManager()
    {
        _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
    }

    public MemoryCacheManager(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Add(string key, object value, int duration)
    {
        _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
    }

    public bool IsAdd(string key)
    {
        return _memoryCache.TryGetValue(key, out _); // parametreyi boş geçmek, kullanmamak için _ kullanılabilir.
    }

    public object Get(string key)
    {
        return _memoryCache.Get(key);
    }

    public T Get<T>(string key)
    {
        return _memoryCache.Get<T>(key);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void RemoveByPattern(string pattern)
    {
        // Cache verileri, Microsoft dokümanına göre, EntriesCollection adında tutuluyor. EntriesCollection'ı Get ediyoruz.
        PropertyInfo? cacheEntriesCollectionDefinition = 
            typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);

        // dynamic, derleme zamanında değil, çalışma zamanında tipini dinamik olarak alan yapıdır.
        var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;

        List<ICacheEntry> cacheCollectionValues = new();
        foreach (var cacheEntry in cacheEntriesCollection)
        {
            ICacheEntry cacheEntryValue = cacheEntry.GetType().GetProperty("Value").GetValue(cacheEntry, null);
            cacheCollectionValues.Add(cacheEntryValue);
        }
    }
}