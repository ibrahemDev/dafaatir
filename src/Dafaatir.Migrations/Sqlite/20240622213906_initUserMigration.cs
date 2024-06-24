using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dafaatir.Migrations.Sqlite
{
    /// <inheritdoc />
    public partial class initUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    display_name = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    email_verified = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "INTEGER", nullable: false),
                    permission_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    role_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "access_tokens",
                columns: table => new
                {
                    access_token_id = table.Column<long>(type: "INTEGER", nullable: false),
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    refresh_token_id = table.Column<long>(type: "INTEGER", nullable: false),
                    secret_part = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    expires_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_access_tokens", x => x.access_token_id);
                    table.ForeignKey(
                        name: "FK_access_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    refresh_token_id = table.Column<long>(type: "INTEGER", nullable: false),
                    user_id = table.Column<long>(type: "INTEGER", nullable: false),
                    secret_part = table.Column<string>(type: "TEXT", nullable: false),
                    data = table.Column<string>(type: "TEXT", nullable: false),
                    created_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    expires_at = table.Column<DateTime>(type: "TEXT", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CurrentAccessTokenEntityId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.refresh_token_id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_access_tokens_CurrentAccessTokenEntityId",
                        column: x => x.CurrentAccessTokenEntityId,
                        principalTable: "access_tokens",
                        principalColumn: "access_token_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_access_tokens_refresh_token_id",
                table: "access_tokens",
                column: "refresh_token_id");

            migrationBuilder.CreateIndex(
                name: "IX_access_tokens_secret_part",
                table: "access_tokens",
                column: "secret_part",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_access_tokens_user_id",
                table: "access_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_name",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_CurrentAccessTokenEntityId",
                table: "refresh_tokens",
                column: "CurrentAccessTokenEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_secret_part",
                table: "refresh_tokens",
                column: "secret_part",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_name",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_id",
                table: "users",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_access_tokens_refresh_tokens_refresh_token_id",
                table: "access_tokens",
                column: "refresh_token_id",
                principalTable: "refresh_tokens",
                principalColumn: "refresh_token_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_tokens_refresh_tokens_refresh_token_id",
                table: "access_tokens");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "access_tokens");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
