using Microsoft.EntityFrameworkCore.Migrations;

namespace KsiazkaAdresowa.Migrations
{
    public partial class datetimeValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeleAddresses_Persons_PersonId",
                table: "TeleAddresses");

            migrationBuilder.AddColumn<string>(
                name: "TimeOfAdding",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeleAddresses_Persons_PersonId",
                table: "TeleAddresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeleAddresses_Persons_PersonId",
                table: "TeleAddresses");

            migrationBuilder.DropColumn(
                name: "TimeOfAdding",
                table: "Persons");

            migrationBuilder.AddForeignKey(
                name: "FK_TeleAddresses_Persons_PersonId",
                table: "TeleAddresses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
