using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Common.Library.Model
{
    /// <summary>
    /// Jwt Token Generator Model
    /// </summary>
    public class JwtTokenGeneratorModel
    {
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Audiance
        /// </summary>
        public string Audiance { get; set; }

        /// <summary>
        /// Key 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// ExpireAfter (In Minutes)
        /// </summary>
        public int ExpireAfter { get; set; }

        /// <summary>
        /// NotBefore
        /// </summary>
        public DateTime? NotBefore { get; set; }

        /// <summary>
        /// List of Claims to create.
        /// </summary>
        public List<Claim> Claims { get; set; }
    }
}
