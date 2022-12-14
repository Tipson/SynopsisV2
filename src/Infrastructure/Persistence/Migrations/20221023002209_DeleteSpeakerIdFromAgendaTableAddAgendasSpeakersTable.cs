using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synopsis.Migrations
{
    public partial class DeleteSpeakerIdFromAgendaTableAddAgendasSpeakersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Speakers_SpeakerId",
                table: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_SpeakerId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "Agendas");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Speakers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AgendaRowSpeakerRow",
                columns: table => new
                {
                    AgendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpeakersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaRowSpeakerRow", x => new { x.AgendasId, x.SpeakersId });
                    table.ForeignKey(
                        name: "FK_AgendaRowSpeakerRow_Agendas_AgendasId",
                        column: x => x.AgendasId,
                        principalTable: "Agendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendaRowSpeakerRow_Speakers_SpeakersId",
                        column: x => x.SpeakersId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaRowSpeakerRow_SpeakersId",
                table: "AgendaRowSpeakerRow",
                column: "SpeakersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaRowSpeakerRow");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Speakers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpeakerId",
                table: "Agendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_SpeakerId",
                table: "Agendas",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Speakers_SpeakerId",
                table: "Agendas",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
