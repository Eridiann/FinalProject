using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RootRPGAPI.Migrations
{
    public partial class Harmfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "WeaponSkills");

            migrationBuilder.DropColumn(
                name: "DamageType",
                table: "WeaponSkills");

            migrationBuilder.AddColumn<string>(
                name: "HarmType",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "HarmValue",
                table: "Equipment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HarmType",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "HarmValue",
                table: "Equipment");

            migrationBuilder.AddColumn<long>(
                name: "Amount",
                table: "WeaponSkills",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DamageType",
                table: "WeaponSkills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
