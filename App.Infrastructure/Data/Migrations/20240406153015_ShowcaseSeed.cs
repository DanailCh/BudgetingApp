using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace App.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShowcaseSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe5a3b44-e0ed-4885-b826-140805d7180e", "AQAAAAEAACcQAAAAEPH8uO06q2SQJ644V9RCKAXpPv3vumAtHyewOtoMq6TrawA9lHytu4lBLTbqsUo37w==", "548cced9-6178-45c4-840c-a266718536f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7245911e-1ad6-46aa-a087-e4eb1445b500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "64ff78da-8e91-488b-ba28-e28db32b09e3", "AQAAAAEAACcQAAAAEKJEeSNsjN0XCvzcLpDJrBpuc3Pbw0eDOO0sJAq8Ck/9l69zAxm2snipCe0FnSIYIA==", "f629b008-9692-4242-884c-862e6a06faa6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0125abad-5f14-479e-8d8d-702b9833046f", "AQAAAAEAACcQAAAAELjqajO1TCt1ClgpCeM9AdbKU/gWLxrNt1rU7AofTt2uteiMiU8TqNJNSA4dhYN8MA==", "44078568-0bcb-4987-9b00-0c5e0e37d48c" });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "BillTypeId", "Cost", "Date", "DeletedOn", "IsArchived", "IsPayed", "PayerId", "UserId" },
                values: new object[,]
                {
                    { 6, 1, 100.00m, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 7, 3, 150.00m, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 8, 5, 1050.00m, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" }
                });

            migrationBuilder.InsertData(
                table: "EndMonthSummaries",
                columns: new[] { "Id", "Date", "IsResolved", "Summary", "UserId" },
                values: new object[] { 1, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Total Household Income: 6500.00<br> <br>Total Household Expences: 1390.00<br> <br>-Victor payed: 150.00 which is 106.62 less.<br> <br>-Danail payed: 70.00 which is 314.92 less.<br> <br>-pesho payed: 150.00 which is 170.77 less.<br> <br>-ivan payed: 1020.00 which is 592.31 too much.", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.InsertData(
                table: "FeedbackMessages",
                columns: new[] { "Id", "Comment", "Content", "Date", "IsReadByUser", "SenderId", "SeverityTypeId", "StatusId", "Title" },
                values: new object[,]
                {
                    { 1, null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", new DateTime(2024, 4, 6, 18, 30, 15, 677, DateTimeKind.Local).AddTicks(6936), true, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", null, 1, "Title for Feedback1" },
                    { 2, "Your feedback has been acknowledged and we are working on solving the problem", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Dictum fusce ut placerat orci nulla pellentesque dignissim enim sit.", new DateTime(2024, 3, 27, 18, 30, 15, 677, DateTimeKind.Local).AddTicks(6938), false, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 1, 2, "Title for Feedback2" },
                    { 3, "Your feedback has been acknowledged and we are working on solving the problem", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", new DateTime(2024, 3, 22, 18, 30, 15, 677, DateTimeKind.Local).AddTicks(6940), true, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 3, 2, "Title for Feedback3" },
                    { 4, "We are happy to inform you that the issue has been resolved", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", new DateTime(2024, 3, 6, 18, 30, 15, 677, DateTimeKind.Local).AddTicks(6942), false, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 2, 3, "Title for Feedback4" }
                });

            migrationBuilder.InsertData(
                table: "HouseholdBudgets",
                columns: new[] { "Id", "Date", "Expences", "Income", "UserId" },
                values: new object[] { 1, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1390.00m, 6500.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.InsertData(
                table: "HouseholdMembers",
                columns: new[] { "Id", "DeletedOn", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Victor", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 2, null, "Danail", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, null, "Pesho", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 4, null, "Ivan", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "BillTypeId", "Cost", "Date", "DeletedOn", "IsArchived", "IsPayed", "PayerId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 150.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, 1, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 2, 2, 70.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, 2, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, 3, 150.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, 3, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 4, 4, 20.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, 4, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 5, 5, 1000.00m, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, true, 4, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" }
                });

            migrationBuilder.InsertData(
                table: "MemberSalaries",
                columns: new[] { "Id", "Date", "HouseholdMemberId", "Salary", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 2, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1800.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 3, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1500.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" },
                    { 4, new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2000.00m, "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Bills",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EndMonthSummaries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FeedbackMessages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FeedbackMessages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FeedbackMessages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FeedbackMessages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HouseholdBudgets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MemberSalaries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MemberSalaries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MemberSalaries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MemberSalaries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "HouseholdMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "HouseholdMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "HouseholdMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "HouseholdMembers",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
