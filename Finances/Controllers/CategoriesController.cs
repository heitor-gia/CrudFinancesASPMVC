using Finances.Models.Entities;
using Finances.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finances.Controllers
{
    public class CategoriesController : Controller
    {
        CategoriesRepository categoriesRepository = new CategoriesRepository();


        public ActionResult Index()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");


            return View(categoriesRepository.GetAllFromUser((int)Session["id_user"]));
        }

        [HttpGet]
        public ActionResult New()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

            else return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

            category.id_user = (int)Session["id_user"];
            categoriesRepository.Create(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login");

            if (categoriesRepository.GetById(id).id_user == (int)Session["id_user"])
            {
                categoriesRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = categoriesRepository.GetById(id);

            if (category.id_user == (int)Session["id_user"])
            {
                return View(category);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (category.id_user == (int)Session["id_user"])
            {
                categoriesRepository.Edit(category);
            }

            return RedirectToAction("Index");

        }
    }
}