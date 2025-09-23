using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Junior.Migrations
{
    /// <inheritdoc />
    public partial class minorCorrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Article_ArticleId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_Article_Order_OrderId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ArticleId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_Article_ArticleId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Article",
                newName: "OrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_OrderId",
                table: "Article",
                newName: "IX_Article_OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_OrderItem_OrderItemId",
                table: "Article",
                column: "OrderItemId",
                principalTable: "OrderItem",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_OrderItem_OrderItemId",
                table: "Article");

            migrationBuilder.RenameColumn(
                name: "OrderItemId",
                table: "Article",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_OrderItemId",
                table: "Article",
                newName: "IX_Article_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Article",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ArticleId",
                table: "OrderItem",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleId",
                table: "Article",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Article_ArticleId",
                table: "Article",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Order_OrderId",
                table: "Article",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Article_ArticleId",
                table: "OrderItem",
                column: "ArticleId",
                principalTable: "Article",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
