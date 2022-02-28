using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FVCPD.Data.Migrations
{
    public partial class AddingForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Verb_Id",
                table: "ConjugatedVerbs",
                newName: "VerbId");

            migrationBuilder.RenameColumn(
                name: "Tense_Id",
                table: "ConjugatedVerbs",
                newName: "TenseId");

            migrationBuilder.RenameColumn(
                name: "Pronound_Id",
                table: "ConjugatedVerbs",
                newName: "PronounId");

            migrationBuilder.RenameColumn(
                name: "Mood_Id",
                table: "ConjugatedVerbs",
                newName: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ConjugatedVerbs_MoodId",
                table: "ConjugatedVerbs",
                column: "MoodId");

            migrationBuilder.CreateIndex(
                name: "IX_ConjugatedVerbs_TenseId",
                table: "ConjugatedVerbs",
                column: "TenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ConjugatedVerbs_VerbId",
                table: "ConjugatedVerbs",
                column: "VerbId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConjugatedVerbs_Moods_MoodId",
                table: "ConjugatedVerbs",
                column: "MoodId",
                principalTable: "Moods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConjugatedVerbs_Pronouns_PronounId",
                table: "ConjugatedVerbs",
                column: "PronounId",
                principalTable: "Pronouns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConjugatedVerbs_Tenses_TenseId",
                table: "ConjugatedVerbs",
                column: "TenseId",
                principalTable: "Tenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConjugatedVerbs_Verbs_VerbId",
                table: "ConjugatedVerbs",
                column: "VerbId",
                principalTable: "Verbs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConjugatedVerbs_Moods_MoodId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ConjugatedVerbs_Pronouns_PronounId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ConjugatedVerbs_Tenses_TenseId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropForeignKey(
                name: "FK_ConjugatedVerbs_Verbs_VerbId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropIndex(
                name: "IX_ConjugatedVerbs_MoodId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropIndex(
                name: "IX_ConjugatedVerbs_TenseId",
                table: "ConjugatedVerbs");

            migrationBuilder.DropIndex(
                name: "IX_ConjugatedVerbs_VerbId",
                table: "ConjugatedVerbs");

            migrationBuilder.RenameColumn(
                name: "VerbId",
                table: "ConjugatedVerbs",
                newName: "Verb_Id");

            migrationBuilder.RenameColumn(
                name: "TenseId",
                table: "ConjugatedVerbs",
                newName: "Tense_Id");

            migrationBuilder.RenameColumn(
                name: "PronounId",
                table: "ConjugatedVerbs",
                newName: "Pronound_Id");

            migrationBuilder.RenameColumn(
                name: "MoodId",
                table: "ConjugatedVerbs",
                newName: "Mood_Id");
        }
    }
}
