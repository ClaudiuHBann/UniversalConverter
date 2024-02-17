using System.Reflection;

using API.Entities;

using LazyCache;

using Microsoft.EntityFrameworkCore;

using Shared.Entities;
using Shared.Requests;
using Shared.Responses;
using Shared.Exceptions;
using Shared.Validators;

namespace API.Services
{
public class LinkZipService : BaseDbService<LinkZipRequest, LinkEntity, LinkZipResponse>
{
    private readonly UCContext _context;
    private readonly RadixService _radix;
    private readonly IAppCache _cache;
    private readonly LinkValidator _validator;

    public LinkZipService(UCContext context, RadixService radix, IAppCache cache, LinkValidator validator)
        : base(context)
    {
        _context = context;
        _radix = radix;
        _cache = cache;
        _validator = validator;
    }

    public override async Task<List<string>> FromTo() =>
        await Task.FromResult<List<string>>(["Shortifier", "Longifier"]);

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

        if (!fromTo.Any(ft => ft.Equals(request.From, StringComparison.OrdinalIgnoreCase)))
        {
            throw new FromToException(this, true);
        }

        if (!fromTo.Any(ft => ft.Equals(request.To, StringComparison.OrdinalIgnoreCase)))
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
                    // TODO: add the validation errors to the exception
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
            urls.Add(await Invoke<Task<string>>($"{request.From[0]}To{request.To[0]}", [url])!);
        }

        return new(urls);
    }

    private Result Invoke<Result>(string method, object?[]? parameters)
        where Result : class
    {
        var methodValidation = GetType().GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic);
        var methodValidationResult = methodValidation!.Invoke(this, parameters)!;
        return (Result)methodValidationResult;
    }

    private async Task<string> SToL(string code)
    {
        var link = await _cache.GetAsync<string>(code);
        if (link != null)
        {
            return link;
        }

        var entity = await ReadEx(MakeId(code));
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

        entity = await Create(new() { Url = url });
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
