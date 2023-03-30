using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Exceptions
{
    public class AppException: Exception
    {

        public AppException(string message, HttpStatusCode httpStatus) : base(message)
        {
            HttpStatus = httpStatus;
        }

        public HttpStatusCode HttpStatus { get; set; }

    }

}
