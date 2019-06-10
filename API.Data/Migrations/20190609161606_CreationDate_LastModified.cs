using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class CreationDate_LastModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHREmployee",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Worktimes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Worktimes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Holidays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Holidays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Departments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Contracts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Contracts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Worktimes");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Worktimes");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Contracts");

            migrationBuilder.AddColumn<bool>(
                name: "IsHREmployee",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }
    }
}
