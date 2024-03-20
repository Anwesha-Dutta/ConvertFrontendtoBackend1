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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Banner()
        {

            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            List<banner> list = new List<banner>();
            using (SqlConnection connection = new SqlConnection(Constring))
            {

                SqlCommand command = new SqlCommand("select [id],[banner_subdescription], [banner_description],[banner_image] from banner ", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
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

        public ActionResult Create(banner banner)
        {

            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            if (banner.banner_subdescription != null && banner.banner_description != null && banner.banner_image != null)
            {
                using (SqlConnection connection = new SqlConnection(Constring))
                {
                    SqlCommand command = new SqlCommand("insert into banner values (@banner_subdescription,@banner_description,@banner_image)", connection);
                    connection.Open();

                    command.Parameters.AddWithValue("@banner_subdescription", banner.banner_subdescription);
                    command.Parameters.AddWithValue("@banner_description", banner.banner_description);
                    command.Parameters.AddWithValue("@banner_image", "Content/assets/images/faces/" + banner.banner_image);


                    command.ExecuteNonQuery();

                    return RedirectToAction("Banner");
                }
                
            }

            return View();
        }
        [HttpGet]

        public ActionResult Delete(int id)
        {
            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            List<banner> list = new List<banner>();
            using (SqlConnection connection = new SqlConnection(Constring))
            {
                SqlCommand command = new SqlCommand("select [id],[banner_subdescription], [banner_description],[banner_image] from banner where id=@id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
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

        [HttpPost]

        public ActionResult Delete(int id,banner banner)

        {
            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(Constring))
            {

                SqlCommand command = new SqlCommand("delete from banner where id =@id" , connection);
                connection.Open();

                command.Parameters.AddWithValue("@id",banner.id);
                command.ExecuteNonQuery();

            }

            return RedirectToAction("Banner");
            return View();

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            banner banner = null;
            string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(Constring))
            {
                SqlCommand command = new SqlCommand("select[id],[banner_subdescription], [banner_description],[banner_image] from banner where id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    banner = new banner
                    {
                        id = reader.GetInt32(0),
                        banner_subdescription = reader.GetString(1),
                        banner_description = reader.GetString(2),
                        banner_image = reader.GetString(3)
                    };

                }
                reader.Close();
            }
            return View(banner);
        }

        [HttpPost]
        public ActionResult Update(int id, banner banner)
        {
            try
            {
                string Constring = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(Constring))
                {
                    SqlCommand command = new SqlCommand("update [banner] set [banner_subdescription]=@banner_subdescription, [banner_description]=@banner_description,[banner_image]=@banner_image where id=@id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@banner_subdescription", banner.banner_subdescription);
                    command.Parameters.AddWithValue("@banner_description", banner.banner_description);
                    command.Parameters.AddWithValue("@banner_image", "Content/assets/images/faces/" + banner.banner_image);

                    connection.Open();
                    command.ExecuteNonQuery();

                }
                return RedirectToAction("Banner");
            }

            catch
            {
                return View();
            }
        }
    }
}

