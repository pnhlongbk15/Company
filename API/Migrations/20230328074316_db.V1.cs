using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class dbV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "4b6c0166-e444-48aa-a33a-6338954d8bbc");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "e48e3499-b63b-430f-aa92-ac3fcb180e90");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b63c2782-fd09-4073-91b1-24ee4e03c134");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c8b877ea-8cc3-4786-969f-60daa98f0b08");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "283feb4b-8278-4b21-ae9b-b233b6191237");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "c1b3594b-51ad-4dba-b78c-105558e2a117");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "39d0337f-709a-459a-90b0-6c40fec89924", "Sale" },
                    { "63d366dc-1e4d-415a-b8d3-a5e812c753f3", "IT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "59424440-034b-4562-947f-20bf2e6ce5f3", null, "Visitor", "VISITOR" },
                    { "a19460ed-b45a-4bf6-96c3-f6f75efce55f", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "180e96da-3b31-4c64-8387-87e00d89f143", new DateTime(1981, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "39d0337f-709a-459a-90b0-6c40fec89924", "jan.kirsten@gmail.com", "Jan", "Kirsten", "111-222-3333" },
                    { "4f801d10-a503-47e4-aa40-c971b84b81b4", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "63d366dc-1e4d-415a-b8d3-a5e812c753f3", "uncle.bob@gmail.com", "Uncle", "Bob", "999-888-7777" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "180e96da-3b31-4c64-8387-87e00d89f143");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: "4f801d10-a503-47e4-aa40-c971b84b81b4");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "59424440-034b-4562-947f-20bf2e6ce5f3");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a19460ed-b45a-4bf6-96c3-f6f75efce55f");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "39d0337f-709a-459a-90b0-6c40fec89924");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: "63d366dc-1e4d-415a-b8d3-a5e812c753f3");

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "283feb4b-8278-4b21-ae9b-b233b6191237", "IT" },
                    { "c1b3594b-51ad-4dba-b78c-105558e2a117", "Sale" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b63c2782-fd09-4073-91b1-24ee4e03c134", null, "Admin", "ADMIN" },
                    { "c8b877ea-8cc3-4786-969f-60daa98f0b08", null, "Visitor", "VISITOR" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { "4b6c0166-e444-48aa-a33a-6338954d8bbc", new DateTime(1979, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "283feb4b-8278-4b21-ae9b-b233b6191237", "uncle.bob@gmail.com", "Uncle", "Bob", "999-888-7777" },
                    { "e48e3499-b63b-430f-aa92-ac3fcb180e90", new DateTime(1981, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "c1b3594b-51ad-4dba-b78c-105558e2a117", "jan.kirsten@gmail.com", "Jan", "Kirsten", "111-222-3333" }
                });
        }
    }
}
