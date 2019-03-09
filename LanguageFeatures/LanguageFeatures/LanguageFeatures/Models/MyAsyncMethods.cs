using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        //using continue with
        public static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            //we are just waiting to get the result. we can do something else also here
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
            {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }

        //using async await
        public async static  Task<long?> GetPageLengthAsync()
        {
            HttpClient client = new HttpClient();
            var httpTask = await client.GetAsync("http://apress.com");
            //we are just waiting to get the result. we can do something else also here
            return httpTask.Content.Headers.ContentLength;
        }
    }
}