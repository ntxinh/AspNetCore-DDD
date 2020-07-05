using System;
using DDD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DDD.Infra.Data.Migrations.EventStoreSql
{
    [DbContext(typeof(EventStoreSqlContext))]
    partial class EventStoreSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DDD.Domain.Core.Events.StoredEvent", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<Guid>("AggregateId");

                b.Property<string>("Data");

                b.Property<string>("MessageType")
                    .HasColumnName("Action")
                    .HasColumnType("varchar(100)");

                b.Property<DateTime>("Timestamp")
                    .HasColumnName("CreationDate");

                b.Property<string>("User");

                b.HasKey("Id");

                b.ToTable("StoredEvent");
            });
        }
    }
}
