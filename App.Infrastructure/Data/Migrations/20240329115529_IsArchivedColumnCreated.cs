using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseholdBudgetingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsArchivedColumnCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Is the Bill archived");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Bills");

            
        }
    }
}
