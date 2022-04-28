using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models;

namespace Tp5.Areas.Admin.ViewModels
{
    public class ListReservationViewModel
    {
        private readonly Dictionary<int, Reservation> _reservationById = new Dictionary<int, Reservation>();

        public Reservation Reservation { get; set; }
        public Reservation[] Reservations { get; set; }

        public Menu Menu { get; set; }
        public Menu[] Menus { get; set; }
    }
}
