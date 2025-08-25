using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corelia.DataLake.Dashboard.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class addWorkspaceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    color = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    is_archived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    is_personal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserWorkspace",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WorkspaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserWorkspace", x => new { x.MembersId, x.WorkspaceId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserWorkspace_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserWorkspace_Workspace_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserWorkspace_WorkspaceId",
                table: "ApplicationUserWorkspace",
                column: "WorkspaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserWorkspace");

            migrationBuilder.DropTable(
                name: "Workspace");
        }
    }
}
