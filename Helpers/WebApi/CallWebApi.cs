using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
namespace WebApp.Helpers.WebApi
{
    public class CallWebApi
    {
        private WebApiInfo WebApiInfo { get; set; }
        public CallWebApi(IOptions<WebApiInfo> settings)
        {
            WebApiInfo = settings.Value;
        }
        public HttpClient InitializeClient()
        {
            var client = new HttpClient();
            //Passing service base url    
            client.BaseAddress = new Uri(WebApiInfo.Url.ToString());

            client.DefaultRequestHeaders.Clear();
            //Define request data format    
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}