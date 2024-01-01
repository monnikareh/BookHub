using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Postgres.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
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
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PublisherId = table.Column<int>(type: "integer", nullable: false),
                    PrimaryGenreId = table.Column<int>(type: "integer", nullable: false),
                    StockInStorage = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    OverallRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Genres_PrimaryGenreId",
                        column: x => x.PrimaryGenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    BooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "integer", nullable: false),
                    GenresId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.BooksId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrders", x => new { x.BookId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_BookOrders_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.BooksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BookUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "ac19d3ce-c42b-4958-b8c8-7b50c34f544f", "roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEPaXndLcJ9O+Jv8LdOin8tGvgEd1rnFU8iyhIyhvd9DEkl5rGJ6qoASWKJSmbxDlyQ==", null, false, "3be31ba8-fcea-448a-a499-a57a616cc2b0", false, "roman" },
                    { 2, 0, "f0598dba-9b33-4744-9ab7-f0fdb71e99ed", "beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEG9NBEq84Y3UKsI/h74xfZqHD+5GgA1tbl+ULoqx15RO5SKUWwLDHzNHWvNZT85t0A==", null, false, "23338dd3-db73-4887-bfed-cf72dc5b995f", false, "beth" },
                    { 3, 0, "43a76491-ef35-44d1-88d9-582dc0576a86", "monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAEOTBwRaK461W4XlmPjZMmX8FDyPeb/WsEe+Tgyhy9/ikvEpjG0Bt5lipOKmu4oclwg==", null, false, "f01a08e1-bb38-4264-9f65-34f618a60922", false, "monika" },
                    { 4, 0, "49226f9f-a410-4d6e-b94e-a792e2e66ef9", "john@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEF+eWNlsToqnCLZxzOuwvQPP2CqjwZMHBeyUGts/Q+WnB13SXkEzjDrPMLxBmEI2Zw==", null, false, "6a7ef666-bf0f-45f8-bd9d-e882cd243f00", false, "john" },
                    { 5, 0, "d86afc99-391a-4ff2-a991-7e02785ab2cf", "james@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEJbC2y9RUZ/uY0XnAOaLfYQmDDeGBZdRUPtklewz7FJKDs+MU1tHkUZ3p9b/RONA7Q==", null, false, "2b2310d0-3ab9-4024-b307-6b5ced9cbfcb", false, "james" },
                    { 6, 0, "bb65f674-d7de-4597-9e76-16d4e0769e0f", "filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAEJMM/QXGX0jWHYQ+kxH7A6GbwHtiNbQzNUVsF1oMI9qIVDyG2U7abuxCI+PYAPYryQ==", null, false, "399dda64-109b-414e-ba38-6dc0d62543d0", false, "filip" },
                    { 7, 0, "e8fdd61a-9959-41b3-b27b-be318bc74f3c", "random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEC+3GxGouB+ffoy0UHhbjkq+AJngskZ7F7YxDYHE1pSNniilwnIdOfE7HYepLWH5Kw==", null, false, "fa29767e-9bdf-4f4b-ac6f-49ed76543524", false, "random" },
                    { 8, 0, "ee36f10e-f71c-4c6d-9c19-d085064e2ee6", "jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEB/ASmEorw6T3UOnTdzbKmVgYsRVHw1JL2VMN1hWpTw/oh/iwSzTDHjRRWygIA1W4w==", null, false, "cc76a897-409f-47e6-ae17-8040c91fd3f7", false, "jack" },
                    { 9, 0, "9deda5d1-4927-4aaf-bb7b-cfe3eae2ca1a", "tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEBJpM6KJEGR11d7BXLZO8+3WYQ7vT+FTR5nzz0LCI1/8yCQI7tR7M5V13XwOapjG7g==", null, false, "09e532f0-f67c-42f4-9482-ee932b0ca510", false, "tom" },
                    { 10, 0, "65172120-6d7f-4386-9908-d70926b18093", "ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEIRmhlEHHs7ylh30qTCJ+3idhSFX7dCJADNa1zADHCoWU6oYYRQXFrdUmf/FgoiQug==", null, false, "1476a619-a956-4ca4-8dd9-cbd8d3a0f049", false, "ali" },
                    { 11, 0, "2feea523-abef-4d96-83e8-3eb8b1da23b2", "rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEFU7FEtVEp9QG2+fdIZvckfn1LT31sREzWMTfGFoSBBvW4oWsnwmG7TlIs8rNycYZg==", null, false, "b9eb83c7-dac8-4c7f-9118-07b4477d24ff", false, "rubber" },
                    { 12, 0, "87ea2c57-7521-4bcd-bcfb-c3bf0abf38b7", "olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEEOK3Cxd0d6+8lAgh8KS1Ft8P+84ACk9f1HxxHr4ONyd1AN+s4ga144AEAIkXQNExw==", null, false, "2699ffc1-c712-4614-946e-d86cd7eea452", false, "olaf" },
                    { 13, 0, "4fd6ae0e-e848-4643-9715-d8160e8d5967", "good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEGOCr+QUJUj7c+skw38qJElDmOrGpFLW4eLv719FoDXTexDDk7jgeyEaOi8um8d9XA==", null, false, "922f8473-3e65-4922-97d3-e920bc8c8e83", false, "good" },
                    { 14, 0, "3a09fbbf-b563-411e-960a-abb571928fa1", "tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAENRZJy2wi+yY3XRVm1c/qGJITEPP+YEokSl5zvkGO4YQiBYLt6tVBN5x7M6WJ6qi9Q==", null, false, "b56eb4fd-be40-4234-a361-81cb8669c181", false, "tim" },
                    { 15, 0, "e72ebf18-e496-4488-89df-ae2cb26b24d1", "adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEDOyDhVMCbKOpAPUXUSHVv5MWI6tZXvGQ/GSzhO2v7wgpUYgwXlldYGurEO8wBW4ug==", null, false, "e1f09825-895b-44b2-aadc-8633c78d8063", false, "adam" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "J. K. Rowling" },
                    { 2, "George R. R. Martin" },
                    { 3, "Stephen King" },
                    { 4, "Agatha Christie" },
                    { 5, "J.R.R. Tolkien" },
                    { 6, "Jane Austen" },
                    { 7, "Mark Twain" },
                    { 8, "Charles Dickens" },
                    { 9, "Harper Lee" },
                    { 10, "Leo Tolstoy" },
                    { 11, "Agnes Christie" },
                    { 12, "Ernest Hemingway" },
                    { 13, "Virginia Woolf" },
                    { 14, "Arthur Conan Doyle" },
                    { 15, "Antoine de Saint-Exupéry" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dystopian" },
                    { 2, "Mystery" },
                    { 3, "Science Fiction" },
                    { 4, "Romance" },
                    { 5, "Fantasy" },
                    { 6, "Thriller" },
                    { 7, "Historical Fiction" },
                    { 8, "Horror" },
                    { 9, "Adventure" },
                    { 10, "Biography" },
                    { 11, "Non-fiction" },
                    { 12, "Children's" },
                    { 13, "Young Adult" },
                    { 14, "Humor" },
                    { 15, "Crime" },
                    { 16, "Historical Romance" },
                    { 17, "Western" },
                    { 18, "Science Fantasy" },
                    { 19, "Self-help" },
                    { 20, "Cookbook" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bloomsbury" },
                    { 2, "Secker & Warburg" },
                    { 3, "Reynal & Hitchcock" },
                    { 4, "Penguin Books" },
                    { 5, "Random House" },
                    { 6, "HarperCollins" },
                    { 7, "Simon & Schuster" },
                    { 8, "Macmillan Publishers" },
                    { 9, "Hachette Book Group" },
                    { 10, "Scholastic Corporation" },
                    { 11, "Oxford University Press" },
                    { 12, "Cambridge University Press" },
                    { 13, "Wiley" },
                    { 14, "Elsevier" },
                    { 15, "Springer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 9 },
                    { 2, 10 },
                    { 2, 11 },
                    { 2, 12 },
                    { 2, 13 },
                    { 2, 14 },
                    { 2, 15 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name", "OverallRating", "Price", "PrimaryGenreId", "PublisherId", "StockInStorage" },
                values: new object[,]
                {
                    { 1, "To Kill a Mockingbird", 77, 21.28m, 16, 1, 21 },
                    { 2, "1984", 58, 19.18m, 8, 3, 31 },
                    { 3, "James Bond", 51, 14.15m, 3, 1, 47 },
                    { 4, "The Great Gatsby", 74, 17.22m, 12, 13, 39 },
                    { 5, "One Hundred Years of Solitude", 35, 16.4m, 16, 12, 37 },
                    { 6, "The Catcher in the Rye", 55, 18.63m, 2, 14, 2 },
                    { 7, "Brave New World", 69, 24.85m, 7, 5, 47 },
                    { 8, "The Hobbit", 40, 14.15m, 10, 7, 33 },
                    { 9, "Love and Basketball", 58, 17.21m, 10, 2, 18 },
                    { 10, "Pride and Prejudice", 96, 13.08m, 9, 1, 39 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 94, 6.78m, 10, 9, 14 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 44, 11.88m, 13, 8, 4 },
                    { 13, "Harry Potter and the Philosopher's Stone", 86, 13.23m, 3, 2, 29 },
                    { 14, "The Hunger Games", 55, 9.28m, 10, 4, 48 },
                    { 15, "The Da Vinci Code", 87, 12.12m, 18, 8, 27 },
                    { 16, "A Game of Thrones", 34, 11.42m, 16, 14, 41 },
                    { 17, "The Shining", 71, 8.03m, 18, 11, 40 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 69, 15.34m, 14, 8, 47 },
                    { 19, "The Alchemist", 52, 21.22m, 8, 10, 47 },
                    { 20, "War and Peace", 71, 11.26m, 6, 2, 44 },
                    { 21, "Crime and Punishment", 83, 13.74m, 11, 5, 49 },
                    { 22, "The Catch-22", 37, 14.96m, 8, 14, 20 },
                    { 23, "The Grapes of Wrath", 83, 14.86m, 19, 1, 3 },
                    { 24, "Fahrenheit 451", 39, 14m, 9, 4, 22 },
                    { 25, "Lord of the Flies", 40, 6.27m, 19, 8, 46 },
                    { 26, "Moby-Dick", 36, 10.21m, 12, 6, 32 },
                    { 27, "Frankenstein", 96, 10.27m, 3, 3, 10 },
                    { 28, "Alice's Adventures in Wonderland", 86, 22.74m, 8, 13, 36 },
                    { 29, "Dracula", 52, 20.26m, 6, 7, 7 },
                    { 30, "The Odyssey", 48, 19.38m, 4, 13, 32 },
                    { 31, "Romeo and Juliet", 75, 20.5m, 10, 6, 7 },
                    { 32, "Hamlet", 34, 14.01m, 12, 10, 1 },
                    { 33, "Macbeth", 67, 22.58m, 14, 6, 2 },
                    { 34, "Othello", 45, 18m, 5, 2, 2 },
                    { 35, "The Divine Comedy", 83, 23.71m, 19, 7, 45 },
                    { 36, "Don Quixote", 74, 9.91m, 5, 14, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "OrderStatus", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8826), 3, 35.48m, 1 },
                    { 2, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8957), 1, 14.82m, 1 },
                    { 3, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8961), 1, 54.61m, 1 },
                    { 4, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8964), 3, 8.97m, 1 },
                    { 5, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8975), 2, 22.85m, 1 },
                    { 6, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8979), 1, 34.8m, 1 },
                    { 7, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8980), 4, 21.73m, 1 },
                    { 8, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8983), 1, 7.93m, 1 },
                    { 9, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(8985), 3, 39.9m, 1 },
                    { 10, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9023), 2, 8.51m, 2 },
                    { 11, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9025), 2, 36.52m, 2 },
                    { 12, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9027), 2, 26.7m, 2 },
                    { 13, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9029), 1, 45.08m, 2 },
                    { 14, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9031), 4, 14m, 2 },
                    { 15, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9033), 1, 19.67m, 2 },
                    { 16, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9054), 4, 45.84m, 2 },
                    { 17, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9056), 2, 23.65m, 2 },
                    { 18, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9059), 3, 19.8m, 2 },
                    { 19, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9061), 4, 40.12m, 2 },
                    { 20, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9063), 3, 5.32m, 3 },
                    { 21, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9065), 4, 17.59m, 3 },
                    { 22, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9067), 2, 23.41m, 3 },
                    { 23, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9068), 3, 14.45m, 3 },
                    { 24, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9070), 3, 37.86m, 3 },
                    { 25, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9072), 3, 28.8m, 3 },
                    { 26, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9074), 3, 40.79m, 3 },
                    { 27, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9076), 1, 45.32m, 3 },
                    { 28, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9078), 1, 17.94m, 3 },
                    { 29, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9080), 1, 15.97m, 3 },
                    { 30, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9093), 1, 54.59m, 4 },
                    { 31, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9154), 3, 12.64m, 4 },
                    { 32, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9156), 4, 40.91m, 4 },
                    { 33, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9158), 3, 15.09m, 4 },
                    { 34, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9161), 2, 32.23m, 4 },
                    { 35, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9163), 2, 9.76m, 4 },
                    { 36, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9165), 2, 21.39m, 4 },
                    { 37, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9167), 4, 21.53m, 4 },
                    { 38, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9169), 2, 42.64m, 4 },
                    { 39, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9170), 3, 38.95m, 4 },
                    { 40, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9172), 4, 21.33m, 5 },
                    { 41, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9174), 2, 36.96m, 5 },
                    { 42, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9176), 2, 9.4m, 5 },
                    { 43, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9178), 2, 6.26m, 5 },
                    { 44, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9179), 2, 50.65m, 5 },
                    { 45, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9182), 3, 34.61m, 5 },
                    { 46, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9183), 4, 53.25m, 5 },
                    { 47, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9185), 3, 49.62m, 5 },
                    { 48, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9187), 3, 17.46m, 5 },
                    { 49, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9189), 1, 35.51m, 5 },
                    { 50, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9191), 3, 49.13m, 6 },
                    { 51, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9192), 4, 45.44m, 6 },
                    { 52, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9195), 4, 38.17m, 6 },
                    { 53, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9197), 1, 50.12m, 6 },
                    { 54, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9198), 1, 18.21m, 6 },
                    { 55, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9200), 1, 31.14m, 6 },
                    { 56, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9202), 4, 24.99m, 6 },
                    { 57, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9204), 3, 33.65m, 6 },
                    { 58, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9206), 4, 25.9m, 6 },
                    { 59, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9207), 4, 13.4m, 6 },
                    { 60, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9299), 4, 33.9m, 7 },
                    { 61, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9302), 4, 32.03m, 7 },
                    { 62, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9303), 1, 24.07m, 7 },
                    { 63, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9305), 3, 31.35m, 7 },
                    { 64, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9307), 3, 38.34m, 7 },
                    { 65, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9309), 2, 10.71m, 7 },
                    { 66, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9311), 2, 11.93m, 7 },
                    { 67, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9313), 2, 26.62m, 7 },
                    { 68, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9315), 1, 52.29m, 7 },
                    { 69, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9317), 3, 10.54m, 7 },
                    { 70, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9319), 1, 25.57m, 8 },
                    { 71, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9320), 4, 25.69m, 8 },
                    { 72, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9322), 2, 8.71m, 8 },
                    { 73, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9324), 1, 30m, 8 },
                    { 74, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9326), 4, 37.02m, 8 },
                    { 75, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9328), 1, 44.88m, 8 },
                    { 76, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9330), 2, 43.21m, 8 },
                    { 77, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9331), 2, 50.51m, 8 },
                    { 78, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9333), 4, 19.18m, 8 },
                    { 79, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9335), 3, 25.45m, 8 },
                    { 80, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9337), 1, 47.32m, 9 },
                    { 81, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9339), 4, 52.17m, 9 },
                    { 82, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9341), 4, 7.31m, 9 },
                    { 83, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9343), 4, 32.33m, 9 },
                    { 84, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9344), 4, 23.37m, 9 },
                    { 85, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9346), 1, 6.98m, 9 },
                    { 86, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9348), 3, 20.37m, 9 },
                    { 87, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9350), 1, 15.19m, 9 },
                    { 88, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9352), 1, 16.08m, 9 },
                    { 89, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9354), 2, 33.1m, 9 },
                    { 90, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9356), 4, 36.97m, 10 },
                    { 91, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9357), 4, 23.24m, 10 },
                    { 92, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9359), 4, 32.78m, 10 },
                    { 93, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9361), 2, 28.18m, 10 },
                    { 94, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9363), 4, 9.61m, 10 },
                    { 95, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9365), 2, 50.78m, 10 },
                    { 96, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9367), 1, 17.04m, 10 },
                    { 97, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9369), 4, 7.53m, 10 },
                    { 98, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9370), 2, 50.85m, 10 },
                    { 99, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9372), 4, 7.46m, 10 },
                    { 100, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9374), 1, 49.77m, 11 },
                    { 101, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9376), 2, 9.42m, 11 },
                    { 102, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9378), 1, 30.63m, 11 },
                    { 103, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9380), 4, 23.51m, 11 },
                    { 104, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9381), 1, 24.89m, 11 },
                    { 105, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9383), 4, 29.74m, 11 },
                    { 106, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9385), 2, 44.66m, 11 },
                    { 107, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9387), 3, 14.69m, 11 },
                    { 108, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9388), 2, 39.21m, 11 },
                    { 109, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9440), 1, 12.24m, 11 },
                    { 110, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9442), 3, 10.93m, 12 },
                    { 111, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9444), 3, 13.49m, 12 },
                    { 112, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9446), 1, 15.06m, 12 },
                    { 113, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9448), 4, 39.35m, 12 },
                    { 114, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9450), 2, 53.43m, 12 },
                    { 115, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9451), 4, 21.27m, 12 },
                    { 116, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9453), 2, 10.84m, 12 },
                    { 117, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9455), 1, 46.88m, 12 },
                    { 118, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9457), 3, 47.35m, 12 },
                    { 119, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9459), 4, 6.19m, 12 },
                    { 120, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9461), 4, 14.1m, 13 },
                    { 121, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9462), 1, 16.49m, 13 },
                    { 122, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9464), 2, 32.38m, 13 },
                    { 123, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9466), 1, 35.64m, 13 },
                    { 124, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9468), 4, 25.24m, 13 },
                    { 125, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9470), 3, 16.01m, 13 },
                    { 126, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9471), 2, 22.71m, 13 },
                    { 127, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9473), 3, 27.51m, 13 },
                    { 128, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9475), 1, 37.59m, 13 },
                    { 129, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9477), 3, 17.27m, 13 },
                    { 130, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9480), 2, 26.74m, 14 },
                    { 131, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9481), 4, 46.59m, 14 },
                    { 132, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9483), 2, 51.21m, 14 },
                    { 133, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9485), 2, 33.73m, 14 },
                    { 134, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9487), 2, 6.96m, 14 },
                    { 135, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9488), 2, 38.47m, 14 },
                    { 136, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9490), 1, 38.89m, 14 },
                    { 137, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9492), 3, 34.3m, 14 },
                    { 138, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9494), 3, 14.38m, 14 },
                    { 139, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9496), 4, 16.22m, 14 },
                    { 140, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9498), 2, 53.61m, 15 },
                    { 141, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9500), 3, 11.23m, 15 },
                    { 142, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9502), 4, 6.38m, 15 },
                    { 143, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9504), 3, 37.82m, 15 },
                    { 144, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9506), 1, 10.59m, 15 },
                    { 145, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9508), 4, 7.99m, 15 },
                    { 146, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9509), 4, 5.15m, 15 },
                    { 147, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9511), 1, 15.19m, 15 },
                    { 148, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9513), 3, 5.73m, 15 },
                    { 149, new DateTime(2024, 1, 7, 18, 50, 56, 49, DateTimeKind.Local).AddTicks(9515), 4, 25.52m, 15 }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 5, 10 },
                    { 5, 11 },
                    { 5, 12 },
                    { 5, 13 },
                    { 6, 14 },
                    { 6, 15 },
                    { 6, 16 },
                    { 7, 17 },
                    { 7, 18 },
                    { 7, 19 },
                    { 7, 20 },
                    { 7, 21 },
                    { 8, 22 },
                    { 8, 23 },
                    { 8, 24 },
                    { 9, 25 },
                    { 9, 26 },
                    { 9, 27 },
                    { 10, 28 },
                    { 11, 29 },
                    { 12, 30 },
                    { 13, 11 },
                    { 13, 12 },
                    { 13, 31 },
                    { 14, 10 },
                    { 14, 32 },
                    { 14, 33 },
                    { 15, 1 },
                    { 15, 34 },
                    { 15, 35 }
                });

            migrationBuilder.InsertData(
                table: "BookGenre",
                columns: new[] { "BooksId", "GenresId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 13 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 13 },
                    { 6, 5 },
                    { 7, 6 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 8 },
                    { 10, 9 },
                    { 11, 10 },
                    { 12, 11 },
                    { 13, 12 },
                    { 14, 12 },
                    { 15, 12 },
                    { 16, 13 },
                    { 17, 13 },
                    { 18, 14 },
                    { 19, 1 },
                    { 19, 15 },
                    { 19, 17 },
                    { 20, 2 },
                    { 20, 16 },
                    { 21, 17 },
                    { 22, 17 },
                    { 23, 3 },
                    { 23, 18 },
                    { 24, 19 },
                    { 25, 20 },
                    { 26, 5 },
                    { 27, 6 },
                    { 28, 7 },
                    { 29, 13 },
                    { 30, 17 },
                    { 31, 16 },
                    { 32, 12 },
                    { 33, 9 },
                    { 34, 20 },
                    { 35, 5 },
                    { 35, 8 },
                    { 35, 10 }
                });

            migrationBuilder.InsertData(
                table: "BookOrders",
                columns: new[] { "BookId", "OrderId", "Count" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 2, 2 },
                    { 1, 3, 1 },
                    { 1, 4, 2 },
                    { 1, 5, 2 },
                    { 1, 6, 1 },
                    { 1, 7, 2 },
                    { 1, 8, 2 },
                    { 1, 9, 1 },
                    { 1, 10, 1 },
                    { 1, 11, 1 },
                    { 1, 12, 2 },
                    { 1, 13, 2 },
                    { 1, 14, 2 },
                    { 1, 15, 2 },
                    { 1, 16, 2 },
                    { 1, 17, 2 },
                    { 1, 18, 1 },
                    { 1, 19, 2 },
                    { 1, 20, 1 },
                    { 1, 21, 1 },
                    { 1, 22, 1 },
                    { 1, 23, 2 },
                    { 1, 24, 1 },
                    { 1, 25, 2 },
                    { 1, 26, 2 },
                    { 1, 27, 2 },
                    { 1, 28, 2 },
                    { 1, 29, 2 },
                    { 1, 30, 2 },
                    { 1, 31, 1 },
                    { 1, 32, 2 },
                    { 1, 33, 1 },
                    { 1, 34, 1 },
                    { 1, 35, 1 },
                    { 1, 36, 2 },
                    { 1, 37, 2 },
                    { 1, 38, 2 },
                    { 1, 39, 2 },
                    { 1, 40, 2 },
                    { 1, 41, 1 },
                    { 1, 42, 2 },
                    { 1, 43, 2 },
                    { 1, 44, 2 },
                    { 1, 45, 2 },
                    { 1, 46, 1 },
                    { 1, 47, 2 },
                    { 1, 48, 1 },
                    { 1, 49, 2 },
                    { 1, 50, 1 },
                    { 1, 51, 2 },
                    { 1, 52, 2 },
                    { 1, 53, 2 },
                    { 1, 54, 1 },
                    { 1, 55, 2 },
                    { 1, 56, 1 },
                    { 1, 57, 1 },
                    { 1, 58, 1 },
                    { 1, 59, 2 },
                    { 1, 60, 1 },
                    { 1, 61, 1 },
                    { 1, 62, 2 },
                    { 1, 63, 2 },
                    { 1, 64, 2 },
                    { 1, 65, 1 },
                    { 1, 66, 2 },
                    { 1, 67, 2 },
                    { 1, 68, 1 },
                    { 1, 69, 2 },
                    { 1, 70, 1 },
                    { 1, 71, 2 },
                    { 1, 72, 1 },
                    { 1, 73, 2 },
                    { 1, 74, 2 },
                    { 1, 75, 1 },
                    { 1, 76, 2 },
                    { 1, 77, 2 },
                    { 1, 78, 2 },
                    { 1, 79, 2 },
                    { 1, 80, 2 },
                    { 1, 81, 1 },
                    { 1, 82, 1 },
                    { 1, 83, 2 },
                    { 1, 84, 2 },
                    { 1, 85, 1 },
                    { 1, 86, 1 },
                    { 1, 87, 2 },
                    { 1, 88, 1 },
                    { 1, 89, 1 },
                    { 1, 90, 1 },
                    { 1, 91, 1 },
                    { 1, 92, 2 },
                    { 1, 93, 1 },
                    { 1, 94, 2 },
                    { 1, 95, 1 },
                    { 1, 96, 2 },
                    { 1, 97, 2 },
                    { 1, 98, 2 },
                    { 1, 99, 2 },
                    { 1, 100, 1 },
                    { 1, 101, 1 },
                    { 1, 102, 1 },
                    { 1, 103, 1 },
                    { 1, 104, 1 },
                    { 1, 105, 1 },
                    { 1, 106, 2 },
                    { 1, 107, 1 },
                    { 1, 108, 1 },
                    { 1, 109, 1 },
                    { 1, 110, 2 },
                    { 1, 111, 2 },
                    { 1, 112, 1 },
                    { 1, 113, 2 },
                    { 1, 114, 1 },
                    { 1, 115, 2 },
                    { 1, 116, 1 },
                    { 1, 117, 2 },
                    { 1, 118, 2 },
                    { 1, 119, 2 },
                    { 1, 120, 2 },
                    { 1, 121, 2 },
                    { 1, 122, 2 },
                    { 1, 123, 2 },
                    { 1, 124, 2 },
                    { 1, 125, 2 },
                    { 1, 126, 1 },
                    { 1, 127, 2 },
                    { 1, 128, 2 },
                    { 1, 129, 1 },
                    { 1, 130, 2 },
                    { 1, 131, 1 },
                    { 1, 132, 1 },
                    { 1, 133, 1 },
                    { 1, 134, 1 },
                    { 1, 135, 2 },
                    { 1, 136, 2 },
                    { 1, 137, 2 },
                    { 1, 138, 1 },
                    { 1, 139, 1 },
                    { 1, 140, 1 },
                    { 1, 141, 1 },
                    { 1, 142, 1 },
                    { 1, 143, 2 },
                    { 1, 144, 2 },
                    { 1, 145, 2 },
                    { 1, 146, 2 },
                    { 1, 147, 2 },
                    { 1, 148, 2 },
                    { 1, 149, 1 },
                    { 5, 1, 1 },
                    { 5, 2, 2 },
                    { 5, 3, 1 },
                    { 5, 4, 2 },
                    { 5, 5, 2 },
                    { 5, 6, 2 },
                    { 5, 7, 2 },
                    { 5, 8, 1 },
                    { 5, 9, 2 },
                    { 5, 10, 2 },
                    { 5, 11, 2 },
                    { 5, 12, 1 },
                    { 5, 13, 2 },
                    { 5, 14, 2 },
                    { 5, 15, 1 },
                    { 5, 16, 2 },
                    { 5, 17, 1 },
                    { 5, 18, 1 },
                    { 5, 19, 1 },
                    { 5, 20, 2 },
                    { 5, 21, 2 },
                    { 5, 22, 2 },
                    { 5, 23, 1 },
                    { 5, 24, 2 },
                    { 5, 25, 1 },
                    { 5, 26, 1 },
                    { 5, 27, 1 },
                    { 5, 28, 2 },
                    { 5, 29, 1 },
                    { 5, 30, 1 },
                    { 5, 31, 2 },
                    { 5, 32, 1 },
                    { 5, 33, 2 },
                    { 5, 34, 2 },
                    { 5, 35, 1 },
                    { 5, 36, 1 },
                    { 5, 37, 2 },
                    { 5, 38, 2 },
                    { 5, 39, 1 },
                    { 5, 40, 1 },
                    { 5, 41, 2 },
                    { 5, 42, 1 },
                    { 5, 43, 1 },
                    { 5, 44, 2 },
                    { 5, 45, 2 },
                    { 5, 46, 2 },
                    { 5, 47, 2 },
                    { 5, 48, 1 },
                    { 5, 49, 2 },
                    { 5, 50, 2 },
                    { 5, 51, 1 },
                    { 5, 52, 2 },
                    { 5, 53, 2 },
                    { 5, 54, 1 },
                    { 5, 55, 2 },
                    { 5, 56, 2 },
                    { 5, 57, 2 },
                    { 5, 58, 2 },
                    { 5, 59, 1 },
                    { 5, 60, 2 },
                    { 5, 61, 1 },
                    { 5, 62, 2 },
                    { 5, 63, 1 },
                    { 5, 64, 1 },
                    { 5, 65, 1 },
                    { 5, 66, 1 },
                    { 5, 67, 2 },
                    { 5, 68, 1 },
                    { 5, 69, 2 },
                    { 5, 70, 1 },
                    { 5, 71, 2 },
                    { 5, 72, 1 },
                    { 5, 73, 1 },
                    { 5, 74, 2 },
                    { 5, 75, 2 },
                    { 5, 76, 1 },
                    { 5, 77, 1 },
                    { 5, 78, 1 },
                    { 5, 79, 2 },
                    { 5, 80, 2 },
                    { 5, 81, 2 },
                    { 5, 82, 1 },
                    { 5, 83, 2 },
                    { 5, 84, 2 },
                    { 5, 85, 2 },
                    { 5, 86, 1 },
                    { 5, 87, 2 },
                    { 5, 88, 1 },
                    { 5, 89, 1 },
                    { 5, 90, 2 },
                    { 5, 91, 1 },
                    { 5, 92, 2 },
                    { 5, 93, 1 },
                    { 5, 94, 1 },
                    { 5, 95, 1 },
                    { 5, 96, 1 },
                    { 5, 97, 1 },
                    { 5, 98, 2 },
                    { 5, 99, 2 },
                    { 5, 100, 1 },
                    { 5, 101, 2 },
                    { 5, 102, 2 },
                    { 5, 103, 2 },
                    { 5, 104, 2 },
                    { 5, 105, 1 },
                    { 5, 106, 1 },
                    { 5, 107, 2 },
                    { 5, 108, 1 },
                    { 5, 109, 1 },
                    { 5, 110, 1 },
                    { 5, 111, 1 },
                    { 5, 112, 1 },
                    { 5, 113, 2 },
                    { 5, 114, 2 },
                    { 5, 115, 1 },
                    { 5, 116, 2 },
                    { 5, 117, 2 },
                    { 5, 118, 2 },
                    { 5, 119, 1 },
                    { 5, 120, 2 },
                    { 5, 121, 1 },
                    { 5, 122, 2 },
                    { 5, 123, 1 },
                    { 5, 124, 2 },
                    { 5, 125, 1 },
                    { 5, 126, 2 },
                    { 5, 127, 2 },
                    { 5, 128, 1 },
                    { 5, 129, 1 },
                    { 5, 130, 2 },
                    { 5, 131, 1 },
                    { 5, 132, 1 },
                    { 5, 133, 1 },
                    { 5, 134, 1 },
                    { 5, 135, 2 },
                    { 5, 136, 1 },
                    { 5, 137, 1 },
                    { 5, 138, 2 },
                    { 5, 139, 2 },
                    { 5, 140, 1 },
                    { 5, 141, 2 },
                    { 5, 142, 1 },
                    { 5, 143, 2 },
                    { 5, 144, 1 },
                    { 5, 145, 2 },
                    { 5, 146, 1 },
                    { 5, 147, 1 },
                    { 5, 148, 1 },
                    { 5, 149, 2 },
                    { 10, 2, 1 },
                    { 10, 3, 2 },
                    { 10, 4, 1 },
                    { 10, 5, 2 },
                    { 10, 6, 1 },
                    { 10, 7, 1 },
                    { 10, 8, 2 },
                    { 10, 11, 2 },
                    { 10, 13, 2 },
                    { 10, 14, 1 },
                    { 10, 16, 1 },
                    { 10, 18, 2 },
                    { 10, 19, 2 },
                    { 10, 20, 2 },
                    { 10, 22, 2 },
                    { 10, 23, 2 },
                    { 10, 26, 2 },
                    { 10, 27, 1 },
                    { 10, 28, 2 },
                    { 10, 32, 1 },
                    { 10, 33, 2 },
                    { 10, 34, 2 },
                    { 10, 35, 1 },
                    { 10, 36, 1 },
                    { 10, 38, 2 },
                    { 10, 39, 1 },
                    { 10, 40, 1 },
                    { 10, 42, 2 },
                    { 10, 44, 2 },
                    { 10, 45, 1 },
                    { 10, 46, 2 },
                    { 10, 48, 2 },
                    { 10, 49, 1 },
                    { 10, 50, 2 },
                    { 10, 51, 2 },
                    { 10, 52, 1 },
                    { 10, 53, 1 },
                    { 10, 54, 1 },
                    { 10, 56, 1 },
                    { 10, 57, 2 },
                    { 10, 58, 2 },
                    { 10, 59, 1 },
                    { 10, 60, 2 },
                    { 10, 61, 2 },
                    { 10, 64, 2 },
                    { 10, 65, 1 },
                    { 10, 66, 2 },
                    { 10, 67, 1 },
                    { 10, 68, 2 },
                    { 10, 69, 1 },
                    { 10, 70, 2 },
                    { 10, 71, 1 },
                    { 10, 72, 1 },
                    { 10, 74, 1 },
                    { 10, 75, 1 },
                    { 10, 76, 1 },
                    { 10, 77, 2 },
                    { 10, 80, 2 },
                    { 10, 81, 2 },
                    { 10, 82, 1 },
                    { 10, 84, 2 },
                    { 10, 85, 2 },
                    { 10, 86, 2 },
                    { 10, 88, 1 },
                    { 10, 91, 2 },
                    { 10, 92, 1 },
                    { 10, 93, 1 },
                    { 10, 94, 1 },
                    { 10, 96, 2 },
                    { 10, 98, 1 },
                    { 10, 99, 2 },
                    { 10, 100, 1 },
                    { 10, 101, 2 },
                    { 10, 102, 2 },
                    { 10, 103, 2 },
                    { 10, 104, 1 },
                    { 10, 105, 1 },
                    { 10, 106, 1 },
                    { 10, 107, 2 },
                    { 10, 108, 1 },
                    { 10, 109, 2 },
                    { 10, 111, 1 },
                    { 10, 112, 1 },
                    { 10, 113, 1 },
                    { 10, 114, 2 },
                    { 10, 116, 1 },
                    { 10, 117, 2 },
                    { 10, 118, 1 },
                    { 10, 119, 2 },
                    { 10, 120, 2 },
                    { 10, 121, 2 },
                    { 10, 122, 2 },
                    { 10, 124, 1 },
                    { 10, 125, 1 },
                    { 10, 126, 1 },
                    { 10, 127, 1 },
                    { 10, 128, 2 },
                    { 10, 129, 1 },
                    { 10, 130, 1 },
                    { 10, 131, 2 },
                    { 10, 135, 1 },
                    { 10, 136, 1 },
                    { 10, 138, 1 },
                    { 10, 139, 2 },
                    { 10, 140, 2 },
                    { 10, 142, 2 },
                    { 10, 143, 1 },
                    { 10, 145, 1 },
                    { 10, 147, 2 },
                    { 10, 148, 2 },
                    { 10, 149, 2 },
                    { 15, 3, 1 },
                    { 15, 4, 2 },
                    { 15, 5, 2 },
                    { 15, 8, 2 },
                    { 15, 27, 2 },
                    { 15, 28, 2 },
                    { 15, 34, 1 },
                    { 15, 38, 1 },
                    { 15, 40, 1 },
                    { 15, 42, 1 },
                    { 15, 46, 1 },
                    { 15, 48, 2 },
                    { 15, 52, 2 },
                    { 15, 53, 1 },
                    { 15, 57, 1 },
                    { 15, 58, 1 },
                    { 15, 59, 1 },
                    { 15, 64, 1 },
                    { 15, 65, 1 },
                    { 15, 68, 1 },
                    { 15, 69, 2 },
                    { 15, 71, 1 },
                    { 15, 74, 2 },
                    { 15, 76, 1 },
                    { 15, 81, 1 },
                    { 15, 85, 1 },
                    { 15, 86, 1 },
                    { 15, 88, 2 },
                    { 15, 91, 1 },
                    { 15, 94, 2 },
                    { 15, 96, 1 },
                    { 15, 98, 2 },
                    { 15, 99, 1 },
                    { 15, 102, 1 },
                    { 15, 103, 2 },
                    { 15, 107, 2 },
                    { 15, 108, 1 },
                    { 15, 116, 1 },
                    { 15, 119, 1 },
                    { 15, 121, 2 },
                    { 15, 126, 2 },
                    { 15, 136, 1 },
                    { 15, 139, 2 },
                    { 15, 142, 1 },
                    { 15, 145, 2 },
                    { 15, 148, 1 },
                    { 20, 3, 2 },
                    { 20, 27, 2 },
                    { 20, 28, 1 },
                    { 20, 52, 2 },
                    { 20, 53, 1 },
                    { 20, 58, 2 },
                    { 20, 69, 1 },
                    { 20, 81, 1 },
                    { 20, 88, 1 },
                    { 20, 96, 1 },
                    { 20, 98, 1 },
                    { 20, 99, 1 },
                    { 20, 103, 1 },
                    { 20, 107, 1 },
                    { 20, 126, 2 },
                    { 20, 148, 2 }
                });

            migrationBuilder.InsertData(
                table: "BookUser",
                columns: new[] { "BooksId", "UsersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 13 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 13 },
                    { 6, 5 },
                    { 7, 6 },
                    { 7, 7 },
                    { 8, 7 },
                    { 9, 8 },
                    { 10, 9 },
                    { 11, 10 },
                    { 12, 9 },
                    { 12, 11 },
                    { 13, 12 },
                    { 14, 12 },
                    { 15, 12 },
                    { 16, 13 },
                    { 17, 13 },
                    { 18, 14 },
                    { 19, 1 },
                    { 19, 12 },
                    { 19, 15 },
                    { 20, 2 },
                    { 20, 8 },
                    { 21, 9 },
                    { 22, 10 },
                    { 23, 3 },
                    { 23, 11 },
                    { 24, 2 },
                    { 25, 1 },
                    { 26, 5 },
                    { 27, 6 },
                    { 28, 7 },
                    { 29, 13 },
                    { 30, 15 },
                    { 31, 7 },
                    { 32, 12 },
                    { 33, 9 },
                    { 34, 14 },
                    { 35, 3 },
                    { 35, 5 },
                    { 35, 8 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "Comment", "UserId", "Value" },
                values: new object[,]
                {
                    { 1, 5, "Great book but it gave me an existential crisis bigger than I had before", 9, 40 },
                    { 2, 15, "Couldn't put it down, finished it in one sitting!", 8, 21 },
                    { 3, 10, "The plot twists in this book are mind-blowing", 3, 48 },
                    { 4, 11, "A classic that everyone should read", 2, 92 },
                    { 5, 1, "The characters are so well-developed, felt like they were real people", 6, 19 },
                    { 6, 26, "This book made me laugh and cry, a roller coaster of emotions", 1, 69 },
                    { 7, 14, "The writing style is beautiful, every sentence is a work of art", 7, 24 },
                    { 8, 2, "I couldn't guess the ending, kept me guessing until the last page", 13, 55 },
                    { 9, 27, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 1, 44 },
                    { 10, 21, "The themes explored in this book are thought-provoking", 8, 38 },
                    { 11, 5, "The pacing is perfect, kept me engaged from start to finish", 12, 21 },
                    { 12, 7, "This book challenged my perspective on life", 4, 66 },
                    { 13, 5, "The world-building is exceptional, I felt like I was there", 5, 63 },
                    { 14, 11, "A must-read for book lovers", 3, 17 },
                    { 15, 17, "The author's storytelling is captivating", 12, 59 },
                    { 16, 30, "This book is a page-turner, couldn't stop reading", 3, 46 },
                    { 17, 34, "The dialogue between characters is witty and realistic", 2, 97 },
                    { 18, 6, "I've recommended this book to all my friends", 12, 57 },
                    { 19, 2, "It left me with a book hangover, couldn't stop thinking about it", 11, 42 },
                    { 20, 13, "", 6, 20 },
                    { 21, 19, "Couldn't get into the story, found it boring from the start", 5, 15 },
                    { 22, 10, "The characters felt one-dimensional and uninteresting", 5, 69 },
                    { 23, 32, "The plot was predictable, I expected more twists", 14, 78 },
                    { 24, 31, "I didn't connect with the protagonist, lacked depth", 6, 46 },
                    { 25, 28, "The writing style was confusing and hard to follow", 11, 41 },
                    { 26, 7, "This book didn't live up to the hype, very disappointing", 13, 55 },
                    { 27, 24, "The ending felt rushed and unresolved", 5, 62 },
                    { 28, 33, "Too much exposition, not enough action", 5, 90 },
                    { 29, 6, "I found the dialogue unrealistic and forced", 12, 21 },
                    { 30, 31, "The author tried too hard to be profound, came off as pretentious", 2, 70 },
                    { 31, 21, "The pacing was off, some parts dragged on while others felt rushed", 14, 79 },
                    { 32, 9, "The world-building was weak and inconsistent", 1, 50 },
                    { 33, 16, "I couldn't sympathize with any of the characters", 8, 54 },
                    { 34, 31, "The themes explored were cliché and overdone", 12, 19 },
                    { 35, 14, "The book didn't live up to the reviews, a letdown", 13, 54 },
                    { 36, 17, "The grammar and editing were poor, distracting from the story", 1, 86 },
                    { 37, 29, "The book felt like a rip-off of [another popular book]", 5, 53 },
                    { 38, 27, "The author relied too heavily on stereotypes", 4, 30 },
                    { 39, 3, "I regret spending time on this book, wish I chose something else", 6, 53 },
                    { 40, 5, "The climax was anticlimactic, left me unsatisfied", 14, 24 },
                    { 41, 32, "Great book but it gave me an existential crisis bigger than I had before", 12, 99 },
                    { 42, 8, "Couldn't put it down, finished it in one sitting!", 11, 30 },
                    { 43, 22, "The plot twists in this book are mind-blowing", 6, 87 },
                    { 44, 34, "A classic that everyone should read", 3, 42 },
                    { 45, 16, "The characters are so well-developed, felt like they were real people", 13, 52 },
                    { 46, 32, "This book made me laugh and cry, a roller coaster of emotions", 14, 46 },
                    { 47, 33, "The writing style is beautiful, every sentence is a work of art", 8, 51 },
                    { 48, 5, "I couldn't guess the ending, kept me guessing until the last page", 10, 25 },
                    { 49, 28, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 8, 38 },
                    { 50, 31, "The themes explored in this book are thought-provoking", 14, 46 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenresId",
                table: "BookGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrders_OrderId",
                table: "BookOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PrimaryGenreId",
                table: "Books",
                column: "PrimaryGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersId",
                table: "BookUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");
        }

        /// <inheritdoc />
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
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "BookOrders");

            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
