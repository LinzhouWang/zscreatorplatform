using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZSCreatorPlatform.Web.Admin.Migrations
{
    public partial class init022410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "T_User",
                comment: "用户表");

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "T_User",
                nullable: false,
                defaultValue: 0,
                comment: "所属分店id");

            migrationBuilder.CreateTable(
                name: "T_Store",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, comment: "主键id")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true, comment: "分店名称"),
                    Address = table.Column<string>(maxLength: 100, nullable: true, comment: "分店地址")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Store", x => x.Id);
                },
                comment: "分店表");

            migrationBuilder.CreateIndex(
                name: "IX_T_User_StoreId",
                table: "T_User",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_T_User_T_Store_StoreId",
                table: "T_User",
                column: "StoreId",
                principalTable: "T_Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_User_T_Store_StoreId",
                table: "T_User");

            migrationBuilder.DropTable(
                name: "T_Store");

            migrationBuilder.DropIndex(
                name: "IX_T_User_StoreId",
                table: "T_User");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "T_User");

            migrationBuilder.AlterTable(
                name: "T_User",
                oldComment: "用户表");
        }
    }
}
