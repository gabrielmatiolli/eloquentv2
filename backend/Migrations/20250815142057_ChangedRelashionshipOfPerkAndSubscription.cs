using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EloquentBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRelashionshipOfPerkAndSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerkSubscription");

            migrationBuilder.DropColumn(
                name: "PerkValue",
                table: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "SubscriptionPerks",
                columns: table => new
                {
                    SubscriptionId = table.Column<int>(type: "integer", nullable: false),
                    PerkId = table.Column<int>(type: "integer", nullable: false),
                    NumericValue = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    BooleanValue = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPerks", x => new { x.SubscriptionId, x.PerkId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPerks_Perks_PerkId",
                        column: x => x.PerkId,
                        principalTable: "Perks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionPerks_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPerks_PerkId",
                table: "SubscriptionPerks",
                column: "PerkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPerks");

            migrationBuilder.AddColumn<int>(
                name: "PerkValue",
                table: "Subscriptions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PerkSubscription",
                columns: table => new
                {
                    PerksId = table.Column<int>(type: "integer", nullable: false),
                    SubscriptionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerkSubscription", x => new { x.PerksId, x.SubscriptionsId });
                    table.ForeignKey(
                        name: "FK_PerkSubscription_Perks_PerksId",
                        column: x => x.PerksId,
                        principalTable: "Perks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerkSubscription_Subscriptions_SubscriptionsId",
                        column: x => x.SubscriptionsId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerkSubscription_SubscriptionsId",
                table: "PerkSubscription",
                column: "SubscriptionsId");
        }
    }
}
