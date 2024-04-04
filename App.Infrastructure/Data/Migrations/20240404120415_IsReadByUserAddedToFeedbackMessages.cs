using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsReadByUserAddedToFeedbackMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReadByUser",
                table: "FeedbackMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReadByUser",
                table: "FeedbackMessages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ba792b0-5823-4cfa-aae2-d1029fac9f69", "AQAAAAEAACcQAAAAELBueZhd0juLw6fzpOctLzPmyx1nwEIonBuJJCx/ZN6mVkwjki5e7gkKs2xI6xnPNQ==", "57cc2881-37ea-4179-9501-49d59731f10b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3acfcfb6-0d9a-4bbb-bbc0-f4fe66360079", "AQAAAAEAACcQAAAAEOQ+iCaL6oKJN8eLzu98AOWr4cfXgHmUjmIHAta4KxVsT9UFcSu3+x19f3Gjvi/ljA==", "860b9927-e10f-4167-afb2-3a677d0198e6" });
        }
    }
}
