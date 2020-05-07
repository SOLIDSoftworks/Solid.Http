using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    public class ClientErrorException : HttpRequestException
    {
        public ClientErrorException()
        {
        }

        public ClientErrorException(string message) 
            : base(message)
        {
        }

        public ClientErrorException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}
