using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _241419_DynamicWebsite.Migrations
{
    /// <inheritdoc />
    public partial class AddWebsiteContentAndContactRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeroEyebrow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeroTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    HeroDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HeroCtaText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeroCtaTarget = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeroBadge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HeroCardMark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AboutEyebrow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AboutTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AboutDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AboutSecondaryDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ServicesEyebrow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServicesTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactEyebrow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ContactDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CeoEyebrow = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CeoTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ChiefExecutiveName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ChiefExecutiveTitle = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ChiefExecutiveDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyContentId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSummaries_CompanyContents_CompanyContentId",
                        column: x => x.CompanyContentId,
                        principalTable: "CompanyContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSummaries_CompanyContentId",
                table: "ServiceSummaries",
                column: "CompanyContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactRequests");

            migrationBuilder.DropTable(
                name: "ServiceSummaries");

            migrationBuilder.DropTable(
                name: "CompanyContents");
        }
    }
}
