﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SvenePrøveProjekt.Database;

#nullable disable

namespace SvenePrøveProjekt.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240621085125_initialCreate")]
    partial class initialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SvenePrøveProjekt.Models.City", b =>
                {
                    b.Property<int>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityID"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ZIPCode")
                        .HasColumnType("int");

                    b.HasKey("CityID");

                    b.HasIndex("UserID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Login", b =>
                {
                    b.Property<int>("LoginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("LoginID");

                    b.HasIndex("RoleId");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.ProductList", b =>
                {
                    b.Property<int>("ProductOrderListID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductOrderListID"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductOrderListID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductList");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LoginId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleTypeRoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("LoginId")
                        .IsUnique()
                        .HasFilter("[LoginId] IS NOT NULL");

                    b.HasIndex("RoleTypeRoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.City", b =>
                {
                    b.HasOne("SvenePrøveProjekt.Models.User", null)
                        .WithMany("Cities")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Login", b =>
                {
                    b.HasOne("SvenePrøveProjekt.Models.Role", "RoleType")
                        .WithMany("logins")
                        .HasForeignKey("RoleId");

                    b.Navigation("RoleType");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Order", b =>
                {
                    b.HasOne("SvenePrøveProjekt.Models.User", "user")
                        .WithMany("order")
                        .HasForeignKey("UserId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.ProductList", b =>
                {
                    b.HasOne("SvenePrøveProjekt.Models.Order", "Orders")
                        .WithMany("orderlists")
                        .HasForeignKey("OrderId");

                    b.HasOne("SvenePrøveProjekt.Models.Product", "Products")
                        .WithMany("orderlists")
                        .HasForeignKey("ProductId");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.User", b =>
                {
                    b.HasOne("SvenePrøveProjekt.Models.Login", "login")
                        .WithOne("Users")
                        .HasForeignKey("SvenePrøveProjekt.Models.User", "LoginId");

                    b.HasOne("SvenePrøveProjekt.Models.Role", "RoleType")
                        .WithMany()
                        .HasForeignKey("RoleTypeRoleID");

                    b.Navigation("RoleType");

                    b.Navigation("login");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Login", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Order", b =>
                {
                    b.Navigation("orderlists");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Product", b =>
                {
                    b.Navigation("orderlists");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.Role", b =>
                {
                    b.Navigation("logins");
                });

            modelBuilder.Entity("SvenePrøveProjekt.Models.User", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("order");
                });
#pragma warning restore 612, 618
        }
    }
}
