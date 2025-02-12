﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceProduct.Misc;

namespace ServiceProduct.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20211206235730_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceProduct.Model.Product", b =>
                {
                    b.Property<int>("ProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProdName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProdPrice")
                        .HasColumnType("float");

                    b.HasKey("ProdId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProdId = 1,
                            ProdName = "Car",
                            ProdPrice = 300.0
                        },
                        new
                        {
                            ProdId = 2,
                            ProdName = "Smartphone",
                            ProdPrice = 99.989999999999995
                        },
                        new
                        {
                            ProdId = 3,
                            ProdName = "Watch",
                            ProdPrice = 30.5
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
