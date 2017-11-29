using JamZoo_MarketingTracking.Entities;
using JamZoo_MarketingTracking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace JamZoo_MarketingTracking.Controllers
{
    public class AdminController : Controller     //JamZoo後台管理員的功能
    {
        JamZooMETrackingSystemEntities DbEntity = new JamZooMETrackingSystemEntities();   //Controller中建立Entity實體

        // GET: Admin
        [HttpGet]
        public ActionResult AdminLogin()     //Get方法:管理員登入頁面  帳號:Admin  密碼:Admin
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(AdminViewModel adminViewModel)  //Post:管理員登入驗證
        {
            if (ModelState.IsValid)
            {
                var adminLogin = DbEntity.JamZooAdmin.FirstOrDefault(admin => admin.AdminName == adminViewModel.AdminName && admin.AdminPassword == adminViewModel.AdminPassword);
                if (adminLogin != null)       //判斷資料庫中 管理者的帳號/密碼是否正確
                {
                    Response.Cookies["AdminID"].Value = adminLogin.AdminID.ToString();
                    Response.Cookies["AdminName"].Value = HttpUtility.UrlEncode(adminLogin.AdminName);

                    if (adminViewModel.RememberMe)  //若有登入時有勾選[保持登入狀態]，利用保存Cookies，狀態保留三天
                    {
                        Response.Cookies["AdminID"].Expires = DateTime.Now.AddDays(3);
                        Response.Cookies["AdminName"].Expires = DateTime.Now.AddDays(3);
                    }
                }
                return RedirectToAction("UserList_All", "Admin");
            }
            //adminViewModel.ad
            ViewBag.ErrorMessage = "帳號/密碼錯誤";
            return View();
        }

        public ActionResult AdminLogout()
        {
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["AdminID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["AdminName"].Expires = DateTime.Now.AddDays(-1);
            Session.Abandon();
            return RedirectToAction("AdminLogin", "Admin");
        }

        public ActionResult UserList_All(int page = 1, int perPage = 30, string sortBy = "", string KeyWordSearch = "")   //登入成功→觀看:所有活動的已註冊會員
        {
            if (Request.Cookies["AdminID"] == null)       //若Cookies中找不到AdminID，跳轉回管理者登入介面
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                var user = new List<User>();
                var userList = DbEntity.User.ToList();
                string strKeyWord = string.IsNullOrEmpty(KeyWordSearch) ? "" : KeyWordSearch;

                if (strKeyWord == "")
                {
                    user = userList;
                }
                else
                {
                    user = userList.Select(u => u).Where(u => u.UserName.Contains(strKeyWord) || u.Phone.Contains(strKeyWord) || u.Email.Contains(strKeyWord)).ToList();
                }

                ViewBag.SortByUserName = (sortBy == "UserName") ? "UserName desc" : "UserName";
                ViewBag.SortByPhone = (sortBy == "Phone") ? "Phone desc" : "Phone";
                ViewBag.SortByEmail = (sortBy == "Email") ? "Email desc" : "Email";
                ViewBag.SortByCreateTime = (sortBy == "CreateTime") ? "CreateTime desc" : "CreateTime";

                switch(sortBy)
                {
                    case "UserName desc":
                        user = user.OrderByDescending(u => u.UserName).ToList();
                        break;
                    case "UserName":
                        user = user.OrderBy(u => u.UserName).ToList();
                        break;
                    case "Phone desc":
                        user = user.OrderByDescending(u => u.Phone).ToList();
                        break;
                    case "Phone":
                        user = user.OrderBy(u => u.Phone).ToList();
                        break;
                    case "Email desc":
                        user = user.OrderByDescending(u => u.Email).ToList();
                        break;
                    case "Email":
                        user = user.OrderBy(u => u.Email).ToList();
                        break;
                    case "CreateTime desc":
                        user = user.OrderByDescending(u => u.UserID).ToList();
                        break;
                    case "CreateTime":
                        user = user.OrderBy(u => u.UserID).ToList();
                        break;
                }

                return View(user.ToPagedList(page,perPage));
            }
        }

        public ActionResult RecordList_All(int page = 1, int perPage = 30, string sortBy = "", string KeyWordSearch = "") //觀看:所有推廣的連結紀錄
        {
            if (Request.Cookies["AdminID"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            else
            {
                string strKeyWord = string.IsNullOrEmpty(KeyWordSearch) ? "" : KeyWordSearch;

                var result = from record in DbEntity.PromotionLog.ToList()
                             join user in DbEntity.User.ToList() on record.UserID equals user.UserID
                             join marketingEvent in DbEntity.MarketingEvent.ToList() on record.EventID equals marketingEvent.EventID
                             orderby record.PromotionID descending
                             select new RecordViewModel
                             {
                                 PromotionID = record.PromotionID,
                                 EventID = marketingEvent.EventID,
                                 EventName = marketingEvent.EventName,
                                 UserID = user.UserID,
                                 UserName = user.UserName,
                                 Platform = record.Platform,
                                 Datetime =record.Datetime,
                                 FingerPrint = record.FingerPrint
                             };
                if (strKeyWord != "")
                {
                    result = result.Select(r => r).Where(r => r.EventName.Contains(strKeyWord) || r.UserName.Contains(strKeyWord) || r.Platform.Contains(strKeyWord) || r.FingerPrint !=null &&r.FingerPrint.Contains(strKeyWord)).ToList();
                }

                //搜尋字串
                ViewBag.SreachbyKeyWord = strKeyWord;
                //排序方法
                ViewBag.SortByEventName = (sortBy == "EventName") ? "EventName desc" : "EventName";
                ViewBag.SortByUserName = (sortBy == "UserName") ? "UserName desc" : "UserName";
                ViewBag.SortByPlatform = (sortBy == "Platform") ? "Platform desc" : "Platform";
                ViewBag.SortByClickTime = (sortBy == "ClickTime") ? "ClickTime desc" : "ClickTime";
                ViewBag.SortByFingerPrint = (sortBy == "FingerPrint") ? "FingerPrint desc" : "FingerPrint";

                switch (sortBy)
                {
                    case "EventName desc":
                        result = result.OrderByDescending(r => r.EventName);
                        break;
                    case "EventName":
                        result = result.OrderBy(r => r.EventName);
                        break;
                    case "UserName desc":
                        result = result.OrderByDescending(r => r.UserName);
                        break;
                    case "UserName":
                        result = result.OrderBy(r => r.UserName);

                        break;
                    case "Platform desc":
                        result = result.OrderByDescending(r => r.Platform);
                        break;
                    case "Platform":
                        result = result.OrderBy(r => r.Platform);
                        break;
                    case "ClickTime desc":
                        result = result.OrderByDescending(r => r.Datetime);
                        break;
                    case "ClickTime":
                        result = result.OrderBy(r => r.Datetime);
                        break;
                    case "FingerPrint desc":
                        result = result.OrderByDescending(r => r.FingerPrint);
                        break;
                    case "FingerPrint":
                        result = result.OrderBy(r => r.FingerPrint);
                        break;
                }
                return View(result.ToList().ToPagedList(page,perPage));
            }
        }

        public ActionResult EventCount()  //觀看:單項產品的推廣次數(目前:警察在哪裡)
        {
            if (Request.Cookies["AdminID"] == null)
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            var result = from user_event in DbEntity.User_MarketingEvent.ToList()
                         join user in DbEntity.User.ToList() on user_event.UserID equals user.UserID
                         orderby user_event.UserAndroidCount descending
                         select new UserViewModel
                         {
                             UserName = user.UserName,
                             Phone = user.Phone,
                             Email = user.Email,
                             UserAndroidCount = user_event.UserAndroidCount,
                             UserIosCount = user_event.UserIosCount
                         };
            return View(result.ToList());

        }

    }
}