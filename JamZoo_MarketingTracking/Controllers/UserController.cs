using JamZoo_MarketingTracking.Entities;
using JamZoo_MarketingTracking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamZoo_MarketingTracking.Controllers
{
    public class UserController : Controller
    {

        JamZooMETrackingSystemEntities DbEntity = new JamZooMETrackingSystemEntities();   //Controller中建立Entity實體

        public ActionResult UserIndex()
        {
            if (Request.Cookies["UserID"] != null)
            {
                int id = int.Parse(Request.Cookies["UserID"].Value);

                var result = from user_marketingevent in DbEntity.User_MarketingEvent.ToList()
                             join user in DbEntity.User.ToList() on user_marketingevent.UserID equals user.UserID
                             //join marketingevent in DbEntity.MarketingEvent.ToList() on user_marketingevent.EventID equals marketingevent.EventID
                             where user.UserID == id
                             select new UserViewModel
                             {
                                 UserName = user.UserName,
                                 Phone = user.Phone,
                                 Email = user.Email,
                                 UserAppLink = user_marketingevent.UserAppLink,
                                 UserAndroidUrl = user_marketingevent.UserAndroidUrl /*+ "?eventId=" + Request.Cookies["eventId"]*/,
                                 UserAndroidCount = user_marketingevent.UserAndroidCount,
                                 UserIosUrl = user_marketingevent.UserIosUrl,
                                 UserIosCount = user_marketingevent.UserIosCount
                             };

                return View(result.ToList().First());
            }
            else
            {
                return RedirectToAction("WhereIsPoliceHome", "Home");
            }
        }

    }
}