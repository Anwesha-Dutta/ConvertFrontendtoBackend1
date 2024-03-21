using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using ConvertFrontendtoBackend1.Models;
using System.Configuration;
using System.Data.SqlClient;
namespace ConvertFrontendtoBackend1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
          // Retrieve ModelState from TempData
    ModelStateDictionary modelState = TempData["ModelState"] as ModelStateDictionary;

            if (modelState != null)
            {
                // Transfer ModelState to the current ModelState
                ModelState.Merge(modelState);
            }

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login data)
        {
            if (ModelState.IsValid)
            {
                string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(Constring))
                {
                    if (data.Username != null && data.Password != null)
                    {
                        SqlCommand command = new SqlCommand("select * from Login Where Username = @Username AND Password = @Password", connection);
                        command.Parameters.AddWithValue("@Username", data.Username);
                        command.Parameters.AddWithValue("@Password", data.Password);
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                Session["Username"] = reader["Username"];
                            }

                            Session.Remove("Error");
                            return RedirectToAction("Welcome");
                        }
                        else
                        {
                            Session["Error"] = "Credential not found";
                        }
                    }
                    else
                    {
                        Session["Error"] = "Fields should not be empty";
                    }
                }
            }
            else
            {
                TempData["ModelState"] = ModelState; // Save ModelState in TempData
                return RedirectToAction("Index", "Login");
            }
          
            //return View();
           return RedirectToAction("Index");
        }
        public ActionResult Welcome()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
          return RedirectToAction("Index");
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
