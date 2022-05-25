using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeloggerCore.Data.Migrations
{
    public partial class CompositeKeyClientAgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientAgency_AgencyId",
                table: "ClientAgency");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAgency_AgencyId_ClientId",
                table: "ClientAgency",
                columns: new[] { "AgencyId", "ClientId" },
                unique: true,
                filter: "[AgencyId] IS NOT NULL AND [ClientId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClientAgency_AgencyId_ClientId",
                table: "ClientAgency");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAgency_AgencyId",
                table: "ClientAgency",
                column: "AgencyId");
        }
    }
}
