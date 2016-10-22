using Finances.Models.Entities;
using Finances.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finances.Controllers
{
    public class EstablishmentsController : Controller
    {
        
        EstablishmentsRepository establishmentsRepository = new EstablishmentsRepository();


        public ActionResult Index()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

         
            return View(establishmentsRepository.GetAllFromUser((int)Session["id_user"]));
        }

        [HttpGet]
        public ActionResult New()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");
            
            else return View();
        }

        [HttpPost]
        public ActionResult New(Establishment establishment)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

            establishment.id_user = (int)Session["id_user"];
            establishmentsRepository.Create(establishment);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

            if (establishmentsRepository.GetById(id).id_user == (int)Session["id_user"])
            {
                establishmentsRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Establishment establishment = establishmentsRepository.GetById(id);

            if (establishment.id_user == (int)Session["id_user"])
            {
                return View(establishment);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Establishment establishment)
        {
            if (establishment.id_user == (int)Session["id_user"])
            {
                establishmentsRepository.Edit(establishment);
            }

            return RedirectToAction("Index");

        }
    }
}