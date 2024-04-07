//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.Security.FormsAuthentication;
using HelpDeskTrain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
//using System.Web.UI.Page;

namespace HelpDeskTrain.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // такой код для отрабатывания представлений
        // Роут обязательно, View() передавать можно и название твоей вью и пустым оставить
        // ну и модель как у тебя ниже в логин
        [Route("Account/Login")]
        /*public IActionResult Login()
        {
            return View("login");
        }
        
        [HttpPost]*/
        public async Task<IActionResult> Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, model.UserName),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));
                    //.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Request");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (HelpdeskContext _db = new HelpdeskContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}

