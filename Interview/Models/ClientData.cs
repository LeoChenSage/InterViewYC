using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class ClientData
    {
        /// <summary>
        /// 抓所有預約資訊
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 抓指定預約資訊
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 更新預約資訊
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
        public static bool Update(Interview.Models.Container.ClientData clientData)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    //var item = context.ClientData.FirstOrDefault(x => x.ID == clientData.ID);
                    var item = context.ClientData.FirstOrDefault(x => x.ID == clientData.ID);

                    if (item == null)
                        return false;

                    item.ClientName = clientData.ClientName;
                    item.Sex = clientData.Sex;
                    item.City = clientData.City;
                    item.Address = clientData.Address;
                    item.Mobile = clientData.Mobile;
                    item.Budget = clientData.Budget;
                    item.UpdateDate = DateTime.Now;

                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 新增預約資訊
        /// </summary>
        /// <param name="clientData"></param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新上下架狀態
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool EditStatus(int id, int status)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    var clientData = context.ClientData.FirstOrDefault(x => x.ID == id);

                    if (clientData == null)
                        return false;

                    clientData.UpdateDate = DateTime.Now;
                    clientData.Status = status;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}