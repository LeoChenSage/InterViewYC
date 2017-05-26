using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InterViewModel;
using Utility;
using System.Text;

namespace Interview.ApiControllers
{
    public class ClientDataApiController : ApiController
    {
        public HttpResponseMessage Get()
        {
            var res = new HttpResponseMessage(HttpStatusCode.OK);
            var jsSerilaizer = new System.Web.Script.Serialization.JavaScriptSerializer();
            res.Content = new StringContent(jsSerilaizer.Serialize(new { status = 0 }));
            res.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var client = ClientData.GetClientsViewModel();

            if (client != null)
            {
                res.Content = new StringContent(jsSerilaizer.Serialize(client), Encoding.UTF8, "application/json");
            }

            return res;
        }

        public IHttpActionResult Post(Interview.Models.Container.ClientData clientData, string editType)
        {
            //前台有檢查但後台還是再檢查一次好了
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
                return Ok(new { status = -1, message = errorMsg.Trim('、') });
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
                return Ok(new { status = 1, message = result });
            }
            catch
            {
                return Ok(new { status = 0, message = "失敗" });
            }
        }

        public IHttpActionResult Delete(int id)
        {
            string result = string.Empty;
            try
            {
                if (ClientData.EditStatus(id, 0))
                    result = "刪除成功";
                else
                    result = "刪除失敗";

                return Ok(new { status = 1, message = result });
            }
            catch
            {
                return Ok(new { status = 0, message = "刪除失敗" });
            }
        }

    }
}
