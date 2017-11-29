using JamZoo_MarketingTracking.Entities;
using JamZoo_MarketingTracking.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JamZoo_MarketingTracking.Controllers
{
    public enum EventName
    {
        police = 1,
        event2 = 2
    }

    public class AppLinkController : Controller
    {
        readonly FingerPrintService fingerPrintService;

        public AppLinkController()
        {
            fingerPrintService = new FingerPrintService();
        }

        JamZooMETrackingSystemEntities DbEntity = new JamZooMETrackingSystemEntities();   //Controller中建立Entity實體

        //各專案在Table Event中的EventID
        int WhereIsPoliceID = 1;  //推廣專案，警察在哪裡的EventID=1 

        public ActionResult RedirectLink(int id = 1)
        {
            Response.Cookies["UserID"].Value = id.ToString();
            ViewBag.UserID = id;

            if (Request.Headers["User-Agent"].Contains("Android"))
            {
                return RedirectToAction("AndriodWhereIsThePolice", "AppLink");
            }
            else if (Request.Headers["User-Agent"].Contains("iPhone") || Request.Headers["User-Agent"].Contains("iPad"))
            {
                return RedirectToAction("IosWhereIsPolice", "AppLink");
            }
            else
            {
                return View(ViewBag.UserID);   //本機Demo測試用
                //return RedirectToAction("WebTestWhereIsPolice", "AppLink"); //正式上線後啟用，點擊者若是透過PC進入，直接轉址至google商店(但不寫入推廣紀錄)
            }
        }

        public ActionResult AndriodWhereIsThePolice(int id = 1)
        {
            id = int.Parse(Request.Cookies["UserID"].Value);
            ViewBag.UserID = id;
            return View(ViewBag.UserID);
        }

        public ActionResult IosWhereIsPolice(int id = 1)
        {
            id = int.Parse(Request.Cookies["UserID"].Value);
            ViewBag.UserID = id;
            return View(ViewBag.UserID);
        }


        public ActionResult WebTestWhereIsPolice()
        {
            return View();
        }


        static string errorMsg = string.Empty;
        [HttpPost]
        public ActionResult AddToAndroid(PromotionLog data, int id = 1)
        {
            id = int.Parse(Request.Cookies["UserID"].Value);
            if (ModelState.IsValid)
            {
                try
                {
                fingerPrintService.AddToAndroid(data, out errorMsg, id);
                }
                catch
                {
                    errorMsg = "Json格式錯誤";
                }
            }
            else
            {
                errorMsg = ModelState.Values.FirstOrDefault(p => p.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage;
            }
            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult AddToIos(PromotionLog data, int id = 1)
        {
            id = int.Parse(Request.Cookies["UserID"].Value);
            if (ModelState.IsValid)
            {
                try
                {
                    fingerPrintService.AddToIos(data, out errorMsg, id);
                }
                catch
                {
                    errorMsg = "Json格式錯誤";
                }
            }
            else
            {
                errorMsg = ModelState.Values.FirstOrDefault(p => p.Errors.Count > 0)?.Errors.FirstOrDefault()?.ErrorMessage;
            }
            return new EmptyResult();
        }

    }
}