using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GroceryListAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grocery_items",
                columns: table => new
                {
                    grocery_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    item_name = table.Column<string>(type: "text", nullable: true),
                    done_tf = table.Column<bool>(type: "boolean", nullable: false),
                    created_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    created_by_name = table.Column<string>(type: "text", nullable: true),
                    last_modified_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: true),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_items", x => x.grocery_item_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name_email = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    middle_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    created_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    created_by_name = table.Column<string>(type: "text", nullable: true),
                    last_modified_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: true),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "grocery_lists",
                columns: table => new
                {
                    grocery_list_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    created_by_name = table.Column<string>(type: "text", nullable: true),
                    last_modified_dt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    last_modified_by_id = table.Column<string>(type: "text", nullable: true),
                    last_modified_by_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grocery_lists", x => x.grocery_list_id);
                    table.ForeignKey(
                        name: "fk_grocery_lists_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_grocery_lists_user_id",
                table: "grocery_lists",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "grocery_items");

            migrationBuilder.DropTable(
                name: "grocery_lists");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
