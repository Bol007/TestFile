using GSB.AlternativeScore.Utils.Exceptions;
using GSB.AlternativeScore.Utils.Converts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GSB.AlternativeScore.Utils.APIs
{

    public class ApiGateProxyBuilder
    {

        private ApiProxy clientProxy;
        private string BaseEndPoint = "";

        public ApiGateProxyBuilder(string baseEndPoint)
        {
            BaseEndPoint = baseEndPoint;
        }

        public ApiProxy Build() => clientProxy = new ApiProxy(BaseEndPoint);
    }

    public class ApiProxy
    {
        public readonly HttpClient client;
        public string ResponseContent="";

        public ApiProxy(string baseEndPoint)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseEndPoint);
            client.DefaultRequestHeaders.Accept.Clear();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        public ApiProxy AddHeaderApplicationJson()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return this;
        }

        public ApiProxy AddHeaderApplicationFormUrlencoded()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            return this;
        }

        public ApiProxy AddHeader(string key, string value)
        {
            client.DefaultRequestHeaders.Add(key, value);
            return this;
        }

        public ApiProxy AddHeaderAuthorization(string token)
        {
            client.DefaultRequestHeaders.Add("Authorization", token);
            return this;
        }

        public ApiProxy AddHeaderAuthorizationBearer(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        public ApiProxy AddHeaderAuthorizationBasic(string username, string password)
        {
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(username + ":" + password));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", svcCredentials);
            return this;
        }

        public T GetToType<T>(string endPointPath, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);
            var response = client.GetAsync(endPointPath).Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public T GetToTypeWithErr<T>(string endPointPath, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);
            var response = client.GetAsync(endPointPath).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode, response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

            var content = response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8();
            ResponseContent = content;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public HttpResponseMessage Get(string endPointPath, double timeout = 100)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            return client.GetAsync(endPointPath).Result;
        }

         public HttpResponseMessage Post(string endPointPath, Object data, double timeout = 100)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

            return client.PostAsync(endPointPath, dataContent).Result;

        }

        public T PostToType<T>(string endPointPath, Object data, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(endPointPath, dataContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);

        }

        public T PostToTypeWithErr<T>(string endPointPath, Object data, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(endPointPath, dataContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode,response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);

        }

        public T PostToTypeWithErr<T>(string endPointPath, Dictionary<string,string> formUrlEncodedContent , double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);


            var values = new List<KeyValuePair<string, string>>();
            foreach (var data in formUrlEncodedContent)
            {
                values.Add(new KeyValuePair<string, string>(data.Key, data.Value));
            }
            var requestContent = new FormUrlEncodedContent(values);


            var response = client.PostAsync(endPointPath, requestContent).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode, response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);

        }

        public HttpResponseMessage Put(string endPointPath, Object data, double timeout = 100)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

            return client.PutAsync(endPointPath, dataContent).Result;

        }

        public void PutWithErr(string endPointPath, Object data, double timeout = 100)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PutAsync(endPointPath, dataContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode, response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

        }


        public T PutToType<T>(string endPointPath, Object data, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PutAsync(endPointPath, dataContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }


        public T PutToTypeWithErr<T>(string endPointPath, Object data, double timeout = 100) where T : class
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);

            var dataJson = JsonConvert.SerializeObject(data);
            var dataContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = client.PutAsync(endPointPath, dataContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode, response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(content);
        }

        public void DeleteWithErr(string endPointPath, double timeout = 100)
        {
            client.Timeout = TimeSpan.FromSeconds(timeout);
            var response = client.DeleteAsync(endPointPath).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ResponseProxyException((int)response.StatusCode, response.Content.ReadAsByteArrayAsync().Result.ByteToStringUtf8());
            }

        }

        public class ErrModel
        {

            private string _ErrMessage;
            public string ErrMessage
            {
                get { return _ErrMessage; }
                set { _ErrMessage = value.Replace("\"", ""); }
            }
            public int Status { get; set; }
        }

    }

    public static class ApiProxyExtension
    {
        public static ApiProxy.ErrModel ToErrModel(this string source)
        {
            return JsonConvert.DeserializeObject<ApiProxy.ErrModel>(source);
        }

    }

}