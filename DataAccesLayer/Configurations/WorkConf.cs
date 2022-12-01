using EntitiesLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Configurations
{
    //fluent Api ile ilgili tablo oluşturcaz.
    public class WorkConf : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(x => x.Id);
            //burda propertyler ile ilgili yaz
            builder.Property(x => x.Definition).HasMaxLength(100);
            builder.Property(x => x.Definition).IsRequired(true);
            builder.Property(x => x.IsCompleted).IsRequired(true);
        }
    }
}
