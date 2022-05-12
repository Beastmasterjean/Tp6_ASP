using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tp5.DataAccessLayer;
using Tp5.Models;
using Tp5.ViewModel;

namespace Tp5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            DAL dal = new DAL();

            ListViewModel viewModel = new ListViewModel()
            {
                Reservation = dal.reservationFactory.CreateEmpty(),
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ListViewModel viewModel)
        {
            if (viewModel != null && viewModel.Reservation != null)
            {
                DAL dal = new DAL();

                Reservation existingReservation = dal.reservationFactory.Get(viewModel.Reservation.id);
                if (existingReservation != null)
                {
                    // Il est possible d'ajouter une erreur personnalisée.
                    // Le premier paramètre est la propriété touchée (à partir du viewModel ici)
                    ModelState.AddModelError("Menu.Id", "Le id de menu existe déjà.");
                    viewModel.Reservations = dal.reservationFactory.GetAll();
                    return View("Index", viewModel);
                }

                // Si le modèle n'est pas valide, on retourne à la vue CreateEdit où les messages seront affichés.
                // Le ViewModèle reçu en POST n'est pas complet (seulement les info dans le <form> sont conservées),
                // il faut donc réaffecter les Catégories.
                if (!ModelState.IsValid)
                {
                    viewModel.Reservations = dal.reservationFactory.GetAll();
                    return View("Index", viewModel);
                }

                dal.reservationFactory.Save(viewModel.Reservation);
            }

            return RedirectToAction("Details", "Reservation");
        }
    }
}
