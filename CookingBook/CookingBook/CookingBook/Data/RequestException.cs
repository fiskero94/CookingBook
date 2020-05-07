using System;
using System.Net.Http;

namespace CookingBook.Data
{
    public class RequestException : Exception
    {
        public HttpResponseMessage Response { get; private set; }

        public RequestException(string message, HttpResponseMessage response) : base(message)
        {
            Response = response;
        }
    }
}