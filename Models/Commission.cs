using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kuwadro.Models
{
    public class Commission
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string FinalArt { get; set; }

        

    }
}
