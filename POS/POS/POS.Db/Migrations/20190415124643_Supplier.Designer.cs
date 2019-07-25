﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POS.Db.Db;

namespace POS.Db.Migrations
{
    [DbContext(typeof(PosContext))]
    [Migration("20190415124643_Supplier")]
    partial class Supplier
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview3.19153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("POS.Model.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<double>("PurchasePrice");

                    b.Property<int>("Quantity");

                    b.Property<double>("SalePrice");

                    b.Property<string>("UPC")
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("UPC")
                        .IsUnique()
                        .HasFilter("[UPC] IS NOT NULL");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("POS.Model.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("Contact")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Supplier");
                });
#pragma warning restore 612, 618
        }
    }
}