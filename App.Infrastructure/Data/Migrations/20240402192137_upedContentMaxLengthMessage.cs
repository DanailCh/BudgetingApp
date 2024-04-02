using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class upedContentMaxLengthMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "FeedbackMessages",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "Content of Message",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldComment: "Content of Message");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "FeedbackMessages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                comment: "Content of Message",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "Content of Message");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9976fbc9-bfee-4995-9acc-b1cfba0c866c", "AQAAAAEAACcQAAAAEP0D0RTlFFSMdtnk9veshRKKHhCFswQWlNlB582ongy+s6Nl+Yq3rAc2A412y0aE0w==", "d64869b9-2bc7-434b-a7a3-45a03c380129" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bd8414be-5391-449a-9be6-972ede49fd8b", "AQAAAAEAACcQAAAAEO2vb8M2jqrtRvnNFn8J7zrvjhXUWe5UaqW9DRta+Xb1LyC2u6j6u7T52VF1Kd/4vw==", "10955600-f367-4fea-9b30-4f82895dade1" });
        }
    }
}
