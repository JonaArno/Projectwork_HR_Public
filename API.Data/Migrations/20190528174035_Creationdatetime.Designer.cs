﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data.Migrations
{
    [DbContext(typeof(HrApplicationContext))]
    [Migration("20190528174035_Creationdatetime")]
    partial class Creationdatetime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Model.Contract", b =>
                {
                    b.Property<int>("ContractID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmployeeId");

                    b.Property<decimal>("GrossSalary");

                    b.Property<int>("NumberOfHolidays");

                    b.HasKey("ContractID");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("API.Model.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName");

                    b.Property<int?>("ManagerId");

                    b.HasKey("DepartmentID");

                    b.HasIndex("ManagerId")
                        .IsUnique()
                        .HasFilter("[ManagerId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("API.Model.Holiday", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDateTime");

                    b.Property<bool>("IsApproved");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("API.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int?>("DepartmentID");

                    b.Property<string>("EmailAddress");

                    b.Property<bool>("IsHREmployee");

                    b.Property<string>("Name");

                    b.Property<string>("PassWord");

                    b.HasKey("UserID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Model.WorkTime", b =>
                {
                    b.Property<int>("WorkTimeID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserID");

                    b.Property<DateTime>("WorkDateTime");

                    b.HasKey("WorkTimeID");

                    b.HasIndex("UserID");

                    b.ToTable("Worktimes");
                });

            modelBuilder.Entity("API.Model.Contract", b =>
                {
                    b.HasOne("API.Model.User", "User")
                        .WithOne("Contract")
                        .HasForeignKey("API.Model.Contract", "EmployeeId");
                });

            modelBuilder.Entity("API.Model.Department", b =>
                {
                    b.HasOne("API.Model.User", "Manager")
                        .WithOne("Department")
                        .HasForeignKey("API.Model.Department", "ManagerId");
                });

            modelBuilder.Entity("API.Model.Holiday", b =>
                {
                    b.HasOne("API.Model.User", "User")
                        .WithMany("Holidays")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("API.Model.User", b =>
                {
                    b.HasOne("API.Model.Department")
                        .WithMany("Members")
                        .HasForeignKey("DepartmentID");
                });

            modelBuilder.Entity("API.Model.WorkTime", b =>
                {
                    b.HasOne("API.Model.User", "User")
                        .WithMany("WorkTimes")
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
