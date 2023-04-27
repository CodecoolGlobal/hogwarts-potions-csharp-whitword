using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HogwartsPotions.Migrations
{
    /// <inheritdoc />
    public partial class CorrectRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Potions_PotionID",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeID",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_PotionID",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PotionID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeID",
                table: "Ingredients");

            migrationBuilder.CreateTable(
                name: "IngredientPotion",
                columns: table => new
                {
                    IngredientsID = table.Column<long>(type: "bigint", nullable: false),
                    PotionListID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPotion", x => new { x.IngredientsID, x.PotionListID });
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Ingredients_IngredientsID",
                        column: x => x.IngredientsID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPotion_Potions_PotionListID",
                        column: x => x.PotionListID,
                        principalTable: "Potions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    IngredientsID = table.Column<long>(type: "bigint", nullable: false),
                    RecipeListID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => new { x.IngredientsID, x.RecipeListID });
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_IngredientsID",
                        column: x => x.IngredientsID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipes_RecipeListID",
                        column: x => x.RecipeListID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPotion_PotionListID",
                table: "IngredientPotion",
                column: "PotionListID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipeListID",
                table: "IngredientRecipe",
                column: "RecipeListID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPotion");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.AddColumn<long>(
                name: "PotionID",
                table: "Ingredients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RecipeID",
                table: "Ingredients",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_PotionID",
                table: "Ingredients",
                column: "PotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeID",
                table: "Ingredients",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Potions_PotionID",
                table: "Ingredients",
                column: "PotionID",
                principalTable: "Potions",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeID",
                table: "Ingredients",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "ID");
        }
    }
}
