using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Exceptions
{
    public class HostException : AppException
    {
        public HostException(string message, HttpStatusCode httpStatus = HttpStatusCode.InternalServerError) : base(message, httpStatus)
        {
        }

    }

}
