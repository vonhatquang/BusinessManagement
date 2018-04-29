using System;
using System.Collections.Generic;
namespace WebApp.Helpers.WebApi
{
    public class WebApiSetting
    {
        public string WebApiUrl { get; set; }
        public string WebApiPrefix { get; set; }
        public List<WebApiController> WebApiControllers{get;set;}
    }   

    public class WebApiController{
        public string Name {get;set;}        
        public List<WebApiAction> WebApiActions {get;set;}
    }
    
    public class WebApiAction{
        public string Name {get;set;}
        public string ActionType{get;set;}
        public bool IsCustom {get;set;}
    }

    public class WebApiParameter{
        public string Name {get;set;}
        public string Value {get;set;}
    }
}