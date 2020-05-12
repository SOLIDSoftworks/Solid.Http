using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// An exception thrown when a request is expected to be successful, but the status code is betwen 500 and above.
    /// </summary>
    public class ServerErrorException : HttpRequestException
    {
        internal ServerErrorException(string message) 
            : base(message)
        {
        }
    }
}
