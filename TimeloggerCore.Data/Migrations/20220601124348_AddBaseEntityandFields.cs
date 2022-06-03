using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeloggerCore.Data.Migrations
{
    public partial class AddBaseEntityandFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companies");
        }
    }
}
