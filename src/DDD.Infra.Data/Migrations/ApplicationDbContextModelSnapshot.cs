using System;
using DDD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DDD.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDD.Domain.Models.Customer", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");

                b.Property<DateTime>("BirthDate");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(11);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100);

                b.HasKey("Id");

                b.ToTable("Customers");
            });
        }
    }
}
