using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    /// <inheritdoc />
    public partial class PotionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recipes_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Potions",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<long>(type: "bigint", nullable: true),
                    BrewingStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    RecipeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Potions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Potions_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Potions_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PotionID = table.Column<long>(type: "bigint", nullable: true),
                    RecipeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ingredients_Potions_PotionID",
                        column: x => x.PotionID,
                        principalTable: "Potions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PotionID",
                table: "Ingredients",
                column: "PotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeID",
                table: "Ingredients",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Potions_RecipeID",
                table: "Potions",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Potions_StudentID",
                table: "Potions",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_StudentID",
                table: "Recipes",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Potions");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
