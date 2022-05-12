using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Areas.Admin.ViewModels;
using Tp5.DataAccessLayer;
using Tp5.Models;

namespace Tp5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Member.ROLE_ADMIN)]
    public class ReservationController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();
            ListReservationViewModel viewModel = new ListReservationViewModel
            {
                Reservations = dal.reservationFactory.GetAll()
            };

            return View(viewModel);
        }
        #region Delete
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                DAL dal = new DAL();
                Reservation reservation = dal.reservationFactory.Get(id);

                if (reservation != null)
                {
                    ListReservationViewModel viewModel = new ListReservationViewModel
                    {
                        Reservation = reservation,
                    };

                    return View(viewModel);
                }
            }

            return View("SiteMessage", new SiteMessageViewModel
            {
                Message = "L'identifiant de la reservation est introuvable."
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                new DAL().reservationFactory.Delete(id);
            }

            return RedirectToAction("List");
        }
        #endregion
    }
}
