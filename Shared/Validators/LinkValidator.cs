using FluentValidation;

using Shared.Entities;

namespace Shared.Validators
{
public class LinkValidator : AbstractValidator<LinkEntity>
{
    public LinkValidator()
    {
        RuleFor(x => x.Url).MustAsync(BeAValidURL);
    }

    private static async Task<bool> BeAValidURL(string url, CancellationToken cancellationToken)
    {
        if (url.Length < 4 || url.Length > 2048)
        {
            return false;
        }

        try
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Head, url);

            var response = await client.SendAsync(request, cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}
}
