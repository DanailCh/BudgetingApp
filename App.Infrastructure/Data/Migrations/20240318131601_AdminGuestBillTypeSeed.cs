using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseholdBudgetingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminGuestBillTypeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7e63efa5-331d-4c9d-895e-a62932cf9d16", 0, "1105b607-2e89-478a-8067-e1665bc79a32", null, false, false, null, null, "GUEST@GUEST.BG", "AQAAAAEAACcQAAAAEFl+eJQ/9LFaL6NNI+Nzv5sExKe5GMYXuhynNcNeHRlov5pWl3mHnw9vs2GQPdJtQA==", null, false, "dcbfbdc5-cf2f-497a-83c3-7c314e8132ab", false, "guest@guest.bg" },
                    { "efa000aa-adc4-4e24-a145-1ea26fa418b8", 0, "6ddf1a4c-b6fb-4ac3-a0c3-52c69bb2fa95", null, false, false, null, null, "ADMIN@ADMIN.BG", "AQAAAAEAACcQAAAAEPYc4UkSoT6vkFIjbmuPBg8EFLyRJxZLqwkD9MRggA2Gd7T29xUr7GgSWDxqx6zP3A==", null, false, "118921c1-57e1-4bfd-a8ed-8382a7229e8b", false, "admin@admin.bg" }
                });

            migrationBuilder.InsertData(
                table: "BillTypes",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Electricity", "efa000aa-adc4-4e24-a145-1ea26fa418b8" },
                    { 2, "Water", "efa000aa-adc4-4e24-a145-1ea26fa418b8" },
                    { 3, "Heat", "efa000aa-adc4-4e24-a145-1ea26fa418b8" },
                    { 4, "Internet", "efa000aa-adc4-4e24-a145-1ea26fa418b8" },
                    { 5, "Rent", "efa000aa-adc4-4e24-a145-1ea26fa418b8" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e63efa5-331d-4c9d-895e-a62932cf9d16");

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

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efa000aa-adc4-4e24-a145-1ea26fa418b8");
        }
    }
}
