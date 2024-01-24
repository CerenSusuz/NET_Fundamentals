using LibraryApp.Documents;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryApp.Services;

public class DocumentCache(Dictionary<DocumentType, TimeSpan> cacheExpiryPerType)
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions());
    private readonly Dictionary<DocumentType, TimeSpan> _cacheExpiryPerType = cacheExpiryPerType;

    public T GetDocument<T>(string title) where T : BaseDocument
    {
        _cache.TryGetValue(title, out T document);
        
        return document;
    }

    public void SetDocument<T>(T document) where T : BaseDocument
    {
        if (!_cacheExpiryPerType.TryGetValue(document.Type, out var expiry))
        {
            return;
        }

        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(DateTimeOffset.UtcNow + expiry);

        if (cacheEntryOptions.AbsoluteExpiration <= DateTimeOffset.UtcNow)
        {
            Console.WriteLine("The document was not added to the cache. Its expiration time is in the past.");
            
            return;
        }

        _cache.Set(document.Title, document, cacheEntryOptions);
    }

    public void RemoveDocument(string title)
    {
        _cache.Remove(title);
    }
}
