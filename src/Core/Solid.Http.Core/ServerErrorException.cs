using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    public class ServerErrorException : HttpRequestException
    {
        public ServerErrorException()
        {
        }

        public ServerErrorException(string message) 
            : base(message)
        {
        }

        public ServerErrorException(string message, Exception inner) 
            : base(message, inner)
        {
        }
    }
}
