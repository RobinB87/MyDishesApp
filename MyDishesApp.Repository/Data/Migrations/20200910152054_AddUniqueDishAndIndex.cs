using Microsoft.EntityFrameworkCore.Migrations;

namespace MyDishesApp.Repository.Data.Migrations
{
    public partial class AddUniqueDishAndIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Name",
                table: "Dishes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Dishes_Name",
                table: "Dishes");
        }
    }
}
