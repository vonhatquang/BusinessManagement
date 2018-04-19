using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
namespace WebApp.Helpers.WebApi
{
    public class CallWebApi
    {
        private WebApiSetting _webApiSetting { get; set; }
        private HttpClient _client;
        private string _webApiCaller;
        public CallWebApi(IOptions<WebApiSetting> settings)
        {
            this._webApiSetting = settings.Value;
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
                if("".equal(parameterValue)){
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
            string parameterValue = "";
            for (int i=0; i < parameters.Count; i++)
            {
                if("".equal(parameterValue)){
                    parameterValue += "?" + parameters[i].Name + "=" + parameters[i].Value; 
                }else{
                    parameterValue += "&" + parameters[i].Name + "=" + parameters[i].Value;
                }
            }
            returnValue += parameterValue;
            return returnValue;
        }

        public T GetFromApi(){
            HttpResponseMessage res = await client.GetAsync(MakeGetApi());  

            T dto = new T();
            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                dto = JsonConvert.DeserializeObject<T>(result);  
  
            }
            return dto;
        }

        public List<T> ListFromApi(){
            HttpResponseMessage res = await client.GetAsync(MakeGetApi());  

            List<T> dtos = new List<T>();
            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                dtos = JsonConvert.DeserializeObject<List<T>>(result);  
  
            }
            return dto;
        }

        public int PostToApi(List<WebApiParameter> parameters){
            string parameterValue = "";
            for (int i=0; i < parameters.Count; i++)
            {
                if("".equal(parameterValue)){
                    parameterValue += "?" + parameters[i].Name + "=" + parameters[i].Value; 
                }else{
                    parameterValue += "&" + parameters[i].Name + "=" + parameters[i].Value;
                }
            }
            HttpResponseMessage res = await client.GetAsync(MakePostApi(parameterValue));  

            int dto = 0;
            //Checking the response is successful or not which is sent using HttpClient    
            if (res.IsSuccessStatusCode)  
            {  
                //Storing the response details recieved from web api     
                var result = res.Content.ReadAsStringAsync().Result;  
  
                //Deserializing the response recieved from web api and storing into the Employee list    
                dto = JsonConvert.DeserializeObject<int>(result);  
  
            }
        }
    }
}