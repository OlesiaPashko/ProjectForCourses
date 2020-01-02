using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AuthentificationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
