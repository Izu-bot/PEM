using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalExpenseManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Despesa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Despesa",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
