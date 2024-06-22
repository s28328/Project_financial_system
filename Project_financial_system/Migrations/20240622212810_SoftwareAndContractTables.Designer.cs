﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project_financial_system.Context;

#nullable disable

namespace Project_financial_system.Migrations
{
    [DbContext(typeof(FinancialContext))]
    [Migration("20240622212810_SoftwareAndContractTables")]
    partial class SoftwareAndContractTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Project_financial_system.Models.Address", b =>
                {
                    b.Property<int>("IdAddress")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdAddress"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("IdAddress");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCategory"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("IdCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Contract", b =>
                {
                    b.Property<int>("IdContract")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdContract"));

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("numeric");

                    b.Property<int>("DayInterval")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("integer");

                    b.Property<int>("IdDiscount")
                        .HasColumnType("integer");

                    b.Property<int>("IdVersion")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSigned")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatesInfo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("YearsOfSupport")
                        .HasColumnType("integer");

                    b.HasKey("IdContract");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdDiscount");

                    b.HasIndex("IdVersion");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdCustomer"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<int>("IdAddress")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("IdCustomer");

                    b.HasIndex("IdAddress");

                    b.ToTable("Customer", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdPayment"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdContract")
                        .HasColumnType("integer");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quota")
                        .HasColumnType("numeric");

                    b.HasKey("IdPayment");

                    b.HasIndex("IdContract");

                    b.HasIndex("IdCustomer");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Software", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdSoftware"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("IdCategory")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("PriceForYear")
                        .HasColumnType("numeric");

                    b.HasKey("IdSoftware");

                    b.HasIndex("IdCategory");

                    b.ToTable("Software");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Version", b =>
                {
                    b.Property<int>("IdVersion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdVersion"));

                    b.Property<int>("IdSoftware")
                        .HasColumnType("integer");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("IdVersion");

                    b.HasIndex("IdSoftware");

                    b.ToTable("Version");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.CompanyCustomer", b =>
                {
                    b.HasBaseType("Project_financial_system.Models.Domain.Customer");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Company_Customer", (string)null);
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.IndividualCustomer", b =>
                {
                    b.HasBaseType("Project_financial_system.Models.Domain.Customer");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PESEL")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.ToTable("Individual_Customer", (string)null);
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Contract", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Customer", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_financial_system.Models.Domain.Discount", "Discount")
                        .WithMany("Contracts")
                        .HasForeignKey("IdDiscount")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_financial_system.Models.Domain.Version", "Version")
                        .WithMany()
                        .HasForeignKey("IdVersion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Discount");

                    b.Navigation("Version");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Customer", b =>
                {
                    b.HasOne("Project_financial_system.Models.Address", "Address")
                        .WithMany("Customers")
                        .HasForeignKey("IdAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Payment", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_financial_system.Models.Domain.Customer", "Customer")
                        .WithMany("Payments")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Software", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Category", "Category")
                        .WithMany("Softwares")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Version", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Software", "Software")
                        .WithMany("Versions")
                        .HasForeignKey("IdSoftware")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Software");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.CompanyCustomer", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Customer", null)
                        .WithOne()
                        .HasForeignKey("Project_financial_system.Models.Domain.CompanyCustomer", "IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.IndividualCustomer", b =>
                {
                    b.HasOne("Project_financial_system.Models.Domain.Customer", null)
                        .WithOne()
                        .HasForeignKey("Project_financial_system.Models.Domain.IndividualCustomer", "IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_financial_system.Models.Address", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Category", b =>
                {
                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Customer", b =>
                {
                    b.Navigation("Contracts");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Discount", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("Project_financial_system.Models.Domain.Software", b =>
                {
                    b.Navigation("Versions");
                });
#pragma warning restore 612, 618
        }
    }
}
