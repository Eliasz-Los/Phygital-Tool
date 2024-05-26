using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Phygital.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ImageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Reactions_ReactionId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Participation_ParticipationId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Installation_InstallationId",
                table: "Session");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Participation_ParticipationId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_ParticipationId",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Likes_ReactionId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ParticipationId",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PostReactions");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "PostLikes");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Participation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "ReactionId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "InstallationId",
                table: "Session",
                newName: "installationId");

            migrationBuilder.RenameIndex(
                name: "IX_Session_InstallationId",
                table: "Session",
                newName: "IX_Session_installationId");

            migrationBuilder.RenameColumn(
                name: "ParticipationId",
                table: "Note",
                newName: "participationId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_ParticipationId",
                table: "Note",
                newName: "IX_Note_participationId");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Installation",
                newName: "Street");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Reactions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Posts",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostTime",
                table: "Posts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Participation",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Participation",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "sessionId",
                table: "Participation",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Installation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PostalCode",
                table: "Installation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Installation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "organisationId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Session_Id",
                table: "Session",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_AccountId",
                table: "Reactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId",
                table: "Posts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Participation_sessionId",
                table: "Participation",
                column: "sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Note_Id",
                table: "Note",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AccountId",
                table: "Likes",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Installation_Id",
                table: "Installation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_organisationId",
                table: "AspNetUsers",
                column: "organisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_id",
                table: "Organisations",
                column: "id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_organisationId",
                table: "AspNetUsers",
                column: "organisationId",
                principalTable: "Organisations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AccountId",
                table: "Likes",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Participation_participationId",
                table: "Note",
                column: "participationId",
                principalTable: "Participation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Session_sessionId",
                table: "Participation",
                column: "sessionId",
                principalTable: "Session",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_AspNetUsers_AccountId",
                table: "Reactions",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Installation_installationId",
                table: "Session",
                column: "installationId",
                principalTable: "Installation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_organisationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AccountId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Note_Participation_participationId",
                table: "Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Session_sessionId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_AspNetUsers_AccountId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Installation_installationId",
                table: "Session");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Session_Id",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_AccountId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Participation_sessionId",
                table: "Participation");

            migrationBuilder.DropIndex(
                name: "IX_Note_Id",
                table: "Note");

            migrationBuilder.DropIndex(
                name: "IX_Likes_AccountId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Installation_Id",
                table: "Installation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_organisationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Participation");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Participation");

            migrationBuilder.DropColumn(
                name: "sessionId",
                table: "Participation");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Installation");

            migrationBuilder.DropColumn(
                name: "organisationId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "installationId",
                table: "Session",
                newName: "InstallationId");

            migrationBuilder.RenameIndex(
                name: "IX_Session_installationId",
                table: "Session",
                newName: "IX_Session_InstallationId");

            migrationBuilder.RenameColumn(
                name: "participationId",
                table: "Note",
                newName: "ParticipationId");

            migrationBuilder.RenameIndex(
                name: "IX_Note_participationId",
                table: "Note",
                newName: "IX_Note_ParticipationId");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Installation",
                newName: "Location");

            migrationBuilder.AddColumn<long>(
                name: "ParticipationId",
                table: "Session",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Posts",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PostReactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "PostLikes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Duration",
                table: "Participation",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Likes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ReactionId",
                table: "Likes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Session_ParticipationId",
                table: "Session",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ReactionId",
                table: "Likes",
                column: "ReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Reactions_ReactionId",
                table: "Likes",
                column: "ReactionId",
                principalTable: "Reactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Note_Participation_ParticipationId",
                table: "Note",
                column: "ParticipationId",
                principalTable: "Participation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Installation_InstallationId",
                table: "Session",
                column: "InstallationId",
                principalTable: "Installation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Participation_ParticipationId",
                table: "Session",
                column: "ParticipationId",
                principalTable: "Participation",
                principalColumn: "Id");
        }
    }
}
