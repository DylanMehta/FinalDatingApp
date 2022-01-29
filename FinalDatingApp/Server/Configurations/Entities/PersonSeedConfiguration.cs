using FinalDatingApp.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalDatingApp.Server.Configurations.Entities
{
    public class PersonSeedConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Dylan",
                    LastName = "Mehta",
                    Gender = "Female",
                    Age = 19
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Clarence",
                    LastName = "Chew",
                    Gender = "Male",
                    Age = 19
                }
                );
        }
    }
}
