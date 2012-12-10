using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContractWeb.Common
{
    public class CustomJsonResult : JsonResult
    {
        private string _dateFormat = "yyyy-MM-dd hh:mm";

        public string dateFormat 
        {
            get
            {
                return _dateFormat;
            }

            set
            {
                _dateFormat = value;
            }
        }

        public CustomJsonResult()
        {
            this.JsonRequestBehavior = JsonRequestBehavior.AllowGet; 
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                // Using Json.NET serializer
                var isoConvert = new IsoDateTimeConverter();
                isoConvert.DateTimeFormat = _dateFormat;
                response.Write(JsonConvert.SerializeObject(Data, isoConvert));
            }
        }
    }
}