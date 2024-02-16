using FluentValidation;

using Shared.Entities;

namespace Shared.Validators
{
public class LinkValidator : AbstractValidator<LinkEntity>
{
    public LinkValidator()
    {
        RuleFor(x => x.Url).Length(0, 2048).MustAsync(ValidateURL);
    }

    private async Task<bool> ValidateURL(string url, CancellationToken token)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.SendAsync(new(HttpMethod.Head, url), token);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
}
