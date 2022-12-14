using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synopsis.Migrations
{
    public partial class AddedSynopsisTypeToParnersTableAndAddedDependencyOnPartnersTableToSpeakersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Partners_PartnerRowId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_PartnerRowId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "PartnerRowId",
                table: "Speakers");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Speakers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<int>(
                name: "IsShow",
                table: "Partners",
                type: "int",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((1))")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AddColumn<int>(
                name: "SynopsisType",
                table: "Partners",
                type: "int",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_PartnerId",
                table: "Speakers",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Partners_PartnerId",
                table: "Speakers",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speakers_Partners_PartnerId",
                table: "Speakers");

            migrationBuilder.DropIndex(
                name: "IX_Speakers_PartnerId",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "SynopsisType",
                table: "Partners");

            migrationBuilder.AlterColumn<int>(
                name: "PartnerId",
                table: "Speakers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PartnerRowId",
                table: "Speakers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "URL",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<int>(
                name: "IsShow",
                table: "Partners",
                type: "int",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((1))")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_PartnerRowId",
                table: "Speakers",
                column: "PartnerRowId");

            migrationBuilder.AddForeignKey(
                name: "FK_Speakers_Partners_PartnerRowId",
                table: "Speakers",
                column: "PartnerRowId",
                principalTable: "Partners",
                principalColumn: "ID");
        }
    }
}
