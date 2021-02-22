using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Recipes.Service.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    About = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CategoryPicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    WhyNotConfirmed = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FoodPicture = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FoodTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PersonSize = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    SeoAuthor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SeoDescription = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    SeoTags = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PreparationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RecipeDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodCategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    WhyNotConfirmed = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recipes_FoodCategories_FoodCategoryId",
                        column: x => x.FoodCategoryId,
                        principalTable: "FoodCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CommentText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    WhyNotConfirmed = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "RoleDescription" },
                values: new object[,]
                {
                    { 1, "9841f5cd-5fed-4fdf-b1a4-595d843b9946", "Admin", "ADMIN", "Tüm yetkilere sahip kullanıcıdır." },
                    { 2, "b64572b7-d644-4bd7-a7dc-705c86ad3b1e", "Editor", "EDITOR", "Tarif ve kategori ekleyebilir fakat silemez, Kullanıcılar ve roller sayfalarını görüntüleyemez." },
                    { 3, "19d21d1d-6146-46c7-93a2-3904012acde9", "Member", "MEMBER", "Üye olan herkese atanır, tarif yazabilirler ve yorum bırakabilirler, admin veya editor tarafından onaylanması gerekir. Kullanıcılar, roller ve çöp kutusu sayfalarını görüntüleyemez." }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "IsActive", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Picture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, "1 id'li kullanıcı", 0, "da3f7766-58ad-4cf4-b5ad-5fb2c9587425", "admin@gmail.com", true, "Caner Sahin", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEKnW1z4ZEpV6cYxZ+DDzdzvL2uyAaG04oC/ScdwY3bXAD1de3FHZOYW00pXzTzYCNw==", "00905425612223", true, "default.jpg", "2ca1f5a7-2f68-4ece-ab28-615e42a1f568", false, "Admin" },
                    { 2, "2 id'li kullanıcı", 0, "044d0348-64b6-464c-88e9-fe501b9e434d", "editor@gmail.com", true, "Caner Sahin", true, false, null, "EDITOR@GMAIL.COM", "EDITOR", "AQAAAAEAACcQAAAAEOeq1ideNRnrA8ihZ1tVfSMeHb05s1UnxpO90yfvsk7/5Zgd0lXPnrKNzcNVUBBT5w==", "00905425612223", true, "default.jpg", "b779571b-6bc5-476f-bdd3-ac87aee0f2bd", false, "Editor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "FoodCategories",
                columns: new[] { "Id", "CategoryDescription", "CategoryName", "CategoryPicture", "CreateDate", "IsConfirmed", "IsDeleted", "UserId", "WhyNotConfirmed" },
                values: new object[,]
                {
                    { 1, "Tatlı Kategorisi", "Tatlılar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(2926), true, false, 1, null },
                    { 2, "Börek Kategorisi", "Börekler", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5891), true, false, 1, null },
                    { 3, "Çorba Kategorisi", "Çorbalar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5944), true, false, 1, null },
                    { 4, "Kek Kategorisi", "Kekler", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5950), true, false, 1, null },
                    { 5, "Makarna Kategorisi", "Makarnalar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5955), true, false, 1, null },
                    { 6, "Pasta Kategorisi", "Pastalar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5959), true, false, 2, null },
                    { 7, "Kahvaltı Kategorisi", "Kahvaltılıklar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5965), true, false, 2, null },
                    { 8, "Kurabiye Kategorisi", "Kurabiyeler", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5971), true, false, 2, null },
                    { 9, "Salata Kategorisi", "Salatalar", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5975), true, false, 2, null },
                    { 10, "Sulu Yemek Kategorisi", "Sulu Yemekler", "default.jpg", new DateTime(2021, 2, 17, 22, 59, 6, 248, DateTimeKind.Local).AddTicks(5980), true, false, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CommentCount", "CookingTime", "CreateTime", "FoodCategoryId", "FoodName", "FoodPicture", "FoodTitle", "IsConfirmed", "IsDeleted", "Material", "PersonSize", "PreparationTime", "RecipeDetail", "SeoAuthor", "SeoDescription", "SeoTags", "UserId", "ViewCount", "WhyNotConfirmed" },
                values: new object[,]
                {
                    { 1, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 260, DateTimeKind.Local).AddTicks(4530), 1, "Kadayif Tatlısı", "default.jpg", "guzel tatli", true, false, "kadayıf tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Kadayif tatlisi tarifi.", "birinci, yemek, tarifi", 1, 400, null },
                    { 2, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1536), 2, "Avcı Böreği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 1, 299, null },
                    { 3, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1626), 3, "Ispanak Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 2, 299, null },
                    { 4, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1633), 4, "Domates Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 2, 299, null },
                    { 5, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1640), 5, "Patates Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 1, 299, null },
                    { 6, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1647), 6, "Soğan Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 1, 299, null },
                    { 7, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1654), 7, "Böğürtlen Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 2, 299, null },
                    { 8, 0, new TimeSpan(0, 0, 15, 0, 0), new DateTime(2021, 2, 17, 22, 59, 6, 261, DateTimeKind.Local).AddTicks(1661), 8, "Peynir Yemeği", "default.jpg", "Tatlı tarifinin kısa bir başlığı.", true, false, "avcı böreği tarifi malzemeler listesi, html text editor ile yazılacak.", 4, new TimeSpan(0, 0, 25, 0, 0), "15 dk pişir, soğut, servis et vs.", "Caner Sahin", "Avcı böreği tarifi.", "börek, avcı, tarif", 1, 299, null }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentText", "CreateDate", "EmailAddress", "Fullname", "IsConfirmed", "IsDeleted", "RecipeId", "UserId", "WhyNotConfirmed" },
                values: new object[,]
                {
                    { 1, "Tatlı çok güzel olmuş.", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(3142), "cnrshn@gmail.com", "Caner Sahin", true, false, 1, 1, null },
                    { 2, "Güzel bir börek olmuş", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6137), "cnrshn@gmail.com", "Caner Sahin", true, false, 2, 2, null },
                    { 3, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6179), "cnrshn@gmail.com", "Caner Sahin", true, false, 3, 1, null },
                    { 4, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6183), "cnrshn@gmail.com", "Caner Sahin", true, false, 4, 2, null },
                    { 5, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6187), "cnrshn@gmail.com", "Caner Sahin", true, false, 5, 2, null },
                    { 6, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6191), "cnrshn@gmail.com", "Caner Sahin", true, false, 6, 1, null },
                    { 7, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6194), "cnrshn@gmail.com", "Caner Sahin", true, false, 7, 2, null },
                    { 8, "Tatlıya bayıldım...", new DateTime(2021, 2, 17, 22, 59, 6, 266, DateTimeKind.Local).AddTicks(6198), "cnrshn@gmail.com", "Caner Sahin", true, false, 8, 2, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodCategories_UserId",
                table: "FoodCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FoodCategoryId",
                table: "Recipes",
                column: "FoodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId",
                table: "Recipes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "FoodCategories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
