using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OA.Data.Models;

namespace OA.Data.Maps
{
    public class AspNetUserMap
    {
        public AspNetUserMap(EntityTypeBuilder<AspNetUser> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(x => x.UserName).HasMaxLength(10);
        }
    }
}
