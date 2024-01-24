using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class HolidayDBInitializer : CreateDatabaseIfNotExists<HolidayContext>
    {
        protected override void Seed(HolidayContext db)
        {
            base.Seed(db);
        }
    }
}