﻿// <auto-generated />
using System;
using CloudWeather.Temperature.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudWeather.Temperature.DataAccess.Migrations
{
    [DbContext(typeof(TempDbContext))]
    partial class TempDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CloudWeather.Temperature.DataAccess.TemperatureModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("TempHighF")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("TempLowF")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TemperatureTable");
                });
#pragma warning restore 612, 618
        }
    }
}