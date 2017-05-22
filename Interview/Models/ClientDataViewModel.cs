using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterViewModel.ViewModel
{
    public class ClientDataViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }

        public int Sex { get; set; }

        public string SexType
        {
            get
            {
                return this.Sex == 0 ? "女性" : "男性";
            }
        }

        public int City { get; set; }

        public string Address { get; set; }

        public string FullAddress
        {
            get
            {
                var city = KeyValues.GetKeyValueById(this.City);
                return city.KeyName + Address;
            }
        }

        public string Mobile { get; set; }

        public string Budget { get; set; }

        public string BudgetDollar
        {
            get
            {
                return this.Budget + "萬元";
            }
        }
    }
}