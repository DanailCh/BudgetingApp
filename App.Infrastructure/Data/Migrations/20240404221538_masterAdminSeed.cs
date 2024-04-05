using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class masterAdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e6caf8-20ae-47d0-8412-e8639612be6f", "AQAAAAEAACcQAAAAEK8rqahEqN1rU90l5w2YNiPxZ712WEB21tJJCJ9WXuysOgJVTzS/zJ9/j0pCb6GGtA==", "e91d5a48-27f5-424c-bca2-525c2851c1f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "395492c7-23a5-47fb-ab5d-3e4fa3d5a902", "AQAAAAEAACcQAAAAEBTF7UXnuBZg3c9wCs+JxqciTysUZvlpD0w9C9bVwuz2Oa4187aWHhVA83eDR8sM6A==", "3cda77ec-7182-4aa9-ac41-0bd2f6aeab4f" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7245911e-1ad6-46aa-a087-e4eb1445b500", 0, "66ce92a0-695b-4cb6-b501-460c20ec3fdb", "madmin@mail.com", false, false, null, "madmin@mail.com", "MASTERADMIN", "AQAAAAEAACcQAAAAEOigl9eTahnYXM2d0GZdT2X5g2pY3205VzoMjw7BucnXgZcuKjH4OnW7RPWq4k6H4Q==", null, false, "a7ea4e2c-f076-47c0-aa02-d24257192278", false, "masteradmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a50be1b5-4ca9-48ad-8c32-9a9ff4c61134", "AQAAAAEAACcQAAAAEOohO+GTMusUx+snmHo0VYcqiLEVUzqZsEyUQGdvmb2KRmSKmfGnaJPWBStSlYfXDA==", "62d408c6-2ac3-4d55-85a1-35d7b9358f94" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d39d5a9a-df14-4c33-b738-0a70e55eb367", "AQAAAAEAACcQAAAAEHmKyCQdNkphcaKl9DRIsKXIFGtLfMd9VAnUQL02gq17cR3jWPAqXBg/xz7GTsrVWQ==", "f172c538-8101-450a-a01e-e3bb9cfbe5d9" });
        }
    }
}
