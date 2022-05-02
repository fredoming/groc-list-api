using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryListAPI.Migrations
{
    public partial class FixFkGrocList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_grocery_lists_users_user_id",
                table: "grocery_lists");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "grocery_lists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_grocery_lists_users_user_id",
                table: "grocery_lists",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_grocery_lists_users_user_id",
                table: "grocery_lists");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "grocery_lists",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_grocery_lists_users_user_id",
                table: "grocery_lists",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
