using AuthorizationTask.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthorizationTask.Controllers
{
    public class HomeController : Controller
    {
        DBLayer db=new DBLayer();
          
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(DataModel data)
        {
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@name",data.name),
            new SqlParameter("@email",data.email),
            new SqlParameter("@address",data.add),
            new SqlParameter("@gender",data.gender),
            new SqlParameter("@qul",data.qul),
            new SqlParameter("@mobno",data.mobno),
            new SqlParameter("@pass",data.pass),
            new SqlParameter("@action",1),
            };
            int res = db.ExecuteDML("sp_register", para);
            if (res>0)
            {
                return Content("<script>alert('Registration Successfull !! Login Now');location.href='/home/login'</script>");
            }
            else
            {
                return Content("<script>alert('Some Error Occured');location.href='/home/signup'</script>");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email,string pass)
        {
            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@pass",pass),
            new SqlParameter("@email",email),
            new SqlParameter("@action",2), };
            DataTable dt = db.ExecuteSelect("sp_register", para);
            if(dt.Rows.Count>0)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return Content("<script>alert('Login Successfull');location.href='/home/dashboard'</script>");
            }
            else
            {
                return Content("<script>alert('Invalid Username or password');location.href='/home/login'</script>");
            }
            
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@email",User.Identity.Name),
            new SqlParameter("@action",3),

            };
            DataTable dt = db.ExecuteSelect("sp_register", para);
            return View(dt);
        }
        [Authorize]
        public ActionResult Changepass(string email)
        {
            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@email",User.Identity.Name),
            new SqlParameter("@action",4),

            };
            DataTable dt = db.ExecuteSelect("sp_register", para);
            return View(dt.Rows[0]);
            
        }
        [HttpPost,Authorize]
        public ActionResult Changepass(string opass,string npass,string cpass,string email)
        {
            if(npass==cpass)
            {
                SqlParameter[] para = new SqlParameter[] 
                {
            new SqlParameter("@email",User.Identity.Name),
            new SqlParameter("@npass",npass),
            new SqlParameter("@action",5),

                };
                int res = db.ExecuteDML("sp_register",para);
                if (res > 0)
                {
                    return Content("<script>alert('Password changed successfully');location.href='/home/dashboard'</script>");
                  
                }
                else
                {
                    return Content("<script>alert('Error Occured');location.href='/home/changepass'</script>");
                }
            }
            else
            {
                return Content("<script>alert('Confirm password is not same as new password');location.href='/home/changepass'</script>");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}