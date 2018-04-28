using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json; 
using System.Threading.Tasks;  
using Newtonsoft.Json.Linq; 
namespace WebApp.Helpers.WebApi
{
    public class WebApiClient
    {
        private WebApiSetting _webApiSetting;
        private HttpClient _client;
        private string _webApiCaller;
        public WebApiClient(WebApiSetting webApiSetting)
        {
            this._webApiSetting = webApiSetting;            
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

        private string MakeCustomApiUri(List<WebApiParameter> parameters = null){
            WebApiCaller webApiCaller = this._webApiSetting.WebApiCallers.Find(x => x.Name == this._webApiCaller);
            WebApiController webApiController = webApiCaller.Controller;
            WebApiAction webApiAction = webApiController.Action;
            //List<WebApiParameter> parameters = webApiAction.Parameters;
            string returnValue = "";
            returnValue += this._webApiSetting.WebApiPrefix;
            returnValue += "/" + webApiController.Name;
            returnValue += "/" + webApiAction.Name;
            if( parameters != null){
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
            }            
            return returnValue;
        }

        private string MakeApiUri(WebApiParameter parameter = null){
            WebApiCaller webApiCaller = this._webApiSetting.WebApiCallers.Find(x => x.Name == this._webApiCaller);
            WebApiController webApiController = webApiCaller.Controller;
            WebApiAction webApiAction = webApiController.Action;
            //List<WebApiParameter> parameters = webApiAction.Parameters;
            string returnValue = "";
            returnValue += this._webApiSetting.WebApiPrefix;
            returnValue += "/" + webApiController.Name;
            returnValue += "/" + webApiAction.Name;
            if( parameter != null){
                returnValue += "/" + parameter.Value;
            }            
            return returnValue;
        }

        /*private string MakePostApi(){
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
        }*/

        public async Task<List<T>> ListFromCustomApi<T>(List<WebApiParameter> parameters = null){
            HttpResponseMessage res = await this._client.GetAsync(MakeCustomApiUri(parameters));  

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

        public async Task<T> GetFromCustomApi<T>(List<WebApiParameter> parameters = null){
            HttpResponseMessage res = await this._client.GetAsync(MakeCustomApiUri(parameters));  
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

        public async Task<List<T>> ListFromApi<T>(WebApiParameter parameter = null){
            HttpResponseMessage res = await this._client.GetAsync(MakeApiUri(parameter));  

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

        public async Task<T> GetFromApi<T>(WebApiParameter parameter = null){
            HttpResponseMessage res = await this._client.GetAsync(MakeApiUri(parameter));  
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

        public async Task<T> PostToApi<T>(T postItem){
            string stringData = JsonConvert.SerializeObject(postItem);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage res = await this._client.PostAsync(MakeApiUri(),contentData);          
            //res.EnsureSuccessStatusCode();
            var returnVal = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(returnVal);
        }

        public async Task<T> PutToApi<T>(WebApiParameter parameter, T putItem){
            string stringData = JsonConvert.SerializeObject(putItem);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage res = await this._client.PutAsync(MakeApiUri(parameter),contentData);          
            //res.EnsureSuccessStatusCode();
            var returnVal = res.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(returnVal);
        }

        public async Task<string> DeleteToApi(WebApiParameter parameter){
            HttpResponseMessage res = await this._client.DeleteAsync(MakeApiUri(parameter));          
            //res.EnsureSuccessStatusCode();
            var returnVal = res.Content.ReadAsStringAsync().Result;
            return returnVal.ToString();
        }
    }
}