using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ServerErrorResponse : Response
    {
        public override string Message { get; set; } = "Error";
        public override int StatusCode { get; set; } = 500;
        public IEnumerable<string> Validation { get; set; }
    }

    public class ServerErrorResponseMVVC : Response
    {
        public override string Message { get; set; } = "Error";
        public override int StatusCode { get; set; } = 500;
        public List<KeyValuePair<string, List<string>>> Validation { get; set; }
    }
}
