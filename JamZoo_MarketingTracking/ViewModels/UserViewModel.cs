using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.ViewModels
{
    public class UserViewModel
    {
        //來自User的屬性欄位
        [DisplayName("會員編號")]
        public int UserID { get; set; }
        [DisplayName("會員姓名")]
        public string UserName { get; set; }
        [DisplayName("手機")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("註冊時間")]
        public Nullable<System.DateTime> CreateDate { get; set; }

        //來自PromotionLog的屬性欄位
        [DisplayName("點擊時間")]
        public Nullable<System.DateTime> Datetime { get; set; }
        [DisplayName("點擊IP")]
        public string IP { get; set; }

        //來自User_MarketingEvent的屬性欄位
        //public string EventName { get; set; }

        //來自User_MarketingEvent的屬性欄位
        [DisplayName("專屬推廣連結")]
        public string UserAppLink { get; set; }
        [DisplayName("推廣連結(Android)")]
        public string UserAndroidUrl { get; set; }
        [DisplayName("被點擊次數(Android)")]
        public Nullable<int> UserAndroidCount { get; set; }
        [DisplayName("推廣連結(IOS)")]
        public string UserIosUrl { get; set; }
        [DisplayName("被點擊次數(IOS)")]
        public Nullable<int> UserIosCount { get; set; }

    }
}