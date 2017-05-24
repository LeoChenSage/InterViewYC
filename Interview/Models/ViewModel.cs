using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterViewModel;
using System.ComponentModel;
using Utility;

namespace Interview.ViewModel
{
    public class ReCaptchaResultViewModel
    {
        public bool success { get; set; }
        public string challenge_ts { get; set; }
        public string hostname { get; set; }
    }

    public class ClientDataViewModel
    {
        [ExportIgnore]
        public int Id { get; set; }

        [DisplayName("客戶姓名")]
        public string ClientName { get; set; }

        [ExportIgnore]
        public int Sex { get; set; }

        [DisplayName("性別")]
        public string SexType
        {
            get
            {
                return this.Sex == 0 ? "女性" : "男性";
            }
        }

        [ExportIgnore]
        public int City { get; set; }

        [ExportIgnore]
        public string Address { get; set; }

        [DisplayName("看屋地址")]
        public string FullAddress
        {
            get
            {
                var city = KeyValues.GetKeyValueById(this.City);
                return city.KeyName + Address;
            }
        }
        [DisplayName("客戶手機")]
        public string Mobile { get; set; }

        [ExportIgnore]
        public string Budget { get; set; }

        [DisplayName("客戶預算")]
        public string BudgetDollar
        {
            get
            {
                return this.Budget + "萬元";
            }
        }
    }

}