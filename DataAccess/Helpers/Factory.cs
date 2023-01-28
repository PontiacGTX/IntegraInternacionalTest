using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public static class Factory
    {
        public static class MVC
        { 

            public static Response GetResponse<T>(object response, int statusCode = 200, string message = "Ok", bool success = true, List<KeyValuePair<string,List<string>>> validation = null)

            {
                var tipo = typeof(T);
                if (tipo == typeof(Response))
                {
                    return new Response { Data = response, Message = message, StatusCode = statusCode, Success = success };
                }
                else if (tipo == typeof(ServerErrorResponse))
                {
                    if (response != null)
                        return new ServerErrorResponseMVVC { Data = response, StatusCode = statusCode, Message = message, Success = false, Validation = validation };

                    return new ServerErrorResponseMVVC { Data = null, StatusCode = statusCode, Message = message, Success = false, Validation = validation };
                }
                return null;
            }
        }
        public static  List<KeyValuePair<string, List<string>>> GetValidationMessage(KeyValuePair<string, List<string>> vallidationEntry)
        {
            return new List<KeyValuePair<string, List<string>>>() { vallidationEntry };
        }
        public static  KeyValuePair<string, List<string>> GetValidationKVEntry(string key,List<string> values)
        {
            //return new List<KeyValuePair<string, List<string>>>()
            //         {
            return new KeyValuePair<string, List<string>>(key, values);
                   // }
        }
        public static Response MVCBadRequest(this ModelStateDictionary ModelState,object model) 
        {
          
            
                return Factory.MVC.GetResponse<ServerErrorResponse>(response: model,
                    statusCode:400,
                    message:"BadRequest",
                    success:false,
                    validation:ModelState.SelectMany(entry => entry.Value.Errors.Select(e => new { entry.Key, e.ErrorMessage }))
                              .GroupBy(x => x.Key)
                              .Select(x => new KeyValuePair<string, List<string>>(x.Key, x.ToList().Select(y => y.ErrorMessage).ToList())).ToList()
                                                                    );
            
            
        }
        public static Response MVCBadRequest(List<KeyValuePair<string, List<string>>> validation, object model)
        {
           

            return Factory.MVC.GetResponse<ServerErrorResponse>(response: model,
                statusCode: 400,
                message: "BadRequest",
                success: false,
                validation:validation);


        }
        public static Response ErrorResponse()
       => Factory.GetResponse<ServerErrorResponse>(null,
                   500,
                   "Various internal unexpected errors happened",
                   false);
        public static byte[] GetBytes(this Response response) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response, GetCamelCaseOptions()));

        public static JsonSerializerSettings GetCamelCaseOptions()
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return serializerSettings;
        }



        public static Response GetResponse<T>(object response, int statusCode = 200, string message = "Ok", bool success = true, IEnumerable<string> validation = null)

        {
            var tipo = typeof(T);
            if (tipo == typeof(Response))
            {
                return new Response { Data = response, Message = message, StatusCode = statusCode, Success = success };
            }
            else if (tipo == typeof(ServerErrorResponse))
            {
                if (response != null)
                    return new ServerErrorResponse { Data = response, StatusCode = statusCode, Message = message, Success = false, Validation = validation };

                return new ServerErrorResponse { Data = null, StatusCode = statusCode, Message = message, Success = false, Validation = validation };
            }
            return null;
        }
    }
}
