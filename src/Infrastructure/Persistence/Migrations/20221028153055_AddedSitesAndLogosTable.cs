using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synopsis.Migrations
{
    public partial class AddedSitesAndLogosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BiographyEn",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BiographyRu",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medium",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Speakers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logos",
                columns: table => new
                {
                    SpeakerId = table.Column<int>(type: "int", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logos", x => new { x.Logo, x.SpeakerId });
                    table.ForeignKey(
                        name: "FK_Logos_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SpeakerId = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyNameRu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => new { x.SpeakerId, x.Link });
                    table.ForeignKey(
                        name: "FK_Sites_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logos_SpeakerId_Logo",
                table: "Logos",
                columns: new[] { "SpeakerId", "Logo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sites_SpeakerId_Link",
                table: "Sites",
                columns: new[] { "SpeakerId", "Link" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logos");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropColumn(
                name: "BiographyEn",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "BiographyRu",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Speakers");
        }
    }
}
