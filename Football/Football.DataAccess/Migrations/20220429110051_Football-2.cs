using Microsoft.EntityFrameworkCore.Migrations;

namespace Football.DataAccess.Migrations
{
    public partial class Football2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Leagues");
        }
    }
}
