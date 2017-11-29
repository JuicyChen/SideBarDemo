using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.Entities
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        private class UserMetaData
        {
            [DisplayName("會員編號")]
            public int UserID { get; set; }
            [DisplayName("會員姓名")]
            [Required(ErrorMessage = "姓名為必填欄位")]    //必填欄位&出現訊息
            public string UserName { get; set; }
            [DisplayName("手機")]
            [Required(ErrorMessage = "手機為必填欄位")]    //必填欄位&出現訊息
            public string Phone { get; set; }
            [DisplayName("Email")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [DisplayName("註冊時間")]
            public Nullable<System.DateTime> CreateDate { get; set; }
        }
    }
}