using API.Entities;

using LazyCache;

using Microsoft.EntityFrameworkCore;

using Shared.Requests;
using Shared.Responses;

namespace API.Services
{
public class LinkZipService : BaseDbService<LinkZipRequest, LinkEntity, LinkZipResponse>
{
    private const string URLBase = "https://localhost:5001/LinkZip/";

    private readonly UCContext _context;
    private readonly RadixService _radix;
    private readonly IAppCache _cache;

    public LinkZipService(UCContext context, RadixService radix, IAppCache cache) : base(context)
    {
        _context = context;
        _radix = radix;
        _cache = cache;
    }

    public override async Task<List<string>> FromTo() =>
        await Task.FromResult<List<string>>(["Shortifier", "Longifier"]);

    public override async Task<LinkZipResponse> Convert(LinkZipRequest request)
    {
        await Validate(request);

        List<string> urls = [];
        request.URLs.ForEach(async url =>
                             {
                                 var code = await _cache.GetAsync<string>(url);
                                 if (code != null)
                                 {
                                     urls.Add(URLBase + code);
                                     return;
                                 }

                                 var entity = await _context.Links.FirstOrDefaultAsync(link => link.LinkLong == url);
                                 if (entity != null)
                                 {
                                     urls.Add(URLBase + MakeCode(entity.Id));
                                     return;
                                 }

                                 entity = new() { LinkLong = url };
                                 await CreateValidate(entity);
                                 entity = await Create(entity);

                                 code = MakeCode(entity.Id);

                                 urls.Add(URLBase + code);
                                 _cache.Add(url, code);
                             });

        return new(urls);
    }

    public async Task<string> Find(string code)
    {
        var entity = await Read(new() { Id = MakeId(code) });
        return entity.LinkLong;
    }

    private string MakeCode(long id)
    {
        RadixRequest request = new() { From = "10", To = "36", Numbers = [id++.ToString()] };
        return _radix.Convert(request).Result.Numbers.First();
    }

    private long MakeId(string code)
    {
        RadixRequest request = new() { From = "36", To = "10", Numbers = [code] };
        return long.Parse(_radix.Convert(request).Result.Numbers.First());
    }
}
}
