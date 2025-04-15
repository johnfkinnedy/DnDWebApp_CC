using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDWebApp_CC.Migrations
{
    /// <inheritdoc />
    public partial class MigInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backgrounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberToRoll = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HitDiceId = table.Column<int>(type: "int", nullable: false),
                    Spellcaster = table.Column<bool>(type: "bit", nullable: false),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterClasses_Dice_HitDiceId",
                        column: x => x.HitDiceId,
                        principalTable: "Dice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiceId = table.Column<int>(type: "int", nullable: true),
                    SlotLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spells_Dice_DiceId",
                        column: x => x.DiceId,
                        principalTable: "Dice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proficiency = table.Column<bool>(type: "bit", nullable: false),
                    BaseStatId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Stats_BaseStatId",
                        column: x => x.BaseStatId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Alignment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    SecondClassId = table.Column<int>(type: "int", nullable: true),
                    SpeciesId = table.Column<int>(type: "int", nullable: false),
                    BackgroundId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyBonus = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Featues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Languages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.id);
                    table.ForeignKey(
                        name: "FK_Characters_Backgrounds_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Backgrounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterClasses_ClassId",
                        column: x => x.ClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterClasses_SecondClassId",
                        column: x => x.SecondClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Characters_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSpells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CharClassId = table.Column<int>(type: "int", nullable: true),
                    SpellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSpells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSpells_CharacterClasses_CharClassId",
                        column: x => x.CharClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSpells_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiceInSpells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiceId = table.Column<int>(type: "int", nullable: false),
                    SpellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceInSpells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiceInSpells_Dice_DiceId",
                        column: x => x.DiceId,
                        principalTable: "Dice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiceInSpells_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    CharClassId = table.Column<int>(type: "int", nullable: true),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassSkills_CharacterClasses_CharClassId",
                        column: x => x.CharClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ClassSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillsInBackgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackgroundId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsInBackgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillsInBackgrounds_Backgrounds_BackgroundId",
                        column: x => x.BackgroundId,
                        principalTable: "Backgrounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsInBackgrounds_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillsInSpecies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeciesId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsInSpecies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillsInSpecies_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsInSpecies_Species_SpeciesId",
                        column: x => x.SpeciesId,
                        principalTable: "Species",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentInCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentInCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentInCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentInCharacters_Equipment_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillsInCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsInCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SkillsInCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsInCharacters_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpellsInCharacter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    SpellId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpellsInCharacter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpellsInCharacter_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpellsInCharacter_Spells_SpellId",
                        column: x => x.SpellId,
                        principalTable: "Spells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatsInCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    StatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsInCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatsInCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatsInCharacters_Stats_StatId",
                        column: x => x.StatId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterClasses_HitDiceId",
                table: "CharacterClasses",
                column: "HitDiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_BackgroundId",
                table: "Characters",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ClassId",
                table: "Characters",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SecondClassId",
                table: "Characters",
                column: "SecondClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_SpeciesId",
                table: "Characters",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSkills_CharClassId",
                table: "ClassSkills",
                column: "CharClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSkills_SkillId",
                table: "ClassSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSpells_CharClassId",
                table: "ClassSpells",
                column: "CharClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassSpells_SpellId",
                table: "ClassSpells",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_DiceInSpells_DiceId",
                table: "DiceInSpells",
                column: "DiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DiceInSpells_SpellId",
                table: "DiceInSpells",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInCharacters_CharacterId",
                table: "EquipmentInCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentInCharacters_EquipmentId",
                table: "EquipmentInCharacters",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_BaseStatId",
                table: "Skills",
                column: "BaseStatId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInBackgrounds_BackgroundId",
                table: "SkillsInBackgrounds",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInBackgrounds_SkillId",
                table: "SkillsInBackgrounds",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInCharacters_CharacterId",
                table: "SkillsInCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInCharacters_SkillId",
                table: "SkillsInCharacters",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInSpecies_SkillId",
                table: "SkillsInSpecies",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsInSpecies_SpeciesId",
                table: "SkillsInSpecies",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_DiceId",
                table: "Spells",
                column: "DiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellsInCharacter_CharacterId",
                table: "SpellsInCharacter",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SpellsInCharacter_SpellId",
                table: "SpellsInCharacter",
                column: "SpellId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsInCharacters_CharacterId",
                table: "StatsInCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_StatsInCharacters_StatId",
                table: "StatsInCharacters",
                column: "StatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassSkills");

            migrationBuilder.DropTable(
                name: "ClassSpells");

            migrationBuilder.DropTable(
                name: "DiceInSpells");

            migrationBuilder.DropTable(
                name: "EquipmentInCharacters");

            migrationBuilder.DropTable(
                name: "SkillsInBackgrounds");

            migrationBuilder.DropTable(
                name: "SkillsInCharacters");

            migrationBuilder.DropTable(
                name: "SkillsInSpecies");

            migrationBuilder.DropTable(
                name: "SpellsInCharacter");

            migrationBuilder.DropTable(
                name: "StatsInCharacters");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Stats");

            migrationBuilder.DropTable(
                name: "Backgrounds");

            migrationBuilder.DropTable(
                name: "CharacterClasses");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Dice");
        }
    }
}
