using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.IO;
using NPOI.HSSF.UserModel;
using System.ComponentModel;
using System.Text;
using InterViewModel;

namespace Utility
{

    #region 加解密
    /// <summary>
    /// 加解密
    /// </summary>
    public class SiteSecurity
    {
        private const string _key = "5678iknbvde4560okmn23rtyjklijfj5";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>when exception will return empty string</returns>
        public static string Encrypt(string value)
        {
            try
            {
                Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(_key);
                Byte[] toEncryptArray = System.Text.UTF8Encoding.UTF8.GetBytes(value);

                System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
                rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>when exception will return empty string</returns>
        public static string Decrypt(string value)
        {
            try
            {
                Byte[] keyArray = System.Text.UTF8Encoding.UTF8.GetBytes(_key);
                Byte[] toEncryptArray = Convert.FromBase64String(value);

                System.Security.Cryptography.RijndaelManaged rDel = new System.Security.Cryptography.RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = System.Security.Cryptography.CipherMode.ECB;
                rDel.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

                System.Security.Cryptography.ICryptoTransform cTransform = rDel.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return System.Text.UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
    #endregion

    #region List to Excel下載
    /// <summary>
    /// IList 擴充方法
    /// </summary>
    public static class IListExtension
    {
        /// <summary>
        /// 匯出Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static MemoryStream ExportExcel<T>(this IList<T> list)
        {
            HSSFWorkbook workBook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workBook.CreateSheet();
            HSSFRow headRow = (HSSFRow)sheet.CreateRow(0);
            HSSFCellStyle style = (HSSFCellStyle)workBook.CreateCellStyle();
            style.WrapText = true;
            style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Top;

            int columnIndex = 0;
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            //標題
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                if (!prop.Attributes.OfType<ExportIgnoreAttribute>().Any())
                {
                    headRow.CreateCell(columnIndex).SetCellValue(props[i].DisplayName);
                    sheet.SetColumnWidth(columnIndex, 18 * 256);
                    columnIndex++;
                }
            }


            //資料
            for (var i = 0; i < list.Count; i++)
            {
                var row = (HSSFRow)sheet.CreateRow(i + 1);
                row.HeightInPoints = 25;
                columnIndex = 0;
                for (var j = 0; j < props.Count; j++)
                {
                    PropertyDescriptor prop = props[j];
                    if (!prop.Attributes.OfType<ExportIgnoreAttribute>().Any())
                    {
                        var item = prop.GetValue(list[i]);
                        row.CreateCell(columnIndex).SetCellValue(item == null ? string.Empty : item.ToString());
                        row.Cells[columnIndex].CellStyle = style;
                        columnIndex++;
                    }
                }
            }

            workBook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headRow = null;
            workBook = null;
            return ms;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ExportIgnoreAttribute : System.Attribute
    {

    }
    #endregion

    #region 目前登入的網站使用者
    /// <summary>
    /// 目前登入的網站使用者
    /// </summary>
    public class CurrentUser
    {
        #region 屬性
        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; private set; }
        /// <summary>
        /// 是否登入
        /// </summary>
        public bool IsSignIn { get; private set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string Name { get; set; }
        public List<int> RoleIDs { get; private set; }
        #endregion

        #region 登入
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <returns></returns>
        public static CurrentUser SignIn(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
                return null;


            var user = AdminAccount.GetAccount(account, password, true);
            if (user != null && user.Status > 0)
            {
                return new CurrentUser
                {
                    Account = user.Account,
                    Name = user.AdminName,
                    IsSignIn = true
                };
            }

            return null;
        }

        /// <summary>
        /// 取得目前的使用者及設定角色
        /// </summary>
        /// <returns></returns>
        public static CurrentUser Get()
        {
            FormsIdentity identity = HttpContext.Current.User.Identity as FormsIdentity;
            CurrentUser res = new CurrentUser();
            if (identity != null)
            {
                var info = identity.Name.Split(',');
                res.Account = info[0];
                res.Name = info[1];
                res.IsSignIn = true;
            }
            return res;
        }

        /// <summary>
        /// 取得目前使用者的名稱
        /// </summary>
        /// <returns></returns>
        public static string GetName()
        {
            return Get().Name;
        }
        #endregion
    }
    #endregion

}