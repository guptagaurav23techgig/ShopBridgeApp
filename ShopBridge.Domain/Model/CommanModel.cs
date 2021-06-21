using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Domain.Model
{
    public class CommanModel
    {

    }

    [DataContract]
    public class ResponseResult<T>
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }

        [DataMember(Name = "Message")]
        public string Message { get; set; }

        [DataMember(Name = "Data")]
        public T Data { get; set; }
    }

    public class RequestData
    {
        public string url { get; set; }
        public string data { get; set; }
    }

    public enum BoolData
    {
        False = 0,
        True = 1
    }
}
