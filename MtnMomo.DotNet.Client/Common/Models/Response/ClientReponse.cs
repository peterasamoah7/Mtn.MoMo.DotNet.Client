﻿using System.Net;

namespace MtnMomo.DotNet.Client.Common.Models.Response
{
    public class ClientResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }

        public string Status { get; set; }
    }

    public class ClientResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Status { get; set; }
    }
}
