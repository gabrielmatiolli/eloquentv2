using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloquentBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSubscriptionAndPerksStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerkValue",
                table: "Subscriptions",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerkValue",
                table: "Subscriptions");
        }
    }
}
