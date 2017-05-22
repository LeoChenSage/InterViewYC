using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class ClientData
    {
        public static bool Update(Interview.Models.Container.ClientData clientData)
        {
            try
            {
                using(var context = new HouseRulesEntities())
                {
                    var item = context.ClientData.FirstOrDefault(x=>x.ID == clientData.ID);

                    if(item == null)
                        return false;
                    context.Entry(item).CurrentValues.SetValues(item);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Insert(Interview.Models.Container.ClientData clientData)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    context.ClientData.Add(clientData);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool
    }
}