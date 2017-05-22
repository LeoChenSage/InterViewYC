using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class KeyValues
    {
        /// <summary>
        /// 抓全部的keyvalue
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 抓指定的keyvalue
        /// </summary>
        /// <param name="keyId"></param>
        /// <returns></returns>
        public static KeyValue GetKeyValueById(int keyId)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    return context.KeyValue.FirstOrDefault(x => x.Status == true && x.KeyID == keyId);
                }
            }
            catch
            {
                return new KeyValue();
            }
        }

    }
}