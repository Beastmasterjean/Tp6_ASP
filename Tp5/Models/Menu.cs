using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Resources;

namespace Tp5.Models
{
    public class Menu
    {
        public List<Menu> listMenu = new List<Menu>();
        public int id { get; set; }
        [Required]
        public string nom { get; set; }

        [Display(Name = "Image", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        public string ImagePath { get; set; }

        public Menu()
        {

        }
        public Menu(int id, string nom, string imagePath)
        {
            this.id = id;
            this.nom = nom;
            ImagePath = imagePath;
        }
    }
}
