using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synopsis.Migrations
{
    public partial class AddedTopicRuAndTopicEnToAgendasTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgendaRow_Speakers_SpeakerId",
                table: "AgendaRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgendaRow",
                table: "AgendaRow");

            migrationBuilder.RenameTable(
                name: "AgendaRow",
                newName: "Agendas");

            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Agendas",
                newName: "TopicRu");

            migrationBuilder.RenameIndex(
                name: "IX_AgendaRow_SpeakerId",
                table: "Agendas",
                newName: "IX_Agendas_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_AgendaRow_Id",
                table: "Agendas",
                newName: "IX_Agendas_Id");

            migrationBuilder.AddColumn<string>(
                name: "TopicEn",
                table: "Agendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendas",
                table: "Agendas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Speakers_SpeakerId",
                table: "Agendas",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Speakers_SpeakerId",
                table: "Agendas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendas",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "TopicEn",
                table: "Agendas");

            migrationBuilder.RenameTable(
                name: "Agendas",
                newName: "AgendaRow");

            migrationBuilder.RenameColumn(
                name: "TopicRu",
                table: "AgendaRow",
                newName: "Topic");

            migrationBuilder.RenameIndex(
                name: "IX_Agendas_SpeakerId",
                table: "AgendaRow",
                newName: "IX_AgendaRow_SpeakerId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendas_Id",
                table: "AgendaRow",
                newName: "IX_AgendaRow_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgendaRow",
                table: "AgendaRow",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgendaRow_Speakers_SpeakerId",
                table: "AgendaRow",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
