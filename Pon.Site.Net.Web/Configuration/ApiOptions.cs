namespace Pon.Site.Net.Web.Configuration
{
    public class ApiOptions
    {
        public const string PonSiteApi = "PonSiteApi";
        public string Url { get; set; } = string.Empty;
        public EndpointOption? Endpoints { get; set; }

    }
}
