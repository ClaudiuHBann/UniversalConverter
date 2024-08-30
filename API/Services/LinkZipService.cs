using API.Entities;

using LazyCache;

using Microsoft.EntityFrameworkCore;

using Shared.Entities;
using Shared.Requests;
using Shared.Utilities;
using Shared.Responses;
using Shared.Exceptions;
using Shared.Validators;

namespace API.Services
{
public class LinkZipService
(UCContext context, RadixService radix, IAppCache cache, LinkValidator validator)
    : BaseService<LinkZipRequest, LinkZipResponse>(context)
{
    private readonly UCContext _context = context;
    private readonly RadixService _radix = radix;
    private readonly IAppCache _cache = cache;
    private readonly LinkValidator _validator = validator;

    private const string _defaultFrom = "Longifier";
    private const string _defaultTo = "Shortifier";
    private static readonly string[] _fromTo = [_defaultTo, _defaultFrom];

    public override bool IsConverter() => true;

    public override string GetServiceName() => "LinkZip";

    private string GetPrefix()
    {
#if DEBUG
        return $"localhost:5173/{GetServiceName()}?code=";
#else
        return $"hbann.ro/{GetServiceName()}?code=";
#endif
    }

    public override async Task<FromToResponse> FromTo() => await Task.FromResult(
        new FromToResponse() { FromTo = [.._fromTo], DefaultFrom = _defaultFrom, DefaultTo = _defaultTo });

    protected override async Task ConvertValidate(LinkZipRequest request)
    {
        await base.ConvertValidate(request);

        if (request.URLs.Count > 69)
        {
            throw new ValueException("The maximum number of links is 69!");
        }

        if (request.From.Equals("Longifier", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var url in request.URLs)
            {
                var result = await _validator.ValidateAsync(new LinkEntity(url));
                if (!result.IsValid)
                {
                    // TODO: the new lines are not permited
                    throw new ValueException("The link is invalid!");
                }
            }
        }
    }

    protected override async Task<LinkZipResponse> ConvertInternal(LinkZipRequest request)
    {
        List<string> urls = [];
        foreach (var url in request.URLs)
        {
            urls.Add(await this.Invoke<Task<string>>($"{request.From[0]}To{request.To[0]}", [url])!);
        }

        return new() { URLs = urls };
    }

#pragma warning disable IDE0051 // Remove unused private members
    private async Task<string> SToL(string url)
#pragma warning restore IDE0051 // Remove unused private members
    {
        var link = await _cache.GetAsync<string>(url);
        if (link != null)
        {
            return link;
        }

        var code = url.Replace(GetPrefix(), null);
        var entity = await Read<LinkEntity>(new() { Id = MakeId(code) });

        _cache.Add(url, entity.Url);
        return entity.Url;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private async Task<string> LToS(string urlLong)
#pragma warning restore IDE0051 // Remove unused private members
    {
        var urlShort = await _cache.GetAsync<string>(urlLong);
        if (urlShort != null)
        {
            return urlShort;
        }

        var entity = await _context.Links.FirstOrDefaultAsync(link => link.Url == urlLong);
        if (entity != null)
        {
            return GetPrefix() + MakeCode(entity.Id);
        }

        entity = await Create<LinkEntity>(new() { Url = urlLong });
        urlShort = GetPrefix() + MakeCode(entity.Id);

        _cache.Add(urlLong, urlShort);
        return urlShort;
    }

    private string MakeCode(long id)
    {
        RadixRequest request = new() { From = "10", To = "36", Numbers = [id.ToString()] };
        return _radix.Convert(request).Result.Numbers.First();
    }

    private long MakeId(string code)
    {
        RadixRequest request = new() { From = "36", To = "10", Numbers = [code] };
        return long.Parse(_radix.Convert(request).Result.Numbers.First());
    }
}
}
