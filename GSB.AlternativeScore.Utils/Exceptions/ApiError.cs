using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Exceptions
{

    public class ApiError
    {
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; } 
        public string Message { get; set; }
        public string Route { get; set; }

    }

}
