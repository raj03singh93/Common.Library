using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Handler
{
    /// <summary>
    /// Delegating Handler to add authorization header to httpClient.
    /// </summary>
    public class JwtHttpMessageHandler : DelegatingHandler
    {
        private readonly HttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor">httpContextAccessor</param>
        public JwtHttpMessageHandler(HttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
