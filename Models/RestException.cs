﻿using System;
using System.Net;

namespace uploaddownloadfiles.Models
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
        public HttpStatusCode Code { get; }
        public object Errors { get; }

        public override string ToString()
        {
            return Message;
        }

    }
}
