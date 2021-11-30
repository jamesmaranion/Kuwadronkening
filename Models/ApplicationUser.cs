using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Kuwadro.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfilePicture { get; set; }

        public string Background { get; set; }

        public string Bio { get; set; }
        [DataType(DataType.MultilineText)]
        public string About { get; set; }
    }
}
