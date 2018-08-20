using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OryxWestAfrica.Data.Migrations
{
    public partial class checkers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckedImage");

            migrationBuilder.CreateTable(
                name: "CheckUpdate",
                columns: table => new
                {
                    CheckUpdateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PictureName = table.Column<string>(nullable: true),
                    Assigned = table.Column<bool>(nullable: false),
                    PictureID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckUpdate", x => x.CheckUpdateID);
                    table.ForeignKey(
                        name: "FK_CheckUpdate_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckUpdate_PictureID",
                table: "CheckUpdate",
                column: "PictureID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckUpdate");

            migrationBuilder.CreateTable(
                name: "CheckedImage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PictureID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckedImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckedImage_Pictures_PictureID",
                        column: x => x.PictureID,
                        principalTable: "Pictures",
                        principalColumn: "PictureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckedImage_PictureID",
                table: "CheckedImage",
                column: "PictureID");
        }
    }
}
