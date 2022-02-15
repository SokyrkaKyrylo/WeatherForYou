﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForYou.Domain.Contexts;

#nullable disable

namespace WeatherForYou.Domain.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220207204310_Add a new property to day")]
    partial class Addanewpropertytoday
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeatherForYou.Domain.Models.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DayNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("WeatherForYou.Domain.Models.MeteorologyData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("DayId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WindSpeed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Meteorologies");
                });

            modelBuilder.Entity("WeatherForYou.Domain.Models.MeteorologyData", b =>
                {
                    b.HasOne("WeatherForYou.Domain.Models.Day", null)
                        .WithMany("MeteorologyData")
                        .HasForeignKey("DayId");
                });

            modelBuilder.Entity("WeatherForYou.Domain.Models.Day", b =>
                {
                    b.Navigation("MeteorologyData");
                });
#pragma warning restore 612, 618
        }
    }
}
