﻿using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace ExamTesting
{
    public class BaseTest
    {
        protected readonly WebApplicationFactory<Program> _factory;

        public BaseTest()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        public HttpClient GetClient => _factory.CreateClient();
    }
}


