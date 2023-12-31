﻿// <auto-generated />
using System;
using CloudWeather.Report.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CloudWeather.Report.DataAccess.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20230918164633_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CloudWeather.Report.DataAccess.ReportsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<decimal>("AverageHighF")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("AverageLowF")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("RainfallTotalInches")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("SnowTotalInches")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
