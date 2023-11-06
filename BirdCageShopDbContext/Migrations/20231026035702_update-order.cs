using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class updateorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a867642-8675-4f3b-bfac-8c27e8d8c1b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c06f5fe-393d-485d-a579-7f063cf19b48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9f32017-6fe8-4170-bd78-d6d6fb57b67b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffe2ba82-e254-43cc-8d45-c714716e3b62");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0decf35d-b2b5-4458-b76a-9e14e5a3de2d", "1", "Admin", "Admin" },
                    { "441ae6c0-332d-4f99-b7b2-470742f39dc0", "2", "Customer", "Customer" },
                    { "4567cf1a-3748-4f0a-8d48-79229d40bc02", "3", "Staff", "Staff" },
                    { "81292ec6-c258-462d-966a-868f39c35070", "2", "Manager", "Manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0decf35d-b2b5-4458-b76a-9e14e5a3de2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "441ae6c0-332d-4f99-b7b2-470742f39dc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4567cf1a-3748-4f0a-8d48-79229d40bc02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81292ec6-c258-462d-966a-868f39c35070");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherCode",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a867642-8675-4f3b-bfac-8c27e8d8c1b2", "1", "Admin", "Admin" },
                    { "8c06f5fe-393d-485d-a579-7f063cf19b48", "2", "Customer", "Customer" },
                    { "f9f32017-6fe8-4170-bd78-d6d6fb57b67b", "2", "Manager", "Manager" },
                    { "ffe2ba82-e254-43cc-8d45-c714716e3b62", "3", "Staff", "Staff" }
                });
        }
    }
}
