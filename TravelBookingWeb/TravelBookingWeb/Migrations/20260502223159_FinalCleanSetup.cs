using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class FinalCleanSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinatii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tara = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinatii", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facilitati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume_Facilitate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilitati", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Activitati",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume_Activitate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret_pe_persoana = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Imagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinatieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activitati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activitati_Destinatii_DestinatieId",
                        column: x => x.DestinatieId,
                        principalTable: "Destinatii",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Inchirieri_Masini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinatieId = table.Column<int>(type: "int", nullable: false),
                    Model_Masina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tip_Masina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmisie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret_pe_zi = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inchirieri_Masini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inchirieri_Masini_Destinatii_DestinatieId",
                        column: x => x.DestinatieId,
                        principalTable: "Destinatii",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zboruri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Companie_Aeriana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Durata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DestinatieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zboruri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zboruri_Destinatii_DestinatieId",
                        column: x => x.DestinatieId,
                        principalTable: "Destinatii",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Hoteluri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stele = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,1)", nullable: false),
                    PretBaza = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImaginiGalerie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreWiFi = table.Column<bool>(type: "bit", nullable: false),
                    ArePiscina = table.Column<bool>(type: "bit", nullable: false),
                    AreParcare = table.Column<bool>(type: "bit", nullable: false),
                    AreMicDejun = table.Column<bool>(type: "bit", nullable: false),
                    AreAerConditionat = table.Column<bool>(type: "bit", nullable: false),
                    AreFitness = table.Column<bool>(type: "bit", nullable: false),
                    AreSpa = table.Column<bool>(type: "bit", nullable: false),
                    PermiteAnimale = table.Column<bool>(type: "bit", nullable: false),
                    DestinatieId = table.Column<int>(type: "int", nullable: true),
                    FacilitateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteluri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hoteluri_Destinatii_DestinatieId",
                        column: x => x.DestinatieId,
                        principalTable: "Destinatii",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Hoteluri_Facilitati_FacilitateId",
                        column: x => x.FacilitateId,
                        principalTable: "Facilitati",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TipServiciu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiciuId = table.Column<int>(type: "int", nullable: false),
                    DetaliiServiciu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PretTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataRezervare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusPlata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInceput = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSfarsit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodaPlata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facilitati = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervari_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Tip_Camera = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capacitate = table.Column<int>(type: "int", nullable: false),
                    Pret_pe_noapte = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Camere_Hoteluri_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteluri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recenzii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    Scor = table.Column<double>(type: "float", nullable: false),
                    Pareri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data_postarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recenzii_Hoteluri_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteluri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activitati_DestinatieId",
                table: "Activitati",
                column: "DestinatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Camere_HotelId",
                table: "Camere",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_DestinatieId",
                table: "Hoteluri",
                column: "DestinatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_FacilitateId",
                table: "Hoteluri",
                column: "FacilitateId");

            migrationBuilder.CreateIndex(
                name: "IX_Inchirieri_Masini_DestinatieId",
                table: "Inchirieri_Masini",
                column: "DestinatieId");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_HotelId",
                table: "Recenzii",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_UserId",
                table: "Rezervari",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Zboruri_DestinatieId",
                table: "Zboruri",
                column: "DestinatieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activitati");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "Inchirieri_Masini");

            migrationBuilder.DropTable(
                name: "Recenzii");

            migrationBuilder.DropTable(
                name: "Rezervari");

            migrationBuilder.DropTable(
                name: "Zboruri");

            migrationBuilder.DropTable(
                name: "Hoteluri");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Destinatii");

            migrationBuilder.DropTable(
                name: "Facilitati");
        }
    }
}
