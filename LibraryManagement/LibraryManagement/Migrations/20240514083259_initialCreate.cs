using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Books_BookID",
                table: "BorrowingRecords");

            migrationBuilder.DropColumn(
                name: "BoodId",
                table: "BorrowingRecords");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "BorrowingRecords",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowingRecords_BookID",
                table: "BorrowingRecords",
                newName: "IX_BorrowingRecords_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowingRecords_Books_BookId",
                table: "BorrowingRecords");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BorrowingRecords",
                newName: "BookID");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowingRecords_BookId",
                table: "BorrowingRecords",
                newName: "IX_BorrowingRecords_BookID");

            migrationBuilder.AddColumn<int>(
                name: "BoodId",
                table: "BorrowingRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowingRecords_Books_BookID",
                table: "BorrowingRecords",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
