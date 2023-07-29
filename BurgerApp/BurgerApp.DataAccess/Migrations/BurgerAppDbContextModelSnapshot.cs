﻿// <auto-generated />
using System;
using BurgerApp.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BurgerApp.DataAccess.Migrations
{
    [DbContext(typeof(BurgerAppDbContext))]
    partial class BurgerAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BurgerApp.Domain.Models.Burger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BurgerSize")
                        .HasColumnType("int");

                    b.Property<bool>("HasFries")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVegan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Burgers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BurgerSize = 2,
                            HasFries = true,
                            IsVegan = false,
                            IsVegetarian = false,
                            Name = "Chicken Burger",
                            Price = 20
                        },
                        new
                        {
                            Id = 2,
                            BurgerSize = 1,
                            HasFries = false,
                            IsVegan = true,
                            IsVegetarian = true,
                            Name = "Veggie Burger",
                            Price = 10
                        },
                        new
                        {
                            Id = 3,
                            BurgerSize = 3,
                            HasFries = true,
                            IsVegan = false,
                            IsVegetarian = false,
                            Name = "Cheeseburger",
                            Price = 35
                        });
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.BurgerOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BurgerId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BurgerId");

                    b.HasIndex("OrderId");

                    b.ToTable("BurgerOrder");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BurgerId = 1,
                            OrderId = 1
                        },
                        new
                        {
                            Id = 2,
                            BurgerId = 3,
                            OrderId = 1
                        },
                        new
                        {
                            Id = 3,
                            BurgerId = 2,
                            OrderId = 2
                        });
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("ClosesAt")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("OpensAt")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Locations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "1371 Queen St W",
                            ClosesAt = new TimeSpan(0, 22, 0, 0, 0),
                            Name = "Quuen St.Burger Shop",
                            OpensAt = new TimeSpan(0, 8, 0, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            Address = "360 Main St",
                            ClosesAt = new TimeSpan(0, 23, 0, 0, 0),
                            Name = "Main St.Burger Shop",
                            OpensAt = new TimeSpan(0, 9, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelivered")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "68 King William St",
                            FullName = "Kennedi Williamson",
                            IsDelivered = false,
                            LocationId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "3550 N Woodlawn St",
                            FullName = "Kendra Heathcote",
                            IsDelivered = true,
                            LocationId = 2
                        });
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.BurgerOrder", b =>
                {
                    b.HasOne("BurgerApp.Domain.Models.Burger", "Burger")
                        .WithMany("BurgerOrders")
                        .HasForeignKey("BurgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BurgerApp.Domain.Models.Order", "Order")
                        .WithMany("BurgerOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Burger");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.Order", b =>
                {
                    b.HasOne("BurgerApp.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.Burger", b =>
                {
                    b.Navigation("BurgerOrders");
                });

            modelBuilder.Entity("BurgerApp.Domain.Models.Order", b =>
                {
                    b.Navigation("BurgerOrders");
                });
#pragma warning restore 612, 618
        }
    }
}