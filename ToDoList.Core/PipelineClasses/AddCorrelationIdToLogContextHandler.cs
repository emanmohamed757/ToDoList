using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoList.Core.PipelineClasses
{
    internal class AddCorrelationIdToLogContextHandler : DelegatingHandler
    {
        private const string CorrelationIdHeaderName = "X-Correlation-Id";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.TryGetValues(CorrelationIdHeaderName, out var values))
            {
                var newId = Guid.NewGuid().ToString();
                request.Headers.Add(CorrelationIdHeaderName, newId);
                values = new[] { newId };
            }

            using (LogContext.PushProperty("CorrelationId", values.First()))
            {
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}