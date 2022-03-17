using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Predmeti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Godina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmeti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Indeks = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rokovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    PredmetID = table.Column<int>(type: "int", nullable: true),
                    StudentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rokovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rokovi_Predmeti_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmeti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rokovi_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentiPredmeti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocena = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    PredmetID = table.Column<int>(type: "int", nullable: true),
                    IspitniRokID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentiPredmeti", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentiPredmeti_Predmeti_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmeti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentiPredmeti_Rokovi_IspitniRokID",
                        column: x => x.IspitniRokID,
                        principalTable: "Rokovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentiPredmeti_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rokovi_PredmetID",
                table: "Rokovi",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Rokovi_StudentID",
                table: "Rokovi",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiPredmeti_IspitniRokID",
                table: "StudentiPredmeti",
                column: "IspitniRokID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiPredmeti_PredmetID",
                table: "StudentiPredmeti",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentiPredmeti_StudentID",
                table: "StudentiPredmeti",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentiPredmeti");

            migrationBuilder.DropTable(
                name: "Rokovi");

            migrationBuilder.DropTable(
                name: "Predmeti");

            migrationBuilder.DropTable(
                name: "Studenti");
        }
    }
}
