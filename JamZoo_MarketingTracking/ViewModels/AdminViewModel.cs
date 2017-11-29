using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.ViewModels
{
    public class AdminViewModel
    {
        [DisplayName("管理員帳號")]           //中文化
        [Required(ErrorMessage = "請輸入帳號")]    //必填欄位&出現訊息
        public string AdminName { get; set; }

        [DisplayName("管理員密碼")]     
        [DataType(DataType.Password)]                //欄位形式:密碼 (轉為暗碼)
        [Required(ErrorMessage = "請輸入密碼")]
        public string AdminPassword { get; set; }

        [DisplayName("保持登入狀態")]
        public bool RememberMe { get; set; }     //自訂屬性:是否保留登入狀態
    }
}