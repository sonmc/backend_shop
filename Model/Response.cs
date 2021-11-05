using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Response
    {
        public int Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
