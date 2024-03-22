using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseholdBudgetingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class softDeleteColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "HouseholdMembers",
                type: "datetime2",
                nullable: true,
                comment: "Date of deletion");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "BillTypes",
                type: "datetime2",
                nullable: true,
                comment: "Date of deletion");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Bills",
                type: "datetime2",
                nullable: true,
                comment: "Date of deletion");

           

            migrationBuilder.UpdateData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeletedOn",
                value: null);

            migrationBuilder.UpdateData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeletedOn",
                value: null);

            migrationBuilder.UpdateData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeletedOn",
                value: null);

            migrationBuilder.UpdateData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeletedOn",
                value: null);

            migrationBuilder.UpdateData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeletedOn",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "HouseholdMembers");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "BillTypes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Bills");

           
        }
    }
}
