using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KsiazkaAdresowa.Migrations
{
    public partial class CascadeDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactsData_Persons_PersonId",
                table: "ContactsData");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOfAdding",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsData_Persons_PersonId",
                table: "ContactsData",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactsData_Persons_PersonId",
                table: "ContactsData");

            migrationBuilder.AlterColumn<string>(
                name: "TimeOfAdding",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactsData_Persons_PersonId",
                table: "ContactsData",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
