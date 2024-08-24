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
public class LinkZipService : BaseService<LinkZipRequest, LinkZipResponse>
{
    private readonly UCContext _context;
    private readonly RadixService _radix;
    private readonly IAppCache _cache;
    private readonly LinkValidator _validator;

    private const string _defaultFrom = "Longifier";
    private const string _defaultTo = "Shortifier";
    private static readonly string[] _fromTo = [_defaultTo, _defaultFrom];

    public LinkZipService(UCContext context, RadixService radix, IAppCache cache, LinkValidator validator)
        : base(context)
    {
        _context = context;
        _radix = radix;
        _cache = cache;
        _validator = validator;
    }

    public override bool IsConverter() => true;

    public override string GetServiceName() => "LinkZip";

    private string GetPrefix()
    {
#if DEBUG
        return $"localhost:5173/{GetServiceName()}/?code=";
#else
        return $"uc.hbann.ro/{GetServiceName()}/?code=";
#endif
    }

    public override async Task<FromToResponse> FromTo() => await Task.FromResult(new FromToResponse([.._fromTo],
                                                                                                    _defaultFrom,
                                                                                                    _defaultTo));

    protected override async Task ConvertValidate(LinkZipRequest request)
    {
        if (request.URLs.Count > 69)
        {
            throw new ValueException("The maximum number of links is 69!");
        }

        if (request.From.Equals(request.To, StringComparison.OrdinalIgnoreCase))
        {
            throw new FromToException(this, false);
        }

        var fromTo = await FromTo();

        if (!fromTo.FromTo.Any(ft => ft.Equals(request.From, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.FromTo.Any(ft => ft.Equals(request.To, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, false);
        }

        if (request.From.Equals("Longifier", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var url in request.URLs)
            {
                var result = await _validator.ValidateAsync(new LinkEntity(url));
                if (!result.IsValid)
                {
                    var errors = result.Errors.Select(e => e.ErrorMessage);
                    var title = "The link is invalid!";
                    var content = string.Join('\n', errors);

                    throw new ValueException($"{title}\n{content}");
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

        return new(urls);
    }

    private async Task<string> SToL(string code)
    {
        var link = await _cache.GetAsync<string>(code);
        if (link != null)
        {
            return link;
        }

        var entity = await Read<LinkEntity>(new() { Id = MakeId(code) });
        _cache.Add(code, entity.Url);
        return entity.Url;
    }

    private async Task<string> LToS(string url)
    {
        var code = await _cache.GetAsync<string>(url);
        if (code != null)
        {
            return code;
        }

        var entity = await _context.Links.FirstOrDefaultAsync(link => link.Url == url);
        if (entity != null)
        {
            return MakeCode(entity.Id);
        }

        entity = await Create<LinkEntity>(new() { Url = url });
        code = MakeCode(entity.Id);

        _cache.Add(url, code);
        return code;
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
