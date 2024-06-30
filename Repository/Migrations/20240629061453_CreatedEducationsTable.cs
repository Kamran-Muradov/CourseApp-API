using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class CreatedEducationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationId",
                table: "Groups",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Educations_EducationId",
                table: "Groups",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Educations_EducationId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EducationId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Groups");
        }
    }
}
