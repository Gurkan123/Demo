using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.API.Migrations
{
    public partial class ExtendedUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanUpdate",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanUpdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
