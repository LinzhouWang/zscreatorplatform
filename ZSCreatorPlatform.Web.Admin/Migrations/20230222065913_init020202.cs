using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZSCreatorPlatform.Web.Admin.Migrations
{
    public partial class init020202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "T_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "用户创建时间");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "T_User",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "用户最后修改时间");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "T_User");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "T_User");
        }
    }
}
