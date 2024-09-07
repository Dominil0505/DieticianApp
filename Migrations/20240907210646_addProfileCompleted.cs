using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class addProfileCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_profile_completed",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Is_profile_completed",
                table: "Dieticians");

            migrationBuilder.AddColumn<bool>(
                name: "Is_profile_completed",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_profile_completed",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Is_profile_completed",
                table: "Patients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Is_profile_completed",
                table: "Dieticians",
                type: "bit",
                nullable: true);
        }
    }
}
