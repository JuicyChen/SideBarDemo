using JamZoo_MarketingTracking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.Service
{
    public class FingerPrintService : BaseService
    {
        //各專案在Table Event中的EventID
        int WhereIsPoliceID = 1;  //推廣專案，警察在哪裡的EventID=1 

        public bool AddToAndroid(PromotionLog data,out string errormsg, int id = 1)
        {
            errormsg = string.Empty;
            try
            {
                PromotionLog promotionLog = new PromotionLog
                {
                    UserID = id,
                    EventID = WhereIsPoliceID,
                    Platform = "Andriod",
                    Datetime = DateTime.Now,
                    FingerPrint = data.FingerPrint
                };
                DbEntity.PromotionLog.Add(promotionLog);
                DbEntity.SaveChanges();

                var user_eventByID = DbEntity.User_MarketingEvent.First(user_event => user_event.UserID == id);
                user_eventByID.UserAndroidCount = ++user_eventByID.UserAndroidCount;
                DbEntity.SaveChanges();
            }
            catch(Exception ex)
            {
                errormsg = ex.InnerException?.Message ?? ex.Message;
            }
            return errormsg.Length == 0;
        }

        public bool AddToIos(PromotionLog data, out string errormsg, int id = 1)
        {
            errormsg = string.Empty;
            try
            {
                PromotionLog promotionLog = new PromotionLog
                {
                    UserID = id,
                    EventID = WhereIsPoliceID,
                    Platform = "Ios",
                    Datetime = DateTime.Now,
                    FingerPrint = data.FingerPrint
                };
                DbEntity.PromotionLog.Add(promotionLog);
                DbEntity.SaveChanges();

                var user_eventByID = DbEntity.User_MarketingEvent.First(user_event => user_event.UserID == id);
                user_eventByID.UserIosCount = ++user_eventByID.UserIosCount;
                DbEntity.SaveChanges();
            }
            catch (Exception ex)
            {
                errormsg = ex.InnerException?.Message ?? ex.Message;
            }
            return errormsg.Length == 0;
        }
    }
}