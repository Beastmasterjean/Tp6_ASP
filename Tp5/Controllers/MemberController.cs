using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tp5.DataAccessLayer;
using Tp5.Helpers;
using Tp5.Models;
using Tp5.Resources;
using Tp5.ViewModel;

namespace Tp5.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member/Login
        public IActionResult Login()
        {
            Member member = new DAL().memberFactory.CreateEmpty();
            MemberLoginViewModel viewModel = new MemberLoginViewModel(member);

            return View(viewModel);
        }

        // POST: Member/Login
        [HttpPost]
        public IActionResult Login(MemberLoginViewModel viewModel, string returnurl)
        {
            if (ModelState.IsValid)
            {
                Member user = new DAL().memberFactory.GetByUsername(viewModel.Username);

                if (user != null)
                {
                    //bool valid = viewModel.Password == user.Password;
                    bool valid = CryptographyHelper.ValidateHashedPassword(viewModel.Password, user.Password);

                    if (valid)
                    {
                        //Create the identity for the user  
                        var identity = new ClaimsIdentity(new[] {
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Role, user.Role)
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
                        {
                            if (user.Role == Member.ROLE_STANDARD && returnurl.ToLower().StartsWith("/admin"))
                            {
                                return RedirectToAction("Index", "Home", new { Area = "" });
                            }

                            return LocalRedirect(returnurl);
                        }
                        else if (user.Role == Member.ROLE_ADMIN)
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }

                        return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                }

                ModelState.AddModelError("Password", Resource.InvalidUsernamePassword);
            }

            return View(viewModel);
        }

        // GET: Member/Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        // GET: Member/Create
        public IActionResult Create()
        {
            Member member = new DAL().memberFactory.CreateEmpty();

            return View(member);
        }

        // POST: Member/Create
        [HttpPost]
        public IActionResult Create(Member member)
        {
            if (ModelState.IsValid)
            {
                member.Role = Models.Member.ROLE_STANDARD;
                member.Password = CryptographyHelper.HashPassword(member.Password);

                new DAL().memberFactory.Save(member);

                return RedirectToAction("Login");
            }

            return View(member);
        }

        // GET: Member/List
        [Authorize(Roles = Member.ROLE_ADMIN + "," + Member.ROLE_STANDARD)]
        public IActionResult List()
        {
            Member[] members = new DAL().memberFactory.GetAll();

            return View(members);
        }
    }
}
