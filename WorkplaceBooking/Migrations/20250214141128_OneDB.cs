using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkplaceBooking.Migrations
{
    /// <inheritdoc />
    public partial class OneDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatBookings");

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "UserProfiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SeatNumber",
                table: "UserProfiles",
                column: "SeatNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_SeatNumber",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "UserProfiles");

            migrationBuilder.CreateTable(
                name: "SeatBookings",
                columns: table => new
                {
                    SeatNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatBookings", x => x.SeatNumber);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatBookings_SeatNumber",
                table: "SeatBookings",
                column: "SeatNumber",
                unique: true);
        }
    }
}
