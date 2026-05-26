using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoList.Core.MessageHandlers
{
    internal class AddCorrelationIdToResponseHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = await base.SendAsync(request, cancellationToken);

            var correlationIdHeader = responseMessage
                .Headers
                .Where(header => header.Key == CorrelationIdHeaderName);
            if (correlationIdHeader == null)
            {
                responseMessage
                    .Headers
                    .Add(CorrelationIdHeaderName, Guid.NewGuid().ToString());
            }

            return responseMessage;
        }
    }
}