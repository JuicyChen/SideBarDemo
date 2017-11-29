using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.Entities  //宣告partial class，命名空間需要和資料模型實體的命名空間相同
{
    [MetadataType(typeof(EventMetaData))]
    public partial class MarketingEvent
    {
        private class EventMetaData
        {
            [DisplayName("專案/行銷活動編號")]
            public int EventID { get; set; }
            [DisplayName("專案/行銷活動")]
            public string EventName { get; set; }
            [DisplayName("Android連結")]
            public string AndroidUrl { get; set; }
            [DisplayName("Android點擊次數")]
            public Nullable<int> AndroidCount { get; set; }
            [DisplayName("Ios連結")]
            public string IosUrl { get; set; }
            [DisplayName("Ios點擊次數")]
            public Nullable<int> IosCount { get; set; }
        }
    }
}