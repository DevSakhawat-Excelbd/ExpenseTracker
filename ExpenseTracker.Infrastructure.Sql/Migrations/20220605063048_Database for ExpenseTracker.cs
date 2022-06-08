using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Infrastructure.Sql.Migrations
{
    public partial class DatabaseforExpenseTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRowDeleted",
                table: "ExpenseDetails",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRowDeleted",
                table: "Categories",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRowDeleted",
                table: "ExpenseDetails");

            migrationBuilder.DropColumn(
                name: "IsRowDeleted",
                table: "Categories");
        }
    }
}
