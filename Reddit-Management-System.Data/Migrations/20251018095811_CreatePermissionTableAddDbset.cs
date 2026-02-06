using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissionTableAddDbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRole_Permission_PermissionsId",
                schema: "public",
                table: "PermissionRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permission",
                schema: "public",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "Permission",
                schema: "public",
                newName: "Permissions",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permissions",
                schema: "public",
                table: "Permissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionsId",
                schema: "public",
                table: "PermissionRole",
                column: "PermissionsId",
                principalSchema: "public",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRole_Permissions_PermissionsId",
                schema: "public",
                table: "PermissionRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permissions",
                schema: "public",
                table: "Permissions");

            migrationBuilder.RenameTable(
                name: "Permissions",
                schema: "public",
                newName: "Permission",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permission",
                schema: "public",
                table: "Permission",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRole_Permission_PermissionsId",
                schema: "public",
                table: "PermissionRole",
                column: "PermissionsId",
                principalSchema: "public",
                principalTable: "Permission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
