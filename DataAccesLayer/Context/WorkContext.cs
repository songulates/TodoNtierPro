using DataAccesLayer.Configurations;
using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Context
{
   public  class WorkContext:DbContext
    {
        //contextin constructurunu oluşturralım, contexti DI aracılığıyla yazalım
        //UI busines görmesi gerekiyor.db context dependenciy busines kaydını businesde yapmak gerekiyo
        //sonra UI user interfacedan businesı çağırmamız gerekiyo.
        //UI doğrudan dataaccesi görmemesi gerekiyor,migrationdan dolayı startupda tanımlamamalıyız.
        //extension metod yazacağız, amaç o klası genişletmek.
        public WorkContext(DbContextOptions<WorkContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkConf());
           //sonra migration yapalım
        }
        public DbSet<Work> Works { get; set; }
    }
}
