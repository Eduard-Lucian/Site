using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class FluentApiSecuritateCorectat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId",
                table: "Activitati");

            migrationBuilder.DropForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId",
                table: "Hoteluri");

            migrationBuilder.DropForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId",
                table: "Zboruri");

            migrationBuilder.AddColumn<int>(
                name: "DestinatieId1",
                table: "Zboruri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinatieId1",
                table: "Hoteluri",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinatieId1",
                table: "Activitati",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zboruri_DestinatieId1",
                table: "Zboruri",
                column: "DestinatieId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_DestinatieId1",
                table: "Hoteluri",
                column: "DestinatieId1");

            migrationBuilder.CreateIndex(
                name: "IX_Activitati_DestinatieId1",
                table: "Activitati",
                column: "DestinatieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId",
                table: "Activitati",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId1",
                table: "Activitati",
                column: "DestinatieId1",
                principalTable: "Destinatii",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId",
                table: "Hoteluri",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId1",
                table: "Hoteluri",
                column: "DestinatieId1",
                principalTable: "Destinatii",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId",
                table: "Zboruri",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId1",
                table: "Zboruri",
                column: "DestinatieId1",
                principalTable: "Destinatii",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId",
                table: "Activitati");

            migrationBuilder.DropForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId1",
                table: "Activitati");

            migrationBuilder.DropForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId",
                table: "Hoteluri");

            migrationBuilder.DropForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId1",
                table: "Hoteluri");

            migrationBuilder.DropForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId",
                table: "Zboruri");

            migrationBuilder.DropForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId1",
                table: "Zboruri");

            migrationBuilder.DropIndex(
                name: "IX_Zboruri_DestinatieId1",
                table: "Zboruri");

            migrationBuilder.DropIndex(
                name: "IX_Hoteluri_DestinatieId1",
                table: "Hoteluri");

            migrationBuilder.DropIndex(
                name: "IX_Activitati_DestinatieId1",
                table: "Activitati");

            migrationBuilder.DropColumn(
                name: "DestinatieId1",
                table: "Zboruri");

            migrationBuilder.DropColumn(
                name: "DestinatieId1",
                table: "Hoteluri");

            migrationBuilder.DropColumn(
                name: "DestinatieId1",
                table: "Activitati");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitati_Destinatii_DestinatieId",
                table: "Activitati",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hoteluri_Destinatii_DestinatieId",
                table: "Hoteluri",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Zboruri_Destinatii_DestinatieId",
                table: "Zboruri",
                column: "DestinatieId",
                principalTable: "Destinatii",
                principalColumn: "Id");
        }
    }
}
