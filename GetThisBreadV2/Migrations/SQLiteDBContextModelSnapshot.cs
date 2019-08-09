﻿// <auto-generated />
using GetThisBreadV2.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GetThisBreadV2.Migrations
{
    [DbContext(typeof(SQLiteDBContext))]
    partial class SQLiteDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("GetThisBreadV2.Core.Currency.BreadCoin", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.HasKey("UserId");

                    b.ToTable("BreadCoin");
                });
#pragma warning restore 612, 618
        }
    }
}
