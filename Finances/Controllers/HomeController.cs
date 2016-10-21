using Finances.Models.Entities;
using Finances.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finances.Controllers
{
    public class HomeController : Controller
    {

        UsersRepository repository = new UsersRepository();

        public ActionResult Index()
        {
            if (Session["logado"] != null) return RedirectToAction("Login");



            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["logado"] != null) return RedirectToAction("Index","Expenses");
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
            User result = null;
            if (user.name!=null && user.password!= null)
            {
                result = repository.Login(user); 
            }             
                
            if (result != null)
            {

                Session["name_user"] = result.name;
                Session["id_user"] = result.id;
                Session["loggedIn"] = true;
                return RedirectToAction("Index","Expenses");
            }
            else
            {
                ViewBag.erro = "Usuário e senha inválidos";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {

            if (Session["loggedIn"] != null) return RedirectToAction("Index", "Expenses");
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            repository.Create(user);
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            Session["name_user"] = null;
            Session["id_user"] = null;
            Session["loggedIn"] = null;
            return RedirectToAction("Login");
        }
    }
}