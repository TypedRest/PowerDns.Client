using System;
using System.Net.Http;
using RichardSzalay.MockHttp;

namespace PowerDns.Client
{
    public abstract class EndpointFactsBase : IDisposable
    {
        public const string JsonMime = "application/json";

        protected readonly MockHttpMessageHandler Mock = new MockHttpMessageHandler();
        protected readonly IPowerDnsClient Client;

        protected EndpointFactsBase()
        {
            Client = new PowerDnsClient(new Uri("http://localhost/"), new HttpClient(Mock));
        }

        public void Dispose() => Mock.VerifyNoOutstandingExpectation();
    }
}
