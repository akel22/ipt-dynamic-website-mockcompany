using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _241419_DynamicWebsite.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "ServiceSummaries");

            migrationBuilder.DropColumn(
                name: "HeroBadge",
                table: "CompanyContents");

            migrationBuilder.DropColumn(
                name: "HeroCardMark",
                table: "CompanyContents");

            migrationBuilder.DropColumn(
                name: "HeroEyebrow",
                table: "CompanyContents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "ServiceSummaries",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeroBadge",
                table: "CompanyContents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeroCardMark",
                table: "CompanyContents",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HeroEyebrow",
                table: "CompanyContents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
