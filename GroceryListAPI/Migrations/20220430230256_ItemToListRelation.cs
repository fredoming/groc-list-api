using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryListAPI.Migrations
{
    public partial class ItemToListRelation : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "grocery_list_id",
                table: "grocery_items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_grocery_items_grocery_list_id",
                table: "grocery_items",
                column: "grocery_list_id");

            migrationBuilder.AddForeignKey(
                name: "fk_grocery_items_grocery_lists_grocery_list_id",
                table: "grocery_items",
                column: "grocery_list_id",
                principalTable: "grocery_lists",
                principalColumn: "grocery_list_id",
                onDelete: ReferentialAction.Cascade);

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
                name: "fk_grocery_items_grocery_lists_grocery_list_id",
                table: "grocery_items");

            migrationBuilder.DropForeignKey(
                name: "fk_grocery_lists_users_user_id",
                table: "grocery_lists");

            migrationBuilder.DropIndex(
                name: "ix_grocery_items_grocery_list_id",
                table: "grocery_items");

            migrationBuilder.DropColumn(
                name: "grocery_list_id",
                table: "grocery_items");

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
