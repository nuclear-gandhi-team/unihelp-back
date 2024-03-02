using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniHelp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangingStructureOfComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Comments");
        }
    }
}
