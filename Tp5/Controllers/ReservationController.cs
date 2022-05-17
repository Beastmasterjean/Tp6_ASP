using Microsoft.AspNetCore.Mvc;
using Tp5.ViewModel;
using Tp5.DataAccessLayer;
using Tp5.Models;

namespace Tp5.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Details(int id)
        {
            DAL dal = new DAL();
            Reservation reservation = dal.reservationFactory.Get(id);
    
            Menu menu = dal.MenuFactory.Get(reservation.MenuChoiceId);

            if(reservation != null)
            {
                DetailsViewModel viewModel = new DetailsViewModel
                {
                    Reservation = reservation,
                    Menu = menu,
                };

                return View(viewModel);
            }
            return View("SiteMessage", new SiteMessagesViewModel
            {
                Message = "L'identifiant de la reservation est introuvable."
            });
        }
    }
}
