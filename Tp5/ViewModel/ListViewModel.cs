using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models;

namespace Tp5.ViewModel
{
    public class ListViewModel
    {
        public Reservation Reservation { get; set; }
        public Reservation[] Reservations { get; set; }
    }
}
