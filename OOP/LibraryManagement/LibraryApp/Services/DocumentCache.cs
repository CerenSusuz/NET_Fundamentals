using LibraryApp.Documents;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Services;

public class DocumentCache(Dictionary<DocumentType, TimeSpan> cacheExpiryPerType)
{
    private readonly MemoryCache _cache = new(new MemoryCacheOptions());
    private readonly Dictionary<DocumentType, TimeSpan> _cacheExpiryPerType = cacheExpiryPerType;

    public T GetDocument<T>(string title) where T : Document
    {
        _cache.TryGetValue(title, out T document);
        
        return document;
    }

    public void SetDocument<T>(T document) where T : Document
    {
        if (_cacheExpiryPerType.TryGetValue(document.Type, out var expiry))
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(expiry);
            _cache.Set(document.Title, document, cacheEntryOptions);
        }
    }

    public void RemoveDocument(string title)
    {
        _cache.Remove(title);
    }
}
