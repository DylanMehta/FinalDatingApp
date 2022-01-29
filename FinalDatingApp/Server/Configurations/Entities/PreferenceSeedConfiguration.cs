using FinalDatingApp.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalDatingApp.Server.Configurations.Entities
{
    public class PreferenceSeedConfiguration : IEntityTypeConfiguration<Preference>
    {
        public void Configure(EntityTypeBuilder<Preference> builder)
        {
            builder.HasData(
              new Preference
              {
                  Id = 1,
                  TargetGender = "Male"
              },
              new Preference
              {
                  Id = 2,
                  TargetGender = "Female"
              }
              );
        }
    }
}
