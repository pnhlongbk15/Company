using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class dbV1Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "abc96609-1907-4c31-b9aa-6386fe7b8ec5");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "c94d6eec-94aa-4c51-b6d3-6c54db4ab89f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7921d379-01f0-4bb8-9888-3796e5049261");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bfd5b911-427b-41ca-8338-431408820e52");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Employees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "23fafa8d-1975-4963-8ac7-754d7ac6446e", "IT" },
                    { "2f626d4b-ac66-489a-b952-f8ad7a7963b8", "Sale" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5ce52853-d3b0-40f5-a92a-e7b9b929249d", null, "Visitor", "VISITOR" },
                    { "a79a6d25-9f5c-429a-9c39-dd1c9ceaf922", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "17a67111-b8ac-4c15-9f0d-f173882b6250", new DateTime(1981, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "2f626d4b-ac66-489a-b952-f8ad7a7963b8", "jan.kirsten@gmail.com", "Jan", "Kirsten", "111-222-3333" },
                    { "f019fa5c-13df-4fb9-92bc-acccee13f77b", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "23fafa8d-1975-4963-8ac7-754d7ac6446e", "uncle.bob@gmail.com", "Uncle", "Bob", "999-888-7777" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "17a67111-b8ac-4c15-9f0d-f173882b6250");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "f019fa5c-13df-4fb9-92bc-acccee13f77b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5ce52853-d3b0-40f5-a92a-e7b9b929249d");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a79a6d25-9f5c-429a-9c39-dd1c9ceaf922");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "abc96609-1907-4c31-b9aa-6386fe7b8ec5", new DateTime(1981, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.kirsten@gmail.com", "Jan", "Kirsten", "111-222-3333" },
                    { "c94d6eec-94aa-4c51-b6d3-6c54db4ab89f", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "uncle.bob@gmail.com", "Uncle", "Bob", "999-888-7777" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7921d379-01f0-4bb8-9888-3796e5049261", null, "Admin", "ADMIN" },
                    { "bfd5b911-427b-41ca-8338-431408820e52", null, "Visitor", "VISITOR" }
                });
        }
    }
}
