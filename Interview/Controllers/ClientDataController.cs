using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InterViewModel;
namespace Interview.Controllers
{
    public class ClientDataController : Controller
    {
        // GET: ClientData
        public ActionResult Index()
        {
            var client = ClientData.GetClients();
            var clientList = new List<InterViewModel.ViewModel.ClientDataViewModel>();
            foreach (var item in client)
            {
                clientList.Add(
                    new InterViewModel.ViewModel.ClientDataViewModel
                    {
                        Id = item.ID,
                        ClientName = item.ClientName,
                        Sex = item.Sex,
                        City = item.City,
                        Address = item.Address,
                        Mobile = item.Mobile,
                        Budget = item.Budget,
                    });
            }

            return View(clientList);
        }

        public ActionResult Edit(int? id)
        {
            var citys = KeyValues.GetKeyValues();
            ViewBag.Citys = citys;
            if (id.HasValue)
            {
                var item = ClientData.GetClientById(id.Value);
                ViewBag.EditMode = "編輯";
                return View(item);
            }
            ViewBag.EditMode = "新增";
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Interview.Models.Container.ClientData clientData, string editType)
        {
            string result = string.Empty;
            try
            {
                if (editType == "新增")
                {
                    if (ClientData.Insert(clientData))
                        result = "成功";
                    else
                        result = "失敗";
                }
                else
                {
                    if (ClientData.Update(clientData))
                        result = "成功";
                    else
                        result = "失敗";
                }
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            string result = string.Empty;
            try
            {
                if (ClientData.EditStatus(id, 0))
                    result = "成功";
                else
                    result = "失敗";
                TempData["Result"] = result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
