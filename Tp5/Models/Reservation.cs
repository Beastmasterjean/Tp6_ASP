using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp5.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public int nbPersonne { get; set; }
        public int menuChoiceId { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string courriel { get; set; }
        public DateTime date { get; set; }

        public Reservation()
        {

        }
        public Reservation(int id, int nbPersonne, int menuChoiceId, string nom, string courriel, DateTime date)
        {
            this.id = id;
            this.nbPersonne = nbPersonne;
            this.menuChoiceId = menuChoiceId;
            this.nom = nom;
            this.courriel = courriel;
            this.date = date;
        }
    }
}
