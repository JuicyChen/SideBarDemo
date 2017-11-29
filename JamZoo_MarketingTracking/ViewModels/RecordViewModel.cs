using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.ViewModels
{
    public class RecordViewModel
    {
        //來自User的屬性欄位
        [DisplayName("會員編號")]
        public int UserID { get; set; }
        [DisplayName("連結來源")]
        public string UserName { get; set; }

        //來自PromotionLog的屬性欄位
        [DisplayName("紀錄編號")]
        public int PromotionID { get; set; }
        [DisplayName("平台")]
        public string Platform { get; set; }
        [DisplayName("點擊時間")]
        public Nullable<System.DateTime> Datetime { get; set; }
        [DisplayName("點擊者FingerPrint")]
        public string FingerPrint { get; set; }

        //來自MarketingEvent的屬性欄位
        [DisplayName("專案/行銷活動編號")]
        public int EventID { get; set; }
        [DisplayName("專案/行銷活動")]
        public string EventName { get; set; }
    }
}