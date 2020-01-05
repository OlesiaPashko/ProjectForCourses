using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Contracts.V1.Requests
{
    public class RequestWithImage
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
