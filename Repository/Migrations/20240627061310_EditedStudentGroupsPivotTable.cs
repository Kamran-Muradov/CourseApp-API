using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class EditedStudentGroupsPivotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroup_Groups_GroupId",
                table: "StudentGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroup_Students_StudentId",
                table: "StudentGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup");

            migrationBuilder.RenameTable(
                name: "StudentGroup",
                newName: "StudentGroups");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroup_StudentId",
                table: "StudentGroups",
                newName: "IX_StudentGroups_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroup_GroupId",
                table: "StudentGroups",
                newName: "IX_StudentGroups_GroupId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroups",
                table: "StudentGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Groups_GroupId",
                table: "StudentGroups",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Students_StudentId",
                table: "StudentGroups",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Groups_GroupId",
                table: "StudentGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Students_StudentId",
                table: "StudentGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentGroups",
                table: "StudentGroups");

            migrationBuilder.RenameTable(
                name: "StudentGroups",
                newName: "StudentGroup");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroups_StudentId",
                table: "StudentGroup",
                newName: "IX_StudentGroup_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentGroups_GroupId",
                table: "StudentGroup",
                newName: "IX_StudentGroup_GroupId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentGroup",
                table: "StudentGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroup_Groups_GroupId",
                table: "StudentGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroup_Students_StudentId",
                table: "StudentGroup",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
