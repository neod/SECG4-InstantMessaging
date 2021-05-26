using Microsoft.EntityFrameworkCore.Migrations;

namespace instantMessagingClient.Migrations
{
    public partial class trois : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_myMessages",
                table: "myMessages");

            migrationBuilder.RenameTable(
                name: "myMessages",
                newName: "MyMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyMessages",
                table: "MyMessages",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyMessages",
                table: "MyMessages");

            migrationBuilder.RenameTable(
                name: "MyMessages",
                newName: "myMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_myMessages",
                table: "myMessages",
                column: "Id");
        }
    }
}
