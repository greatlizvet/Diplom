﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeForPets.Models
{
    public class Error
    {
        public string ExceptionPageUrl { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string UserName { get; set; }
    }
}