using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rokovi_Predmeti_PredmetID",
                table: "Rokovi");

            migrationBuilder.DropForeignKey(
                name: "FK_Rokovi_Studenti_StudentID",
                table: "Rokovi");

            migrationBuilder.DropIndex(
                name: "IX_Rokovi_PredmetID",
                table: "Rokovi");

            migrationBuilder.DropIndex(
                name: "IX_Rokovi_StudentID",
                table: "Rokovi");

            migrationBuilder.DropColumn(
                name: "Ocena",
                table: "Rokovi");

            migrationBuilder.DropColumn(
                name: "PredmetID",
                table: "Rokovi");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Rokovi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ocena",
                table: "Rokovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PredmetID",
                table: "Rokovi",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Rokovi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rokovi_PredmetID",
                table: "Rokovi",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Rokovi_StudentID",
                table: "Rokovi",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rokovi_Predmeti_PredmetID",
                table: "Rokovi",
                column: "PredmetID",
                principalTable: "Predmeti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rokovi_Studenti_StudentID",
                table: "Rokovi",
                column: "StudentID",
                principalTable: "Studenti",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
