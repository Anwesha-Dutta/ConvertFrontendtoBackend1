using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ConvertFrontendtoBackend1.Models;

namespace ConvertFrontendtoBackend1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            List<banner> list = new List<banner>();
            using (SqlConnection connection = new SqlConnection(Constring))
            {
               
                SqlCommand command = new SqlCommand("select [id],[banner_subdescription], [banner_description],[banner_image] from banner", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        list.Add(new banner
                        {
                            id = reader.GetInt32(0),
                            banner_subdescription = reader.GetString(1),
                            banner_description = reader.GetString(2),
                            banner_image = reader.GetString(3)

                        });
                    
                    }
                    reader.Close();
                }
            }
            ViewBag.list = list;
            return View();

        }

        
    }
}