using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class cccc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Clienti",
                newName: "Password");

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "Prodotti",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prodotti_IdCategoria",
                table: "Prodotti",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodotti_Categorie_IdCategoria",
                table: "Prodotti",
                column: "IdCategoria",
                principalTable: "Categorie",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodotti_Categorie_IdCategoria",
                table: "Prodotti");

            migrationBuilder.DropIndex(
                name: "IX_Prodotti_IdCategoria",
                table: "Prodotti");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "Prodotti");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Clienti",
                newName: "password");
        }
    }
}
