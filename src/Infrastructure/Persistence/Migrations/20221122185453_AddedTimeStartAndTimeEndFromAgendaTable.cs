using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synopsis.Migrations
{
    public partial class AddedTimeStartAndTimeEndFromAgendaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Agendas",
                newName: "StartTime");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndTime",
                table: "Agendas",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Agendas");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Agendas",
                newName: "Time");
        }
    }
}
