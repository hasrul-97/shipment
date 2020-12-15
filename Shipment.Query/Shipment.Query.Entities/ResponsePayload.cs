using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Query.Entities
{
    public class ResponsePayload<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
    }
}
