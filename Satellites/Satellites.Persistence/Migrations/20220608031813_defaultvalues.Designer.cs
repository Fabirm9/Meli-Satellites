﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Satellites.Persistence;

namespace Satellites.Persistence.Migrations
{
    [DbContext(typeof(SatelliteContext))]
    [Migration("20220608031813_defaultvalues")]
    partial class defaultvalues
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Satellites.Core.Models.Satellite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("Distance")
                        .HasColumnType("real");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Satellites");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Message = "",
                            Name = "kenobi",
                            Position = "-500,-200"
                        },
                        new
                        {
                            Id = 2,
                            Message = "",
                            Name = "skywalker",
                            Position = "100,-100"
                        },
                        new
                        {
                            Id = 3,
                            Message = "",
                            Name = "sato",
                            Position = "500,100"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}