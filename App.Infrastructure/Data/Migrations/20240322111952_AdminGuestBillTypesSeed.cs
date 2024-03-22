using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseholdBudgetingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminGuestBillTypesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "529039c5-1688-44f7-a240-2fdcded4f3ee", 0, "eb8441dd-2b1d-4394-8d8b-d3d72fa91f68", null, false, false, null, null, "GUEST@GUEST.BG", "AQAAAAEAACcQAAAAEIqidsNU0x60sf8R7W2ZKfy8U7JYyTiUhdspS5VWMOxl5Wg7mfeQ/STIlaOpQRJXWg==", null, false, "6e724bc7-0e61-44e8-bb11-4637ac414f3e", false, "guest@guest.bg" },
                    { "eaeace5c-bfa5-4b25-a7e3-25b91e3917fd", 0, "3ac4ace8-bcf2-49b2-ae5f-8b1632052377", null, false, false, null, null, "ADMIN@ADMIN.BG", "AQAAAAEAACcQAAAAEDZgl4xC1WmZVZNsfXv2YuxyJDWlOkEaXTc2zTpsXF+TGJa9/YhO5/G8+ZKgRHFTPA==", null, false, "0221e71d-25ae-4d74-a53e-400cfa7a578b", false, "admin@admin.bg" }
                });

            migrationBuilder.InsertData(
                table: "BillTypes",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Electricity", null },
                    { 2, "Water", null },
                    { 3, "Heat", null },
                    { 4, "Internet", null },
                    { 5, "Rent", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "529039c5-1688-44f7-a240-2fdcded4f3ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eaeace5c-bfa5-4b25-a7e3-25b91e3917fd");

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BillTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
