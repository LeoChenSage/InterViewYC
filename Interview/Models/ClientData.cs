using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class ClientData
    {
        public static List<Interview.Models.Container.ClientData> GetClients()
        {
            try
            {
                var item = new List<Interview.Models.Container.ClientData>();
                using (var context = new HouseRulesEntities())
                {
                    item = context.ClientData.Where(x => x.Status == 1).ToList();
                }
                return item;
            }
            catch
            {
                return new List<Interview.Models.Container.ClientData>();
            }
        }

        public static Interview.Models.Container.ClientData GetClientById(int id)
        {
            try
            {
                var item = new Interview.Models.Container.ClientData();
                using (var context = new HouseRulesEntities())
                {
                    item = context.ClientData.FirstOrDefault(x => x.Status == 1 && x.ID == id);
                }
                return item;
            }
            catch
            {
                return new Interview.Models.Container.ClientData();
            }
        }

        public static bool Update(Interview.Models.Container.ClientData clientData)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    var item = context.ClientData.FirstOrDefault(x => x.ID == clientData.ID);

                    if (item == null)
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
                    clientData.CreateDate = DateTime.Now;
                    clientData.UpdateDate = DateTime.Now;
                    clientData.Status = 1;
                    context.ClientData.Add(clientData);
                    context.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}