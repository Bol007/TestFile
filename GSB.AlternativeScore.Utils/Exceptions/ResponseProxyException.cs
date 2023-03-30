using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Exceptions
{
    public class ResponseProxyException : Exception
    {

        public ResponseProxyException(int statusCode, string message):base(message)
        {
            StatusCode = statusCode;
            try
            {
                ErrModel = JsonConvert.DeserializeObject<ApiError>(message);
            }
            catch (Exception)
            {
                // ถ้าเป็น err ที่ convert ไม่ได้รับ Message อย่างเดียว ()
                ErrModel = new ApiError()
                {
                    Message = message
                };
            }
       
        }

        public int StatusCode { get; set; }

        public ApiError ErrModel { get; set; }

    }
}
