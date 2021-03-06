﻿using Finances.Models.Entities;
using Finances.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finances.Controllers
{
    public class ExpensesController : Controller
    {

        ExpensesRepository expensesRepository = new ExpensesRepository();
        CategoriesRepository categoriesRepository = new CategoriesRepository();
        EstablishmentsRepository establishmentsRepository = new EstablishmentsRepository();
        

        public ActionResult Index()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            ViewBag.categories = categoriesRepository.GetAllFromUser((int)Session["id_user"]);
            ViewBag.establishments = establishmentsRepository.GetAllFromUser((int)Session["id_user"]);
            return View(expensesRepository.GetAllFromUser((int)Session["id_user"], 7));
        }

        [HttpGet]
        public ActionResult New()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            ViewBag.categories = categoriesRepository.GetAllFromUser((int)Session["id_user"]);
            ViewBag.establishments = establishmentsRepository.GetAllFromUser((int)Session["id_user"]);
            if (ViewBag.categories.Count == 0 || ViewBag.categories.Count == 0)
            {
                TempData["alert"] = "Primeiro adincione pelo menos uma categoria e um estabelecimento.";
                return RedirectToAction("Index", "Expenses");
            } else return View();
        }

        [HttpPost]
        public ActionResult New(Expense expense)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            expense.id_user = (int)Session["id_user"];
            expensesRepository.Create(expense);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login","Home");

            if (expensesRepository.GetById(id).id_user == (int)Session["id_user"])
            {
                expensesRepository.Delete(id);   
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login", "Home");

            Expense expense = expensesRepository.GetById(id);
            ViewBag.categories = categoriesRepository.GetAllFromUser((int)Session["id_user"]);
            ViewBag.establishments = establishmentsRepository.GetAllFromUser((int)Session["id_user"]);
            if (expense.id_user == (int)Session["id_user"])
            {
                return View(expense);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (expense.id_user == (int)Session["id_user"])
            {
                expensesRepository.Edit(expense);
            }

            return RedirectToAction("Index");
            
        }

        public ActionResult Search()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login", "Home");

            ViewBag.categories = categoriesRepository.GetAllFromUser((int)Session["id_user"]);
            ViewBag.establishments = establishmentsRepository.GetAllFromUser((int)Session["id_user"]);
            return View();
        }

        [HttpPost]
        public ActionResult SearchResults()
        {
            if (Session["loggedIn"] == null) return RedirectToAction("Login", "Home");

            int id_establishment = Int32.Parse(Request["id_establishment"]);
            int id_category= Int32.Parse(Request["id_category"]);
            string period = Request["period"];

            ViewBag.categories = categoriesRepository.GetAllFromUser((int)Session["id_user"]);
            ViewBag.establishments = establishmentsRepository.GetAllFromUser((int)Session["id_user"]);
            List<Expense> results = expensesRepository.Search(id_establishment, id_category, period, (int)Session["id_user"]);
            return View(results);
        }
    }
}