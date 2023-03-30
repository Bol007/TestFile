using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Exceptions
{
    public class ClientException : AppException
    {
        public ClientException(string message, int errorCode = 0,HttpStatusCode httpStatus = HttpStatusCode.BadRequest) : base(message, httpStatus)
        {
            ErrorCode = errorCode;
            HttpStatus = httpStatus;
        }

        public int ErrorCode { get; set; }

        public ApiError GetStatus()
        {
            return new ApiError() { 
                ErrorCode = this.ErrorCode, 
                StatusCode = (int) HttpStatus, 
                Message = this.Message };
        }

    }
}
