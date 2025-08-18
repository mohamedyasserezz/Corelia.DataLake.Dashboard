using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.Register
{
    public record RegisterRequest(
            string Email,
            string Password,
            string FullName,
            IFormFile? Image,
            string UserType
            );
}
