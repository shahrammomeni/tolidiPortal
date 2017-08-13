using Gurock.SmartInspect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;
using DevExpress.Web;
using pres = Resources.Resource;
using tpr.Models;

namespace tpr.Controllers
{
    public class ctrlHelper
    {
        public static T1 mixObj<T1, T2>(T1 table, T2 vu)
        {
            try
            {
                var pinfo = table.GetType().GetProperties();
                foreach (var p in pinfo)
                {
                    try
                    {
                        //if (p.PropertyType.IsPrimitive) continue;
                        Type ttype = p.PropertyType;
                        var val = vu.GetType().GetProperty(p.Name).GetValue(vu, null);
                        try
                        {
                            if (ttype == typeof(DateTime))
                                p.SetValue(table, DateTime.Parse(val.ToString().Trim()));
                            else
                                p.SetValue(table, val);
                        }
                        catch { }
                    }
                    catch (Exception ex)
                    {
                        logerHelper.LogException(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
            }
            return table;
        }
        public static AspNetUser getUser(string userName)
        {
            try
            {
                var db = new tprGeneralDBContext();
                var usr = db.AspNetUsers
                        .Where(x => x.UserName == userName)
                   .ToList();
                if (usr.Count == 0) return new AspNetUser();
                return usr.First();
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return new AspNetUser();
            }
        }
        public static IEnumerable getRoles()
        {
            var db = new tprGeneralDBContext();
            return (from x in db.AspNetRoles
                    select new
                    {
                        RoleId = x.Id,
                        roleName = x.Name
                    })
                    .ToList();
        }
        //==================================================================
        public static bool dublicateEmail(string email)
        {
            try
            {
                using (var dbcode = new tprGeneralDBContext())
                {
                    var f = dbcode.AspNetUsers.Where(x => x.Email.Trim() == email).ToList();
                    return f.Count() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return true;
            }

        }
        public static bool dublicateMobile(string mobile)
        {
            try
            {
                using (var dbcode = new tprGeneralDBContext())
                {
                    var f = dbcode.AspNetUsers.Where(x => x.Mobile.Trim() == mobile).ToList();
                    return f.Count() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return true;
            }

        }
        public static bool valid(string email)
        {
            try
            {
                tprGeneralDBContext dbcode = new tprGeneralDBContext();
                var f = dbcode.AspNetUsers.Where(x => x.Email.Trim() == email).ToList();
                return f.Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return true;
            }

        }
        public static string fixMobil(string mobile)
        {
            try
            {
                string m = mobile.Replace("+", string.Empty).Replace("00", string.Empty);
                if (!m.StartsWith("0")) m += "0";
                return m;
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return string.Empty;
            }

        }
        public static bool chkMobil(string mobile)
        {
            try
            {
                return (mobile.StartsWith("09") && mobile.Length == 11);
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                return false;
            }

        }
    }
}