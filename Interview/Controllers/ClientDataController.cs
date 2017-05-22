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
            string errorMsg = string.Empty;
            if (string.IsNullOrWhiteSpace(clientData.ClientName))
            {
                errorMsg += "請填客戶姓名、";
            }

            if (string.IsNullOrWhiteSpace(clientData.Sex.ToString()))
            {
                errorMsg += "請選擇客戶性別、";
            }

            if (string.IsNullOrWhiteSpace(clientData.City.ToString()))
            {
                errorMsg += "請選擇縣市、";
            }

            if (string.IsNullOrWhiteSpace(clientData.Address))
            {
                errorMsg += "請輸入地址、";
            }

            if (string.IsNullOrWhiteSpace(clientData.Budget))
            {
                errorMsg += "請輸入預算、";
            }

            if (string.IsNullOrWhiteSpace(clientData.Mobile))
            {
                errorMsg += "請輸入手機號碼、";
            }

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                TempData["Message"] = errorMsg.Trim('、');
                return RedirectToAction("Edit");
            }


            string result = string.Empty;
            try
            {
                if (editType == "新增")
                {
                    if (ClientData.Insert(clientData))
                        result = "新增成功";
                    else
                        result = "新增失敗";
                }
                else
                {
                    if (ClientData.Update(clientData))
                        result = "編輯成功";
                    else
                        result = "編輯失敗";
                }
                TempData["Message"] = result;
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
                    result = "刪除成功";
                else
                    result = "刪除失敗";
                TempData["Message"] = result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
