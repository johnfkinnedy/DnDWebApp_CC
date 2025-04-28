using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDWebApp_CC.Migrations
{
    /// <inheritdoc />
    public partial class mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Dice_DiceDenominationId",
                table: "Spells");

            migrationBuilder.RenameColumn(
                name: "DiceDenominationId",
                table: "Spells",
                newName: "DiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Spells_DiceDenominationId",
                table: "Spells",
                newName: "IX_Spells_DiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Dice_DiceId",
                table: "Spells",
                column: "DiceId",
                principalTable: "Dice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spells_Dice_DiceId",
                table: "Spells");

            migrationBuilder.RenameColumn(
                name: "DiceId",
                table: "Spells",
                newName: "DiceDenominationId");

            migrationBuilder.RenameIndex(
                name: "IX_Spells_DiceId",
                table: "Spells",
                newName: "IX_Spells_DiceDenominationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spells_Dice_DiceDenominationId",
                table: "Spells",
                column: "DiceDenominationId",
                principalTable: "Dice",
                principalColumn: "Id");
        }
    }
}
