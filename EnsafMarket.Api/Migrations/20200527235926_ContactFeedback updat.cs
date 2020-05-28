using Microsoft.EntityFrameworkCore.Migrations;

namespace EnsafMarket.Api.Migrations
{
    public partial class ContactFeedbackupdat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactFeedback_OwnerFeedbackId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactFeedback_UserFeedbackId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactFeedback_Advertisement_AdvertisementId",
                table: "ContactFeedback");

            migrationBuilder.DropIndex(
                name: "IX_ContactFeedback_AdvertisementId",
                table: "ContactFeedback");

            migrationBuilder.DropIndex(
                name: "IX_Contact_OwnerFeedbackId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_UserFeedbackId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "ContactFeedback");

            migrationBuilder.DropColumn(
                name: "OwnerFeedbackId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "UserFeedbackId",
                table: "Contact");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "ContactFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeedback_ContactId",
                table: "ContactFeedback",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactFeedback_Contact_ContactId",
                table: "ContactFeedback",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactFeedback_Contact_ContactId",
                table: "ContactFeedback");

            migrationBuilder.DropIndex(
                name: "IX_ContactFeedback_ContactId",
                table: "ContactFeedback");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "ContactFeedback");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "ContactFeedback",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerFeedbackId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserFeedbackId",
                table: "Contact",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactFeedback_AdvertisementId",
                table: "ContactFeedback",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_OwnerFeedbackId",
                table: "Contact",
                column: "OwnerFeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserFeedbackId",
                table: "Contact",
                column: "UserFeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactFeedback_OwnerFeedbackId",
                table: "Contact",
                column: "OwnerFeedbackId",
                principalTable: "ContactFeedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactFeedback_UserFeedbackId",
                table: "Contact",
                column: "UserFeedbackId",
                principalTable: "ContactFeedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactFeedback_Advertisement_AdvertisementId",
                table: "ContactFeedback",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
