namespace WebApp.Helpers.WebApi
{
    public class WebApiSetting
    {
        public string WebApiUrl { get; set; }
        public string WebApiPrefix { get; set; }
        public List<WebApiCaller> WebApiCallers {get;set;}
    }

    public class WebApiCaller{
        public string Name {get;set;}
        public WebApiController Controller {get;set;}
    }

    public class WebApiController{
        public string Name {get;set;}
        public WebApiAction Action {get;set;}
    }
    
    public class WebApiAction{
        public string Name {get;set}
        public List<WebApiParameter> Parameters {get;set;}
    }

    public class WebApiParameter{
        public string Name {get;set;}
        public string Value {get;set;}
    }
}