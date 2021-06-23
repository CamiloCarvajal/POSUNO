using System;
using System.Collections.Generic;
using System.Text;

namespace POSUNO.Models
{
    class Response
    {
        public bool IsSuccess { get; set; }
        public string message { get; set; }
        public object  result { get; set; }
    }
}
