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
            var item = ClientData.GetClients();
            return View(item);
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
        public ActionResult Edit(Interview.Models.Container.ClientData clientData)
        {
            string result = string.Empty;
            try
            {
                if (ClientData.Insert(clientData))
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


        public ActionResult Delete(int id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
