using System;

namespace Postie.Api.Services
{
    public interface IUrlService
    {
        string GenerateUrl(string url);
    }

    public class UrlService : IUrlService
    {
        public string GenerateUrl(string uri)
        {
            return Uri.EscapeUriString(uri.Replace(" ", "-"));
        }
    }
}
