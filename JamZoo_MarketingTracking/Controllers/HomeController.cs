using JamZoo_MarketingTracking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace JamZoo_MarketingTracking.Controllers
{
    public class HomeController : Controller
    {

        JamZooMETrackingSystemEntities DbEntity = new JamZooMETrackingSystemEntities();   //Controller中建立Entity實體

        //各專案在Table Event中的EventID
        int WhereIsPoliceID = 1;  //推廣專案，警察在哪裡的EventID=1 

        [HttpPost]
        public ActionResult Register(User user,User_MarketingEvent user_Marketing) //Post:User註冊 若資料驗證無誤，寫入資料庫
        {
            if (ModelState.IsValid)
            {
                user.CreateDate = DateTime.Now;
                DbEntity.User.Add(user);
                DbEntity.SaveChanges();
                Response.Cookies["UserID"].Value = user.UserID.ToString();
                Response.Cookies["UserName"].Value = HttpUtility.UrlEncode(user.UserName);
                Response.Cookies["EventID"].Value = WhereIsPoliceID.ToString();   //暫定寫法(避免使用者自行手動清除瀏覽器的cookies，把EventID清除)
                                                                                                                 //(EventID若為null，直接註冊，無法產生user_Marketing的錯誤)
                user_Marketing.UserID = int.Parse(Response.Cookies["UserID"].Value);
                user_Marketing.EventID = int.Parse(Response.Cookies["EventID"].Value);
                user_Marketing.UserAppLink = "http://localhost:53029/AppLink/RedirectLink/" + Response.Cookies["UserID"].Value;
                user_Marketing.UserAndroidUrl = "http://localhost:53029/AppLink/AndriodWhereIsThePolice/"+ Response.Cookies["UserID"].Value;
                user_Marketing.UserIosUrl = "http://localhost:53029/AppLink/IosWhereIsPolice/" + Response.Cookies["UserID"].Value;
                user_Marketing.UserAndroidCount = 0;
                user_Marketing.UserIosCount = 0;
                DbEntity.User_MarketingEvent.Add(user_Marketing);
                DbEntity.SaveChanges();

                return RedirectToAction("UserIndex", "User");
            }

            return RedirectToAction("WhereIsPoliceHome", "Home");
        }

        [HttpPost]
        public ActionResult Login (User user)
        {
            if(ModelState.IsValid)
            {
                var LoginUser = DbEntity.User.FirstOrDefault(loginUser => loginUser.UserName == user.UserName && loginUser.Phone == user.Phone);
                if(LoginUser!=null)
                {
                    Response.Cookies["UserID"].Value = LoginUser.UserID.ToString();
                    Response.Cookies["UserName"].Value = HttpUtility.UrlEncode(LoginUser.UserName);
                    return RedirectToAction("UserIndex","User");
                }
            }
            ViewBag.ErrorMessage = "帳號/密碼錯誤";
            return RedirectToAction("WhereIsPoliceHome", "Home");
        }

        public ActionResult Logout()
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["AdminID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["AdminName"].Expires = DateTime.Now.AddDays(-1);
            Session.Abandon();
            return RedirectToAction("WhereIsPoliceHome", "Home");
        }




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


        public ActionResult WhereIsPoliceHome()   //首頁:警察在哪裡   EventID=1
        {

            Response.Cookies["EventID"].Value = WhereIsPoliceID.ToString();
            return View();
        }

        public ActionResult Demo1()
        {
            return View();
        }


    }
}