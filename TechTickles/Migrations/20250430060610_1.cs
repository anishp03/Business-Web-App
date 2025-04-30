using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechTickles.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.AccountId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripton = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<double>(type: "double", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.PlanId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<long>(type: "bigint", nullable: false),
                    Expiration = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CVV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Customers",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Customers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Customers",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Cart_Customers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Customers",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CustomerSubscriptions",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSubscriptions", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptions_Customers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Customers",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSubscriptions_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanFeatures",
                columns: table => new
                {
                    FeatureFeatureId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanFeatures", x => new { x.FeatureFeatureId, x.SubscriptionPlanPlanId });
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanFeatures_Features_FeatureFeatureId",
                        column: x => x.FeatureFeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanFeatures_SubscriptionPlans_SubscriptionPlanP~",
                        column: x => x.SubscriptionPlanPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "FeatureId", "Descripton", "Name" },
                values: new object[,]
                {
                    { 1, "No Features", "No Features" },
                    { 2, "Detects malware while you are on the web and as soon as a malicious site attempts to invade our software will remove it immediately", "Real-Time Malware Protection" },
                    { 3, "High-Speed secure internet connection that blocks your IP address and prevents network based attacks", "Virtual Private Network(VPN)" },
                    { 4, "Real-Time Virus Detection while you are browsing and downloading from the web, then our software will remove this without you knowing", "Real-Time Antivirus Protection" },
                    { 5, "Clean your digital footprint from spam websites and scams and preventing your digital footprint to leave a trail on all the sits you visit", "Data Scrubber" },
                    { 6, "Tracks where your information is being used and detects if your personal information has been leaked such as payment information, and key identity informaton", "Identity Theft Protection" },
                    { 7, "Prevent pop-up ads from injecting malware into your system. This software detects websites trying to give pop-up ads and prevents it from being shown", "Ad Blocker" },
                    { 8, "Scan through your computer's internal drive and history for potential malware rooted inside", "Limited Malware and Virus Scan" },
                    { 9, "Securely store your passwords and generate strong passwords for you to use. Protected by our own database your passwords will be protected from being leaked", "Password Manager" },
                    { 10, "Permanently delete files from your computer and prevent them from being recovered by hackers. Important for confidential files related to your personal data", "Secure File Shredder" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "PlanId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "You are not subscribed to any plan!", "None", 0.0 },
                    { 2, "This is a basic plan that includes featues that are essential to protect your information on the web like: Ad Blocker, Limited Malware and Virus Scans, and an efficient Password Manager", "Tickles Basic", 50.0 },
                    { 3, "Middle tier plan providing more security measures for users that are more active on the web. This plan includes all the features from the Basic plan and adds: Virtual Private Network(VPN), Identity Theft Protection, and a Secure File Shredder", "Tickles Plus", 89.0 },
                    { 4, "Top of the line plan with every security measure taken to protect your information on the web. This plan includes all of the features of the other plans and adds : Data Scrubber, Real-Time Malware and Virus Protection", "Tickles Ultimate", 101.0 }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlanFeatures",
                columns: new[] { "FeatureFeatureId", "SubscriptionPlanPlanId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 4 },
                    { 3, 3 },
                    { 3, 4 },
                    { 4, 4 },
                    { 5, 4 },
                    { 6, 3 },
                    { 6, 4 },
                    { 7, 2 },
                    { 7, 3 },
                    { 7, 4 },
                    { 8, 2 },
                    { 8, 3 },
                    { 8, 4 },
                    { 9, 2 },
                    { 9, 3 },
                    { 9, 4 },
                    { 10, 3 },
                    { 10, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_PlanId",
                table: "Cart",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserName",
                table: "Customers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSubscriptions_PlanId",
                table: "CustomerSubscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AccountId",
                table: "Reviews",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanFeatures_SubscriptionPlanPlanId",
                table: "SubscriptionPlanFeatures",
                column: "SubscriptionPlanPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "CustomerSubscriptions");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanFeatures");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
