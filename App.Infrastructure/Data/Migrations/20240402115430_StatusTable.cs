using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "FeedbackMessages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Status for message");

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Identifier for Status")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Status name")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackMessages_StatusId",
                table: "FeedbackMessages",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackMessages_Statuses_StatusId",
                table: "FeedbackMessages",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackMessages_Statuses_StatusId",
                table: "FeedbackMessages");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackMessages_StatusId",
                table: "FeedbackMessages");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "FeedbackMessages");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bb4e2cb-4568-4299-b75c-38674e7dd9c2", "AQAAAAEAACcQAAAAEJwdhoYVAEvO5wUrgDsmG5N7EQlq79+BkfFd7ABlWUor6HGG54k7Q6azinLDSFAd4Q==", "8c881fd6-9983-4729-98a7-3b31730c12a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48440846-2660-4f62-9bd1-ff8a6e588139", "AQAAAAEAACcQAAAAEGODI0z5IvHWA8LfBmX6vmds9ToZ9yVxhLKB/dBRa4konkndAJwPzQXmbvHFMuiEow==", "320dd0fe-83bf-49a3-93f7-2593b15da27e" });
        }
    }
}
