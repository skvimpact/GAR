using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarPublicClient
{
    /*
    public static class HttpClientExtensions
    {
        public static HttpClient AddMessageId(this HttpClient httpClient){            
            httpClient.DefaultRequestHeaders
                .Add("X-messageID", Guid.NewGuid().ToString().Replace("-", String.Empty));
            return httpClient;
        }
        public static HttpClient AddMessageDt(this HttpClient httpClient){            
			httpClient.DefaultRequestHeaders
                .Add("X-messageDT", DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ffffffZ"));
            return httpClient;
        }        
        public static HttpClient AddCorrelationId(this HttpClient httpClient, Guid correlationId){            
            httpClient.DefaultRequestHeaders
                .Add("X-correlationID", correlationId.ToString().Replace("-", String.Empty));
            return httpClient;
        }        
    }
    */
}