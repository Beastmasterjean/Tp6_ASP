using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp5.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int NbPersonne { get; set; }
        public int MenuChoiceId { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Courriel { get; set; }
        public DateTime Date { get; set; }

        public Reservation()
        {

        }
        public Reservation(int id, int nbPersonne, int menuChoiceId, string nom, string courriel, DateTime date)
        {
            this.Id = id;
            this.NbPersonne = nbPersonne;
            this.MenuChoiceId = menuChoiceId;
            this.Nom = nom;
            this.Courriel = courriel;
            this.Date = date;
        }
    }
}
