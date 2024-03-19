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
            return View();
        }

        public ActionResult Login(Login data)
        {
            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(Constring))
            {
                if(data.Username != null)
                {
                    SqlCommand command = new SqlCommand("select * from Login Where Username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", data.Username);
                    //command.Parameters.AddWithValue("@Password", data.Password);
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
            }
            // return View();
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
