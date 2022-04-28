using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp5.Models
{
    public class Menu
    {
        public List<Menu> listMenu = new List<Menu>();
        public int id { get; set; }
        [Required]
        public string nom { get; set; }

        public Menu()
        {

        }
        public Menu(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }
    }
}
