using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.DataAccessLayer.Factories;

namespace Tp5.DataAccessLayer
{
    public class DAL
    {
        private MenuFactory _menuFactory = null;
        private ReservationFactory _reservationFactory = null;
        public static string ConnectionString { get; set; }

        public MenuFactory MenuFactory
        {
            get
            {
                if(_menuFactory == null)
                {
                    _menuFactory = new MenuFactory();
                }
                return _menuFactory;
            }
        }

        public ReservationFactory reservationFactory
        {
            get
            {
                if(_reservationFactory == null)
                {
                    _reservationFactory = new ReservationFactory();
                }
                return _reservationFactory;
            }
        }

        public DAL()
        { }
    }
}
