using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KonnektuTask.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SecretKey = table.Column<string>(nullable: true, defaultValueSql: "md5(random()::text)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GivenName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PersonalDataAgree = table.Column<bool>(nullable: false),
                    EmailSubscribeAgree = table.Column<bool>(nullable: false),
                    GenderId = table.Column<string>(nullable: true),
                    SecretKey = table.Column<string>(nullable: true, defaultValueSql: "md5(random()::text)"),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    SourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Sources",
                column: "Id",
                values: new object[]
                {
                    new Guid("abb6793a-e143-48b0-a80e-2c13cf40d147"),
                    new Guid("49cb7c18-41c0-4205-9155-fa46594b944b")
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_SourceId",
                table: "Users",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
