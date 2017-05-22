using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class KeyValues
    {
        public static List<KeyValue> GetKeyValues()
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    return context.KeyValue.Where(x => x.Status == true).ToList();
                }
            }
            catch
            {
                return new List<KeyValue>();
            }
        }

    }
}