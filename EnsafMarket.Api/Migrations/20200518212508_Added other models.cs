using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnsafMarket.Api.Migrations
{
    public partial class Addedothermodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Cne = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Major = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Hobbies = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adertisement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adertisement_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactFeedback",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(nullable: false),
                    Feedback = table.Column<string>(nullable: true),
                    AdvertisementId = table.Column<int>(nullable: false),
                    FromUserId = table.Column<int>(nullable: true),
                    ToUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactFeedback_Adertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Adertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactFeedback_User_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactFeedback_User_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AdvertisementId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    OwnerFeedbackId = table.Column<int>(nullable: true),
                    UserFeedbackId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Adertisement_AdvertisementId",
                        column: x => x.AdvertisementId,
                        principalTable: "Adertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_ContactFeedback_OwnerFeedbackId",
                        column: x => x.OwnerFeedbackId,
                        principalTable: "ContactFeedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_ContactFeedback_UserFeedbackId",
                        column: x => x.UserFeedbackId,
                        principalTable: "ContactFeedback",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    ContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactMessage_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactMessage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adertisement_OwnerId",
                table: "Adertisement",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AdvertisementId",
                table: "Contact",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OwnerFeedbackId",
                table: "Contact",
                column: "OwnerFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserFeedbackId",
                table: "Contact",
                column: "UserFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeedback_AdvertisementId",
                table: "ContactFeedback",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeedback_FromUserId",
                table: "ContactFeedback",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeedback_ToUserId",
                table: "ContactFeedback",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessage_ContactId",
                table: "ContactMessage",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessage_UserId",
                table: "ContactMessage",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessage");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "ContactFeedback");

            migrationBuilder.DropTable(
                name: "Adertisement");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
