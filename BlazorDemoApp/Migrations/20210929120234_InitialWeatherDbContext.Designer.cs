﻿// <auto-generated />
using System;
using BlazorDemoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorDemoApp.Migrations
{
    [DbContext(typeof(WeatherDbContext))]
    [Migration("20210929120234_InitialWeatherDbContext")]
    partial class InitialWeatherDbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlazorDemoApp.Data.WeatherData", b =>
                {
                    b.Property<int>("Weather_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvgHumidity")
                        .HasColumnType("int");

                    b.Property<int>("AvgPressure")
                        .HasColumnType("int");

                    b.Property<int>("AvgWind")
                        .HasColumnType("int");

                    b.Property<string>("Condition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxTemp")
                        .HasColumnType("int");

                    b.Property<int>("MinTemp")
                        .HasColumnType("int");

                    b.HasKey("Weather_ID");

                    b.ToTable("weatherData");
                });
#pragma warning restore 612, 618
        }
    }
}
