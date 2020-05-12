using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Solid.Http
{
    /// <summary>
    /// An exception thrown when a request is expected to be successful, but the status code is between 400 and 499.
    /// </summary>
    public class ClientErrorException : HttpRequestException
    {
        internal ClientErrorException(string message) 
            : base(message)
        {
        }
    }
}
