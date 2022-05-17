using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Areas.Admin.ViewModels;
using Tp5.DataAccessLayer;
using Tp5.Models;

namespace Tp5.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Member.ROLE_ADMIN)]
    public class MenuController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();
            ListMenuViewModel viewModel = new ListMenuViewModel
            {
                Menus = dal.MenuFactory.GetAll()
            };

            return View(viewModel);

        }

        public IActionResult Create()
        {
            DAL dal = new DAL();

            CreateEditMenuViewModel viewModel = new CreateEditMenuViewModel
            {
                Menu = dal.MenuFactory.CreateEmpty(),
            };

            return View("CreateEdit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEditMenuViewModel viewModel, IFormFile uploadFile)
        {
            if (viewModel != null && viewModel.Menu != null )
            {
                DAL dal = new DAL();

                Menu existingMenu = dal.MenuFactory.Get(viewModel.Menu.id);
                if (existingMenu != null)
                {
                    // Il est possible d'ajouter une erreur personnalisée.
                    // Le premier paramètre est la propriété touchée (à partir du viewModel ici)
                    ModelState.AddModelError("Menu.Id", "Le id de menu existe déjà.");
                    viewModel.Menus = dal.MenuFactory.GetAll();
                    return View("CreateEdit", viewModel);
                }
                else if (uploadFile != null && uploadFile.Length > 0)
                {
                    string extension = Path.GetExtension(uploadFile.FileName).ToLower();
                    string filename = String.Format("{0}{1}", Guid.NewGuid().ToString(), extension);

                    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\", filename);

                    using FileStream stream = System.IO.File.Create(pathToSave);
                    uploadFile.CopyTo(stream);

                    viewModel.Menu.ImagePath = filename;
                    ModelState["Menu.ImagePath"].ValidationState = ModelValidationState.Valid;
                }
                // Si le modèle n'est pas valide, on retourne à la vue CreateEdit où les messages seront affichés.
                // Le ViewModèle reçu en POST n'est pas complet (seulement les info dans le <form> sont conservées),
                // il faut donc réaffecter les Catégories.
                if (!ModelState.IsValid)
                {
                    viewModel.Menus = dal.MenuFactory.GetAll();
                    return View("CreateEdit", viewModel);
                }

                dal.MenuFactory.Save(viewModel.Menu);
            }

            return RedirectToAction("List");
        }

        public IActionResult Edit(int id)
        {
            if(id > 0)
            {
                DAL dal = new DAL();
                Menu menu = dal.MenuFactory.Get(id);

                if(menu != null)
                {
                    CreateEditMenuViewModel viewModel = new CreateEditMenuViewModel
                    {
                        Menu = menu,
                    };
                    return View("CreateEdit",viewModel);
                }                       
            }
            return View("SiteMessage", new SiteMessageViewModel
            {
                Message = "L'identifiant du menu est introuvable (Admin/Menu/" + id + ")."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateEditMenuViewModel viewModel, IFormFile uploadFile)
        {
            if (viewModel != null && viewModel.Menu != null)
            {
                DAL dal = new DAL();

                Menu existingmenu = dal.MenuFactory.Get(viewModel.Menu.id);
                if (existingmenu != null && existingmenu.id != viewModel.Menu.id)
                {
                    // Il est possible d'ajouter une erreur personnalisée.
                    // Le premier paramètre est la propriété touchée (à partir du viewModel ici)
                    ModelState.AddModelError("menu.Id", "Le id de menu existe déjà.");
                    viewModel.Menus = dal.MenuFactory.GetAll();
                    return View("CreateEdit", viewModel);
                }
                else if (uploadFile != null && uploadFile.Length > 0)
                {
                    string extension = Path.GetExtension(uploadFile.FileName).ToLower();
                    string filename = String.Format("{0}{1}", Guid.NewGuid().ToString(), extension);

                    string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\", filename);

                    using FileStream stream = System.IO.File.Create(pathToSave);
                    uploadFile.CopyTo(stream);

                    viewModel.Menu.ImagePath = filename;
                    ModelState["Menu.ImagePath"].ValidationState = ModelValidationState.Valid;
                }
                // Si le modèle n'est pas valide, on retourne à la vue CreateEdit où les messages seront affichés.
                // Le ViewModèle reçu en POST n'est pas complet (seulement les info dans le <form> sont conservées),
                // il faut donc réaffecter les Catégories.
                if (!ModelState.IsValid)
                {
                    viewModel.Menus = dal.MenuFactory.GetAll();
                    return View("CreateEdit", viewModel);
                }

                dal.MenuFactory.Save(viewModel.Menu);
            }

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            if(id > 0)
            {
                DAL dal = new DAL();
                Menu menu = dal.MenuFactory.Get(id);

                if(menu != null)
                {
                    ListMenuViewModel viewModel = new ListMenuViewModel
                    {
                        Menu = menu,
                        Menus = dal.MenuFactory.GetAll(),
                    };
                    return View(viewModel);
                }
                               
            }

            return View("SiteMessage", new SiteMessageViewModel
            {
                Message = "L'identifiant du menu est introuvable (Admin/Menu/" + id + ")."
            });
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                new DAL().MenuFactory.Delete(id);
            }

            return RedirectToAction("List");
        }
    }
}
