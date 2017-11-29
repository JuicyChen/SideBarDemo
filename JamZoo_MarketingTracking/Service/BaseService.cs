using JamZoo_MarketingTracking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JamZoo_MarketingTracking.Service
{
    public class BaseService
    {
        public JamZooMETrackingSystemEntities DbEntity;

        public BaseService()
        {
            DbEntity = new JamZooMETrackingSystemEntities();
        }

        //public static string Domain = System.Web.Configuration.WebConfigurationManager.AppSettings["Domain"];
    }
}