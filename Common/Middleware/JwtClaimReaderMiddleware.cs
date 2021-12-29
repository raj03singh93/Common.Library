using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Common.Middleware
{

    /// <summary>
    /// Middleware to read claims sent in Jwt
    /// </summary>
    public class JwtClaimReaderMiddleware
    {
        /// <summary>
        /// Request Delegate to invoke next middleware in pipeline
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// ClaimsIdentity that contains all the claims
        /// </summary>
        private static ClaimsIdentity Identity;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next">To initialize _next</param>
        public JwtClaimReaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Method that invokes this middleware
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <returns>calls next middleware in pipeline</returns>
        public async Task Invoke(HttpContext context)
        {
            Identity = context.User.Identity as ClaimsIdentity;
            await _next.Invoke(context);
        }

        /// <summary>
        /// Gets the claim value
        /// </summary>
        /// <typeparam name="T">datatype of claim value</typeparam>
        /// <param name="claimName">name of claim</param>
        /// <returns>value of claim</returns>
        public static T GetClaimValue<T>(string claimName)
        {
            T result = default(T);
            if (Identity == null || Identity.FindFirst(claimName) == null)
                return result;

            result = (T)Convert.ChangeType(Identity.FindFirst(claimName).Value, typeof(T));
            return result;
        }
    }
}
