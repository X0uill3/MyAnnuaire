using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnnuaireModel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstActif",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Visiteur_EstActif",
                table: "Utilisateurs");

            migrationBuilder.AddColumn<bool>(
                name: "EstAdmin",
                table: "Utilisateurs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstAdmin",
                table: "Utilisateurs");

            migrationBuilder.AddColumn<bool>(
                name: "EstActif",
                table: "Utilisateurs",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visiteur_EstActif",
                table: "Utilisateurs",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
