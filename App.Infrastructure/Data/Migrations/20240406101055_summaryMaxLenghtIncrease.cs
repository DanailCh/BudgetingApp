using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class summaryMaxLenghtIncrease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "EndMonthSummaries",
                type: "nvarchar(3000)",
                maxLength: 3000,
                nullable: false,
                comment: "Summary content",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldComment: "Summary content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10daa75a-3c50-4fc9-afdc-3d45483ba924", "AQAAAAEAACcQAAAAEOISFrFWXmosPXY6726FwQ7gQrxA0cWcvUvI8nhy0FcMEq23XzYf3RBd50ao/GUcCw==", "58e9462b-81cf-474c-bb89-8922fa96aaae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58e8d1c5-7f1f-4094-b5be-4ba600bddf96", "AQAAAAEAACcQAAAAEDJHk/ShIb/J3lan/+AAd528h6bEyNT42tTu9Paau6jF3oERcNAaKnLWKZu2Spx+9Q==", "b65b9862-c1a4-4a71-b26a-75368b3ec410" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27775724-db62-40f5-bf32-b55f5e2f84f9", "AQAAAAEAACcQAAAAEDwjaKXoIjDos08lWcYlWPguBxsSvsWdMckxD6935HWZxcfEN7rCEjiY1D2VuxwFpA==", "68e689e6-0c85-4bd9-8396-67ca73f29ffb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "EndMonthSummaries",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                comment: "Summary content",
                oldClrType: typeof(string),
                oldType: "nvarchar(3000)",
                oldMaxLength: 3000,
                oldComment: "Summary content");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb76fc45-bba0-4f56-8f7c-843bd7bc03da", "AQAAAAEAACcQAAAAEBmyVOVYcw/FPyrVgEqkLIw6WGxuCHrNHiRjMTer36SEhd+fLF1XpUOQq3oZjENe7Q==", "6ad4cebf-c730-4ce0-970e-e75b308a8c47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab90653f-f2a0-4c5e-a17c-588dc571bef5", "AQAAAAEAACcQAAAAEODIfCpe0Vy+1Q+tPiqai7y3F1ahabuw++QHrT1F5T4IMdBKo7+04OutbrLFcseWEA==", "4858d595-e3b9-4bfa-817d-17961bfa789d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2071236c-2a99-4bee-be69-1663064b5e7b", "AQAAAAEAACcQAAAAEKLtAG7xKfgwGVbjhGzt7VrNAP+nx4/IA6AeZFSlbsjAEpupwgBNoAWcbTbfeKCspA==", "62217b0b-54a1-4637-8b3b-6640fa23879b" });
        }
    }
}
