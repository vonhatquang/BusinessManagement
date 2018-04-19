using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Newtonsoft.Json; 
using System.Threading.Tasks;  
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq; 
namespace WebApp.Helpers.WebApi
{
    public class WebApiClient
    {
        private WebApiSetting _webApiSetting = new WebApiSetting();
        //private IConfigurationSection section; 
        private HttpClient _client;
        private string _webApiCaller;
        private IConfiguration _configuration;
        public WebApiClient(IConfiguration configuration)
        {
            this._configuration = configuration;
            JToken jAppSettings = JToken.Parse(
                File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
            );
            var section = jAppSettings["WebApiUrl"];
        }

        public void InitializeClient(string webApiCaller)
        {
            this._webApiCaller = webApiCaller;
            this._client = new HttpClient();
            //Passing service base url    
            this._client.BaseAddress = new Uri(this._webApiSetting.WebApiUrl.ToString());

            this._client.DefaultRequestHeaders.Clear();
            //Define request data format    
            this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private string MakeGetApi(){
            WebApiCaller webApiCaller = this._webApiSetting.WebApiCallers.Find(x => x.Name == this._webApiCaller);
            WebApiController webApiController = webApiCaller.Controller;
            WebApiAction webApiAction = webApiController.Action;
            List<WebApiParameter> parameters = webApiAction.Parameters;
            string returnValue = "";
            returnValue += this._webApiSetting.WebApiPrefix;
            returnValue += "/" + webApiController.Name;
            returnValue += "/" + webApiAction.Name;
            string parameterValue = "";
            for (int i=0; i < parameters.Count; i++)
            {
                if("".Equals(parameterValue)){
                    parameterValue += "?" + parameters[i].Name + "=" + parameters[i].Value; 
                }else{
                    parameterValue += "&" + parameters[i].Name + "=" + parameters[i].Value;
                }
            }
            returnValue += parameterValue;
            return returnValue;
        }

        private string MakePostApi(string parameterValue){
            WebApiCaller webApiCaller = this._webApiSetting.WebApiCallers.Find(x => x.Name == this._webApiCaller);
            WebApiController webApiController = webApiCaller.Controller;
            WebApiAction webApiAction = webApiController.Action;
            List<WebApiParameter> parameters = webApiAction.Parameters;
            string returnValue = "";
            returnValue += this._webApiSetting.WebApiPrefix;
            returnValue += "/" + webApiController.Name;
            returnValue += "/" + webApiAction.Name;
            returnValue += parameterValue;
            return returnValue;
        }

        public async Task<T> GetFromApi<T>(){
            HttpResponseMessage res = await this._client.GetAsync(MakeGetApi());  

            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                return JsonConvert.DeserializeObject<T>(result);  
  
            }
            return default(T);
        }

        public async Task<List<T>> ListFromApi<T>(){
            HttpResponseMessage res = await this._client.GetAsync(MakeGetApi());  

            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                return JsonConvert.DeserializeObject<List<T>>(result);  
  
            }
            return default(List<T>);
        }

        public async Task<int> PostToApi(List<WebApiParameter> parameters){
            string parameterValue = "";
            for (int i=0; i < parameters.Count; i++)
            {
                if("".Equals(parameterValue)){
                    parameterValue += "?" + parameters[i].Name + "=" + parameters[i].Value; 
                }else{
                    parameterValue += "&" + parameters[i].Name + "=" + parameters[i].Value;
                }
            }
            HttpResponseMessage res = await this._client.GetAsync(MakePostApi(parameterValue));  

            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                return JsonConvert.DeserializeObject<int>(result);  
  
            }
            return -1;
        }
    }
}