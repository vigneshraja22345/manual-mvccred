using manual_mvccred.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace manual_mvccred.Controllers
{
    public class studentController : Controller
    {


        String constr = "Data Source=VIGNESHWARAN;Integrated Security=True;Initial Catalog=studentproject";
        // GET: student
        public ActionResult Index()
        {
            List<student> tbl_obj = new List<student>();
            SqlConnection conn = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("sp_get", conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbl_obj.Add(new student
                {
                    id = Convert.ToInt32(dr["id"]),
                    stu_name = Convert.ToString(dr["stu_name"]),
                    age = Convert.ToInt32(dr["age"]),
                }
                );
            }
            conn.Close();

            return View(tbl_obj);


        
        }

        // GET: student/Details/5
        public ActionResult Details(int id,student tbl_obj)
        {

            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_getid " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbl_obj = new student
                {
                    id = Convert.ToInt32(dr["id"]),
                    stu_name = Convert.ToString(dr["stu_name"]),
                    age = Convert.ToInt32(dr["age"]),
                };

            }
            conn.Close();

            return View(tbl_obj);
            
        }

        // GET: student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: student/Create
        [HttpPost]
        public ActionResult Create(student tbl_obj)
        {
            try
            {

                SqlConnection conn = new SqlConnection(constr);
                string query = "sp_insert'" + tbl_obj.stu_name + "'," + tbl_obj.age;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");



                // TODO: Add insert logic here

                
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: student/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, student tbl_obj)
        {
            try
            {
                // TODO: Add update logic here



                SqlConnection conn = new SqlConnection(constr);
                string query = "sp_edit'" + id + "','" + tbl_obj.stu_name + "'," + tbl_obj.age;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");

               
            }
            catch
            {
                return View();
            }
        }

        // GET: student/Delete/5
        public ActionResult Delete(int id, student tbl_obj)
        {

            SqlConnection conn = new SqlConnection(constr);
            string query = "sp_getid " + id;
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tbl_obj = new student
                {
                    id = Convert.ToInt32(dr["id"]),
                    stu_name = Convert.ToString(dr["stu_name"]),
                    age = Convert.ToInt32(dr["age"]),
                };
               
            }
            conn.Close();

            return View(tbl_obj);
            
        }

        // POST: student/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                SqlConnection conn = new SqlConnection(constr);
                string query ="sp_delete " + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                conn.Close();
             

              return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
