//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace JamZoo_MarketingTracking.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PromotionLog
    {
        public int PromotionID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public string Platform { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public string FingerPrint { get; set; }
    
        public virtual MarketingEvent MarketingEvent { get; set; }
        public virtual User User { get; set; }
    }
}