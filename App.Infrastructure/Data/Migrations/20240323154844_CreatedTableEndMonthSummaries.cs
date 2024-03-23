using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseholdBudgetingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTableEndMonthSummaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "EndMonthSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for Summary")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "For which month and year is the Summary"),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false, comment: "Summary content"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Foreign key for User")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndMonthSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndMonthSummaries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_EndMonthSummaries_UserId",
                table: "EndMonthSummaries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndMonthSummaries");

            
        }
    }
}
