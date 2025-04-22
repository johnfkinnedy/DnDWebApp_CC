using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDWebApp_CC.Migrations
{
    /// <inheritdoc />
    public partial class mig03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberToRoll",
                table: "Dice");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Characters",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Characters",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "NumberToRoll",
                table: "Dice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
