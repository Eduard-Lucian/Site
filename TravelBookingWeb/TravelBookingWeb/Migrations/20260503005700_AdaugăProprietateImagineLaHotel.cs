using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class AdaugăProprietateImagineLaHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagine",
                table: "Hoteluri",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagine",
                table: "Hoteluri");
        }
    }
}
