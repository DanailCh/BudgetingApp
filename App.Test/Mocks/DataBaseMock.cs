using HouseholdBudgetingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.Mocks
{
    public static class DataBaseMock
    {
        public static ApplicationDbContext Instance
        {
            get
            {
                var dbContextOptions=new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ApplicationInMemoryDb"+ DateTime.Now.Ticks.ToString()).Options;
                return new ApplicationDbContext(dbContextOptions,false);

            }
        }
    }
}
