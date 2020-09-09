using Http.Request.Service.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;

namespace Make.Magic.Challenge.Connector
{
    public abstract class ServiceConnector
    {
        protected internal IHttpService HttpService { get; }

        protected internal string ResourceName { get; }

        protected HttpRequest HttpRequest { get; private set; }

        public ServiceConnector(IHttpService httpService, string resourceName)
        {
            HttpService = httpService ?? throw new ArgumentNullException($"{httpService}. Adicione o 'app.MakeMagicConnector();' na 'startup.css'.");
            ResourceName = resourceName;
        }

        public void SetHttpRequest(HttpRequest httpRequest)
            => HttpRequest = httpRequest ?? throw new ArgumentNullException(nameof(httpRequest));
    }
}
