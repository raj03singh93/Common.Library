using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Library.Model
{
    public class JwtTokenModel
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
    }
}
