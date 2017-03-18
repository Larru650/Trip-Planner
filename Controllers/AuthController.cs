using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager) //we need a ctor to inject the signinmanager
        {

            _signInManager = signInManager;
        }
        public IActionResult Login()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Trips", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(vm.Username,
                                                                      vm.Password,
                                                                      true,
                                                                      false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                    { 
                    return RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect"); //we don't want to add a key as either one or the other or both properties failed, so we will just use the model

                }
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
               await _signInManager.SignOutAsync(); //this will also get rid of the cookie collection so the user then is signed out

            }
           
                return RedirectToAction("Index", "App");
            
        }

    }

}
