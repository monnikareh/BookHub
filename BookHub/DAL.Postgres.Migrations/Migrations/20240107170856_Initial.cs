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
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
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
                    { 1, 0, "106376e6-68f4-43cb-8909-c83c80ecf635", "roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEFK0VMckfQDvyVrR3JuVWIK83fJWjgvAJPtQ7hARz/Iuyt8//QlbeA3BlI1iXjK2vw==", null, false, "6fd1d5da-9479-4bf5-b562-fa4e35db744a", false, "roman" },
                    { 2, 0, "1fa36924-64e8-4917-b16f-01bcf643c8a3", "beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAENKrSi5e6FVExqsdBvnU+UjVjoHGU1NDerlmaDaLLuLTbZSz3aMQut0BMdr/dmp3qg==", null, false, "c9cca287-cb4b-4567-bd4a-60cf38f786b4", false, "beth" },
                    { 3, 0, "986c58e1-bb35-4efa-8b7e-f8467421475c", "monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAENXiUET0AbOq3YMhjn4wW+hYtoG1Gh2dQx93/8vmnMH9930QbIovsrUi7GfHD4A0FQ==", null, false, "3b6d3ee3-8734-4290-a844-eb2914cb4b94", false, "monika" },
                    { 4, 0, "44189074-4b21-4b15-a919-d85579e22cbc", "john@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEC90+hdZ/m/22JNvytBvpbvyhr4xVZuWHNzmhy7Iopca6oIV8aDWgT5S/wmWgzPbCg==", null, false, "eb840ece-277f-4c49-9519-c6c1b551da84", false, "john" },
                    { 5, 0, "ab9fe786-b3e1-4128-9765-ee18828b732d", "james@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEPjFnbB/9CzYS7ctht8XUwadyg12ibMBWHcYbkExmXUxMCWBxSLmERCq77bk7SMG/g==", null, false, "9fa5d32a-a334-44af-9712-e2c5957eb37c", false, "james" },
                    { 6, 0, "77193ae1-6559-41b3-8d3b-9d0856d6db8f", "filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAEDe1xHSR55JRDxwmyO773JbIRfpoXhQVqJMbn3B6LUw2jU9dDSzuKoEOQy1JF07U3Q==", null, false, "1e45f580-c40b-4ca1-8a5e-ad5a2feaf446", false, "filip" },
                    { 7, 0, "a0eebaa9-9413-45d5-92be-237a6ba44146", "random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEFbIZpy3KjXLX346cCuyj/C1k7bcRciGYNjl+6Ht64+XsgvDkCIKbAKgsPu6+YznyA==", null, false, "2fc11d7d-a56c-466c-bc83-50486764bec5", false, "random" },
                    { 8, 0, "c8a44730-96b1-43b8-be88-31591a4119d7", "jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEIwKINTWom7HasZWyqZDxdYM31RII3jmMgvIqmQsL2QgbHHjtj1DIZLMpmyrXgPGeA==", null, false, "ac4fa9cf-53dd-456b-8cfe-1ff23857b277", false, "jack" },
                    { 9, 0, "8b67566f-6a87-434f-9761-0c0aee356547", "tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEPYNno8965+fkiC2ZpwKpMZ/UyA0oM01k2NYw5TRdUzFRCHkkjhgEnZ6PTtkL0WtFA==", null, false, "4a17cf5c-da37-476c-8a3a-b2bbec593ca3", false, "tom" },
                    { 10, 0, "6de3ecd3-e789-43bd-a408-948ab5da29f2", "ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEIE6NjYSX+9wCP4FZ+ogfy1BhwjQzqSrmkENFFFKaWp4krahJYYhuZw6X4q7KnTnvA==", null, false, "9506e5f2-a084-48bc-ae92-85b2da694703", false, "ali" },
                    { 11, 0, "62b39023-be4d-4b29-b288-dfd7dbdcd6f0", "rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEOxGH8O90dP8K1jRASbtiVMrIwdD/MRxQ9QczcmxPJj0bc73+sJqYpUBhVLP+q7ZBw==", null, false, "d088d0fd-a8d1-4d97-91d2-97453a117706", false, "rubber" },
                    { 12, 0, "343e97d6-a0b3-4645-a416-3b8ef5b9a855", "olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEBczxExAsHlYjpMTgK8TnkegcCBGA8i+uHGlSdu+814z28ECP0Td32SnhV4gquwECA==", null, false, "b42ed8a7-6d82-476c-8d81-1053479e847f", false, "olaf" },
                    { 13, 0, "94f21aff-ab42-44f6-ab6d-460b5cf87a34", "good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAENJNwKOO5R5hFIFym4TaLNh6JXNhjY93qKthDH4TAA1pSs4QjXT1sf0+DC2iBB3gCA==", null, false, "bf39ce0f-e1bd-441c-b43b-1486963f4b86", false, "good" },
                    { 14, 0, "55ed1278-2930-41db-9451-32bb5ffb4bed", "tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAEBfjXt4uwh8m9PArVm4evCm3FV6Ct3YO76xTXWDGPAk3MvYKMAB3k9zhhm2/PJJrEA==", null, false, "97515e99-e175-4575-90ca-65ccbe76b86d", false, "tim" },
                    { 15, 0, "2fa7f809-1480-46c0-a95b-c60bc8aeb945", "adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEDQu7ATGgwuIKsVkRMxxTJFI8nW1qmA4xlN0VU7LsOgNNXGFpYOl4RhxaHTOuSsVUg==", null, false, "09211eeb-44bd-4a6c-a18d-b61191f68525", false, "adam" }
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
                    { 1, "To Kill a Mockingbird", 89, 23.74m, 13, 14, 6 },
                    { 2, "1984", 37, 11.32m, 6, 12, 4 },
                    { 3, "James Bond", 66, 21.06m, 4, 3, 15 },
                    { 4, "The Great Gatsby", 59, 14.15m, 5, 10, 26 },
                    { 5, "One Hundred Years of Solitude", 94, 15.38m, 8, 11, 31 },
                    { 6, "The Catcher in the Rye", 34, 21.3m, 5, 6, 46 },
                    { 7, "Brave New World", 76, 18.52m, 4, 6, 27 },
                    { 8, "The Hobbit", 80, 9.67m, 3, 4, 5 },
                    { 9, "Love and Basketball", 40, 7.31m, 11, 8, 26 },
                    { 10, "Pride and Prejudice", 55, 9.32m, 5, 1, 21 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 68, 6.37m, 17, 11, 41 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 83, 24.18m, 3, 9, 39 },
                    { 13, "Harry Potter and the Philosopher's Stone", 45, 14.76m, 4, 12, 25 },
                    { 14, "The Hunger Games", 69, 21.81m, 13, 12, 18 },
                    { 15, "The Da Vinci Code", 98, 10.14m, 7, 10, 3 },
                    { 16, "A Game of Thrones", 96, 7.24m, 3, 4, 6 },
                    { 17, "The Shining", 55, 15.09m, 16, 6, 8 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 86, 9.39m, 2, 12, 20 },
                    { 19, "The Alchemist", 61, 9.85m, 15, 11, 38 },
                    { 20, "War and Peace", 55, 13.68m, 16, 1, 9 },
                    { 21, "Crime and Punishment", 76, 22.56m, 2, 14, 39 },
                    { 22, "The Catch-22", 70, 22.76m, 12, 5, 38 },
                    { 23, "The Grapes of Wrath", 92, 21.94m, 7, 7, 22 },
                    { 24, "Fahrenheit 451", 84, 22.53m, 12, 4, 49 },
                    { 25, "Lord of the Flies", 59, 5.04m, 1, 5, 26 },
                    { 26, "Moby-Dick", 67, 9.1m, 9, 8, 13 },
                    { 27, "Frankenstein", 50, 13.18m, 1, 10, 16 },
                    { 28, "Alice's Adventures in Wonderland", 83, 8.06m, 4, 5, 20 },
                    { 29, "Dracula", 69, 22.59m, 18, 4, 41 },
                    { 30, "The Odyssey", 68, 9.44m, 18, 7, 25 },
                    { 31, "Romeo and Juliet", 80, 15.31m, 7, 5, 46 },
                    { 32, "Hamlet", 89, 14.94m, 10, 3, 36 },
                    { 33, "Macbeth", 34, 14.3m, 17, 14, 48 },
                    { 34, "Othello", 31, 15.46m, 18, 11, 28 },
                    { 35, "The Divine Comedy", 66, 10.61m, 3, 2, 1 },
                    { 36, "Don Quixote", 61, 17.7m, 4, 8, 38 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "PaymentStatus", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6388), 1, 37.46m, 2 },
                    { 2, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6511), 4, 41.88m, 1 },
                    { 3, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6515), 1, 37.41m, 1 },
                    { 4, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6518), 1, 7.18m, 3 },
                    { 5, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6529), 1, 6.49m, 1 },
                    { 6, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6532), 4, 35.4m, 2 },
                    { 7, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6534), 2, 5.81m, 3 },
                    { 8, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6536), 4, 15.8m, 3 },
                    { 9, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6538), 3, 46.08m, 3 },
                    { 10, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6540), 1, 12.51m, 1 },
                    { 11, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6542), 1, 46.49m, 3 },
                    { 12, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6544), 3, 18.98m, 3 },
                    { 13, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6546), 3, 38.04m, 1 },
                    { 14, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6548), 3, 44.56m, 1 },
                    { 15, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6549), 3, 54.16m, 3 },
                    { 16, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6582), 3, 27.89m, 1 },
                    { 17, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6584), 3, 31.78m, 1 },
                    { 18, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6586), 3, 6.02m, 1 },
                    { 19, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6628), 3, 22.16m, 1 },
                    { 20, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6630), 1, 17.38m, 2 },
                    { 21, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6632), 2, 7.86m, 3 },
                    { 22, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6634), 2, 17.32m, 2 },
                    { 23, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6636), 1, 8.51m, 1 },
                    { 24, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6638), 2, 30.1m, 2 },
                    { 25, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6640), 3, 24.46m, 3 },
                    { 26, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6642), 2, 48.67m, 2 },
                    { 27, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6644), 2, 8.5m, 1 },
                    { 28, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6646), 2, 27.88m, 1 },
                    { 29, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6648), 1, 44.45m, 3 },
                    { 30, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6654), 4, 34.09m, 3 },
                    { 31, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6697), 2, 8.94m, 3 },
                    { 32, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6700), 1, 43.23m, 2 },
                    { 33, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6702), 4, 25.16m, 1 },
                    { 34, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6705), 4, 17.53m, 2 },
                    { 35, new DateTime(2024, 1, 7, 18, 8, 56, 303, DateTimeKind.Local).AddTicks(6707), 3, 36.15m, 3 }
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
                    { 1, 8, 4 },
                    { 3, 28, 5 },
                    { 3, 35, 4 },
                    { 7, 4, 7 },
                    { 7, 12, 3 },
                    { 7, 26, 9 },
                    { 9, 19, 3 },
                    { 9, 30, 4 },
                    { 9, 34, 8 },
                    { 10, 7, 7 },
                    { 11, 18, 7 },
                    { 11, 24, 1 },
                    { 14, 6, 9 },
                    { 15, 1, 8 },
                    { 15, 32, 5 },
                    { 16, 2, 6 },
                    { 16, 20, 4 },
                    { 17, 11, 7 },
                    { 19, 27, 7 },
                    { 21, 17, 3 },
                    { 21, 22, 2 },
                    { 22, 10, 3 },
                    { 26, 3, 6 },
                    { 26, 9, 5 },
                    { 26, 23, 3 },
                    { 27, 16, 8 },
                    { 28, 29, 5 },
                    { 28, 33, 3 },
                    { 29, 21, 6 },
                    { 30, 5, 2 },
                    { 30, 15, 9 },
                    { 32, 14, 1 },
                    { 32, 31, 2 },
                    { 33, 25, 5 },
                    { 34, 13, 5 }
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
                    { 1, 21, "Great book but it gave me an existential crisis bigger than I had before", 7, 33 },
                    { 2, 22, "Couldn't put it down, finished it in one sitting!", 11, 80 },
                    { 3, 21, "The plot twists in this book are mind-blowing", 1, 46 },
                    { 4, 14, "A classic that everyone should read", 10, 78 },
                    { 5, 16, "The characters are so well-developed, felt like they were real people", 6, 50 },
                    { 6, 19, "This book made me laugh and cry, a roller coaster of emotions", 1, 42 },
                    { 7, 24, "The writing style is beautiful, every sentence is a work of art", 13, 50 },
                    { 8, 8, "I couldn't guess the ending, kept me guessing until the last page", 4, 82 },
                    { 9, 33, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 12, 15 },
                    { 10, 33, "The themes explored in this book are thought-provoking", 1, 68 },
                    { 11, 22, "The pacing is perfect, kept me engaged from start to finish", 11, 29 },
                    { 12, 31, "This book challenged my perspective on life", 8, 20 },
                    { 13, 14, "The world-building is exceptional, I felt like I was there", 3, 43 },
                    { 14, 17, "A must-read for book lovers", 2, 43 },
                    { 15, 9, "The author's storytelling is captivating", 12, 42 },
                    { 16, 30, "This book is a page-turner, couldn't stop reading", 9, 56 },
                    { 17, 7, "The dialogue between characters is witty and realistic", 10, 86 },
                    { 18, 21, "I've recommended this book to all my friends", 11, 86 },
                    { 19, 33, "It left me with a book hangover, couldn't stop thinking about it", 9, 29 },
                    { 20, 25, "", 7, 39 },
                    { 21, 12, "Couldn't get into the story, found it boring from the start", 13, 89 },
                    { 22, 11, "The characters felt one-dimensional and uninteresting", 10, 88 },
                    { 23, 9, "The plot was predictable, I expected more twists", 13, 57 },
                    { 24, 29, "I didn't connect with the protagonist, lacked depth", 13, 90 },
                    { 25, 18, "The writing style was confusing and hard to follow", 14, 56 },
                    { 26, 8, "This book didn't live up to the hype, very disappointing", 7, 87 },
                    { 27, 12, "The ending felt rushed and unresolved", 13, 88 },
                    { 28, 29, "Too much exposition, not enough action", 5, 62 },
                    { 29, 3, "I found the dialogue unrealistic and forced", 6, 31 },
                    { 30, 1, "The author tried too hard to be profound, came off as pretentious", 2, 62 },
                    { 31, 16, "The pacing was off, some parts dragged on while others felt rushed", 5, 44 },
                    { 32, 1, "The world-building was weak and inconsistent", 1, 29 },
                    { 33, 18, "I couldn't sympathize with any of the characters", 7, 83 },
                    { 34, 32, "The themes explored were cliché and overdone", 12, 24 },
                    { 35, 31, "The book didn't live up to the reviews, a letdown", 11, 37 },
                    { 36, 11, "The grammar and editing were poor, distracting from the story", 1, 28 },
                    { 37, 6, "The book felt like a rip-off of [another popular book]", 5, 48 },
                    { 38, 12, "The author relied too heavily on stereotypes", 7, 93 },
                    { 39, 10, "I regret spending time on this book, wish I chose something else", 4, 49 },
                    { 40, 31, "The climax was anticlimactic, left me unsatisfied", 8, 10 },
                    { 41, 6, "Great book but it gave me an existential crisis bigger than I had before", 10, 61 },
                    { 42, 33, "Couldn't put it down, finished it in one sitting!", 2, 62 },
                    { 43, 9, "The plot twists in this book are mind-blowing", 1, 23 },
                    { 44, 34, "A classic that everyone should read", 3, 12 },
                    { 45, 12, "The characters are so well-developed, felt like they were real people", 11, 83 },
                    { 46, 28, "This book made me laugh and cry, a roller coaster of emotions", 1, 89 },
                    { 47, 33, "The writing style is beautiful, every sentence is a work of art", 14, 33 },
                    { 48, 21, "I couldn't guess the ending, kept me guessing until the last page", 9, 25 },
                    { 49, 20, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 11, 20 },
                    { 50, 14, "The themes explored in this book are thought-provoking", 13, 58 }
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
