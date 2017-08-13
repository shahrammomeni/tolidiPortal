using Gurock.SmartInspect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace tpr.Controllers
{
    public class logerHelper
    {
        //===============================================
        public static void LogException(Exception e)
        {
            try
            {
                var p = System.Web.Hosting.HostingEnvironment.MapPath("~/log/mvc.tpr.log.sil");
                SiAuto.Si.Connections = "file(filename=" + p + ",append=true)";
                SiAuto.Si.Enabled = true;
                SiAuto.Main.LogException(e);
            }
            catch (Exception ex)
            {

            }
        }

        public static void LogMsg(string msg)
        {
            try
            {
                var p = System.Web.Hosting.HostingEnvironment.MapPath("~/log/mvc.tpr.log.sil");
                SiAuto.Si.Connections = "file(filename=" + p + ",append=true)";
                SiAuto.Si.Enabled = true;
                SiAuto.Main.LogString("Log", msg);              
            }
            catch (Exception ex)
            {
                logerHelper.LogException(ex);
                //return ex.Message;
            }

        }

        //=================================================
    }
}