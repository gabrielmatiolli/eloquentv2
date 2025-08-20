using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloquentBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeOfValueOfSubscriptionPerk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BooleanValue",
                table: "SubscriptionPerks");

            migrationBuilder.RenameColumn(
                name: "NumericValue",
                table: "SubscriptionPerks",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "SubscriptionPerks",
                newName: "NumericValue");

            migrationBuilder.AddColumn<bool>(
                name: "BooleanValue",
                table: "SubscriptionPerks",
                type: "boolean",
                nullable: true);
        }
    }
}
