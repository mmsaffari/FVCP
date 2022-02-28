using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FVCPD.Data.Migrations
{
    public partial class AddedConjugatedVerb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConjugatedVerbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pronound_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Tense_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Mood_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Verb_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Conjugation = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConjugatedVerbs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UK_ConjugatedVerb",
                table: "ConjugatedVerbs",
                columns: new[] { "Pronound_Id", "Tense_Id", "Mood_Id", "Verb_Id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConjugatedVerbs");
        }
    }
}
