

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWebDal.Models.CalendarModel;


namespace SimpleWebDal.Configuration
{
    public class CalendarActivityConfiguration : IEntityTypeConfiguration<CalendarActivity>
    {
        public void Configure(EntityTypeBuilder<CalendarActivity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Activities);
        }
    }
}