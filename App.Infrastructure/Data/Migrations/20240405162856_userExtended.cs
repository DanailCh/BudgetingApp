using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class userExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PasswordSetupRequired",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PasswordSetupRequired", "SecurityStamp" },
                values: new object[] { "eb76fc45-bba0-4f56-8f7c-843bd7bc03da", "AQAAAAEAACcQAAAAEBmyVOVYcw/FPyrVgEqkLIw6WGxuCHrNHiRjMTer36SEhd+fLF1XpUOQq3oZjENe7Q==", false, "6ad4cebf-c730-4ce0-970e-e75b308a8c47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PasswordSetupRequired", "SecurityStamp" },
                values: new object[] { "ab90653f-f2a0-4c5e-a17c-588dc571bef5", "AQAAAAEAACcQAAAAEODIfCpe0Vy+1Q+tPiqai7y3F1ahabuw++QHrT1F5T4IMdBKo7+04OutbrLFcseWEA==", false, "4858d595-e3b9-4bfa-817d-17961bfa789d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PasswordSetupRequired", "SecurityStamp" },
                values: new object[] { "2071236c-2a99-4bee-be69-1663064b5e7b", "AQAAAAEAACcQAAAAEKLtAG7xKfgwGVbjhGzt7VrNAP+nx4/IA6AeZFSlbsjAEpupwgBNoAWcbTbfeKCspA==", true, "62217b0b-54a1-4637-8b3b-6640fa23879b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSetupRequired",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2e6caf8-20ae-47d0-8412-e8639612be6f", "AQAAAAEAACcQAAAAEK8rqahEqN1rU90l5w2YNiPxZ712WEB21tJJCJ9WXuysOgJVTzS/zJ9/j0pCb6GGtA==", "e91d5a48-27f5-424c-bca2-525c2851c1f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "66ce92a0-695b-4cb6-b501-460c20ec3fdb", "AQAAAAEAACcQAAAAEOigl9eTahnYXM2d0GZdT2X5g2pY3205VzoMjw7BucnXgZcuKjH4OnW7RPWq4k6H4Q==", "a7ea4e2c-f076-47c0-aa02-d24257192278" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "395492c7-23a5-47fb-ab5d-3e4fa3d5a902", "AQAAAAEAACcQAAAAEBTF7UXnuBZg3c9wCs+JxqciTysUZvlpD0w9C9bVwuz2Oa4187aWHhVA83eDR8sM6A==", "3cda77ec-7182-4aa9-ac41-0bd2f6aeab4f" });
        }
    }
}
