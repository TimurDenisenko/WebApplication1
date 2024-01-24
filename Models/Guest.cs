using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Models
{
    public class Guest
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Sisesta nimi siia")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sisesta aastat vana siia")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Sisesta epost siia")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage ="Vale postkast")]
        public string Email { get; set; }

        [RegularExpression(@"\+372\d{8}", ErrorMessage = "Vale number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Sisesta oma valik siia")]
        public bool WillAttend { get; set; }
        public Holiday holiday { get; set; }
    }
}