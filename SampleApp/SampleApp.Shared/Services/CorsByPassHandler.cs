using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class CorsByPassHandler : DelegatingHandler
    {
        public CorsByPassHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var builder = new UriBuilder(request.RequestUri)
            {
                Host = "cors-anywhere.herokuapp.com",
            };

            builder.Path = request.RequestUri.Host + builder.Path;
            
            return base.SendAsync(new HttpRequestMessage(request.Method, builder.Uri){
                Headers = { {"origin", ""} },
            }, cancellationToken);
        }
    }
}