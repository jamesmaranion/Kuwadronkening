using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kuwadro.Models
{
    public class Art
    {
        [Key]
        public int Id { get; set; }


        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Image { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }


        public Genres Genre { get; set; }

        public bool Featured { get; set; }
        public bool Recommended { get; set; }
        public bool Popular { get; set; }
    }

    public enum Genres
    {
        Anime = 1,
        Modernism = 2,
        History = 3, 
        Fantasy = 4, 
        Abstract = 5,
        Expressionism = 6,
        Horror = 7,
        Science = 8,
        Supernatural = 9,
        Comic = 10,
    }
}
