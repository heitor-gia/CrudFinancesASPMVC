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
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");


            return View(categoriesRepository.GetAllFromUser((int)Session["id_user"]));
        }

        [HttpGet]
        public ActionResult New()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            else return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            category.id_user = (int)Session["id_user"];
            categoriesRepository.Create(category);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            if (categoriesRepository.GetById(id).id_user == (int)Session["id_user"])
            {
                try
                {
                    categoriesRepository.Delete(id);
                }catch(MySql.Data.MySqlClient.MySqlException ex)
                {
                    TempData["error"]= "Não é possível excluir uma categoria que possui depesas catadastradas.";
                }
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