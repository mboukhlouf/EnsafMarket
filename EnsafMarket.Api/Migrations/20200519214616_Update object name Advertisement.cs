using Microsoft.EntityFrameworkCore.Migrations;

namespace EnsafMarket.Api.Migrations
{
    public partial class UpdateobjectnameAdvertisement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adertisement_User_OwnerId",
                table: "Adertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Adertisement_AdvertisementId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactFeedback_Adertisement_AdvertisementId",
                table: "ContactFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adertisement",
                table: "Adertisement");

            migrationBuilder.RenameTable(
                name: "Adertisement",
                newName: "Advertisement");

            migrationBuilder.RenameIndex(
                name: "IX_Adertisement_OwnerId",
                table: "Advertisement",
                newName: "IX_Advertisement_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisement",
                table: "Advertisement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisement_User_OwnerId",
                table: "Advertisement",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Advertisement_AdvertisementId",
                table: "Contact",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactFeedback_Advertisement_AdvertisementId",
                table: "ContactFeedback",
                column: "AdvertisementId",
                principalTable: "Advertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisement_User_OwnerId",
                table: "Advertisement");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Advertisement_AdvertisementId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactFeedback_Advertisement_AdvertisementId",
                table: "ContactFeedback");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisement",
                table: "Advertisement");

            migrationBuilder.RenameTable(
                name: "Advertisement",
                newName: "Adertisement");

            migrationBuilder.RenameIndex(
                name: "IX_Advertisement_OwnerId",
                table: "Adertisement",
                newName: "IX_Adertisement_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adertisement",
                table: "Adertisement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adertisement_User_OwnerId",
                table: "Adertisement",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Adertisement_AdvertisementId",
                table: "Contact",
                column: "AdvertisementId",
                principalTable: "Adertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactFeedback_Adertisement_AdvertisementId",
                table: "ContactFeedback",
                column: "AdvertisementId",
                principalTable: "Adertisement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
