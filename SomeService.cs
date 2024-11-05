using Microsoft.Extensions.Caching.Hybrid;

public class SomeService(HybridCache cache)
{
    private HybridCache _cache = cache;

    public async Task<string> GetSomeInfoAsync(string name, int id, CancellationToken token = default)
    {
        var tags = new List<string> { "tag1", "tag2", "tag3" };
        var entryOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(1),
            LocalCacheExpiration = TimeSpan.FromMinutes(1)
        };
        return await _cache.GetOrCreateAsync(
            $"{name}-{id}", // Unique key to the cache entry
            async cancel => await GetDataFromTheSourceAsync(name, id, cancel),
            entryOptions,
            tags,
            cancellationToken: token
        );
    }
    
    public async Task<string> GetDataFromTheSourceAsync(string name, int id, CancellationToken token)
    {
        string someInfo = $"someinfo-{name}-{id}";
        return someInfo;
    }
}