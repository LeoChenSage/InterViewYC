using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models.Container;

namespace InterViewModel
{
    public partial class AdminAccount
    {

        /// <summary>
        /// 新增後台帳號
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public static bool Insert(Interview.Models.Container.AdminAccount userAccount)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    context.AdminAccount.Add(userAccount);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        /// <summary>
        /// 檢查後台帳號登入
        /// </summary>
        /// <param name="account">帳號</param>
        /// <param name="password">密碼</param>
        /// <param name="isLoginAction">是否為登入動作</param>
        /// <returns></returns>
        public static Interview.Models.Container.AdminAccount GetAccount(string account, string password, bool isLoginAction)
        {
            try
            {
                using (var context = new HouseRulesEntities())
                {
                    var encryptPassword = Utility.SiteSecurity.Encrypt(password);
                    if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(encryptPassword))
                        return null;

                    var query = context.AdminAccount.FirstOrDefault(x => x.Account == account && x.Password == encryptPassword);
                    if (query != null)
                    {
                        return query;
                    }

                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}