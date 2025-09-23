using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Junior.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_OrderItem_OrderItemId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_OrderItemId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Article");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ArticleId",
                table: "OrderItem",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ArticleId",
                table: "OrderItem");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Article",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_OrderItemId",
                table: "Article",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_OrderItem_OrderItemId",
                table: "Article",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id");
        }
    }
}
