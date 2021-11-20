using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kuwadro.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePicture { get; set; }

        public string Background { get; set; }

        public string Bio { get; set; }
    }
}
