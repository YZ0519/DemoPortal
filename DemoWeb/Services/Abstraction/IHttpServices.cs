namespace SimplePOSWeb.Services.Abstraction
{
    public interface IHttpServices
    {
        HttpClient GetHttpClient();
        HttpClient GetHttpClientWithBearerToken();
    }
}
