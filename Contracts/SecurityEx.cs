using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class SecurityEx
    {
        string message;

        [DataMember]
        public string Message { get => message; set => message = value; }

        public SecurityEx(string message)
        {
            Message = message;
        }
    }
}
