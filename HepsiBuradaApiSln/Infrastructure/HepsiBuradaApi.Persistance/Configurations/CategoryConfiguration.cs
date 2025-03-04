using Bogus.DataSets;
using HepsiBuradaApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Persistance.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        Category category1 = new Category()
        {
            Id = 1,
            Name = "Elektronik",
            Priorty = 1,
            ParentId = 0,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        Category category2 = new Category()
        {
            Id = 2,
            Name = "Moda",
            Priorty = 1,
            ParentId = 0,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        Category parents1 = new Category()
        {
            Id = 3,
            Name = "Bilgisayar",
            Priorty = 1,
            ParentId = 1,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        Category parents2 = new Category()
        {
            Id = 4,
            Name = "Kadın",
            Priorty = 1,
            ParentId = 2,
            IsDeleted = false,
            CreatedDate = DateTime.Now
        };
        builder.HasData(category1,category2,parents1,parents2);

    }
}
