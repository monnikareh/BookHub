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
                    { 1, 0, "b3ab738b-1ba0-4e19-81c5-3a3f85dbc6ca", "Roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEN5iGBwDFL6sK6CmEWOCmUV8/Zv+O6BeJO90ml4sYfTHGWFFL3Eknon/6HvUSOjpSw==", null, false, "9029ef24-307c-4086-aa5c-f10eae2dad10", false, "roman" },
                    { 2, 0, "c1277305-03bf-4a05-9911-f70639337c1c", "Beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEGkDFlpY7Hku6W0mO3AYga3CZZYIkbEoEFAoqH8ZdxuJUKnUo2S/ApIOFUA4IMK/Tg==", null, false, "c5f3c4fe-4639-40fd-9abf-9beeaf89b2f1", false, "beth" },
                    { 3, 0, "92b7321a-0a47-4fe2-a4f1-0f85ecae8080", "Monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAEKSIUW+za8R5GnKbOv4ENyG4crUxVPCQxQPP2t2OCYIis4LPkOLSH30nJpomXZLVaw==", null, false, "21f76505-c31a-4bb7-b332-68d54d80705c", false, "monika" },
                    { 4, 0, "06ea01b6-3e58-4984-91f4-c915c30064f3", "John@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEKqwq8AbjUzMQxuZDjSz2gcGcE1BMb1L1Y5ZckbFH0mYRTGhIjkkkOYespy5R9BU2g==", null, false, "8869d7e5-a5a3-45db-b550-4febccba6e6f", false, "john" },
                    { 5, 0, "3e698194-32ff-438a-afe0-9f45b57110fa", "James@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEHoFqzPy31Sc6PdptCGJfudYvFKpY7CqwEEs8yDEt1yMJHoJh7qJWPzaucSYZvD27Q==", null, false, "45d5014e-4cec-48a1-bf0c-70ab6283df78", false, "james" },
                    { 6, 0, "d5bd603f-8648-43bd-8f6f-c5964f7b41b8", "Filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAELoKEms2osTUlhaChPTPV4BPLjRwu76qJbnLcq8WhKhPqEHbVg89pb2j7v++dA+kyg==", null, false, "f3d4b675-b854-4ab9-bd62-1b9a6c0071b8", false, "filip" },
                    { 7, 0, "0dd28b65-780b-4dd3-9a38-3c415a2738b7", "Random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEKUAqHHTvnC+TIGxjKRqhXEs1sGvv/I4kDVZm/4gZhMgAPQQz80iFXje3iJ7xasu9A==", null, false, "2ac818a9-d76d-47fb-8878-218689a29e2c", false, "random" },
                    { 8, 0, "cfdf2ef8-3fb8-4f91-9c97-28dbec1f5ef6", "Jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEDRV1okO27uAK9SJjrDBASb5064agnd9ZtERGBZHsQTdxa8yms59ENty6DkdavFNiw==", null, false, "329724d6-bf2e-4495-9f7d-9d75b721712e", false, "jack" },
                    { 9, 0, "99035666-b790-457f-8439-a8b3fcf36790", "Tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEJR8OQz7YjoP4yuEa67iO7pPvEWNSAhxFtomEiDq+BE2CI4LeGHguloISDflkh+90Q==", null, false, "51ae49a1-9c91-46c2-82d0-5eb2817c77a2", false, "tom" },
                    { 10, 0, "6327e3fd-f88f-4523-af1d-c8bfafa7462f", "Ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEM+LtzEGe/dWm2gKhApqNEmR21MH6+w+TXxIY3ix4zE+wLfBDJLITkecF0VTxop6rQ==", null, false, "0fe83b07-aa72-4950-9b32-a0ec059b8cc7", false, "ali" },
                    { 11, 0, "dae1af43-4388-49f1-a651-5d177f42ba56", "Rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEOTiFNBouY576qJZqAYLr66KAoWWNcC+WErx9W+yeXL+qGNEPMOiLnOIMTy7oqaMJw==", null, false, "af6c60ca-8c94-4988-8144-bdd24ea40af2", false, "rubber" },
                    { 12, 0, "5f7bf594-9644-426e-9d2f-2f255d160c87", "Olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEFKksxSmJiDt8uLJy6LQpY/9nXkxG6Yw1njsZC2udzr+IkAK2JKdCUYGn5TGiS0e9g==", null, false, "b0a9208e-f836-43dc-b3c7-10ba8b0b0452", false, "olaf" },
                    { 13, 0, "69612f1b-bba4-4b23-874b-885e0a885107", "Good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEEUgTd6EGbNizDX/M70NBUHgYTtN8oN2GWr1Ln9MiiczSv0atoGq656+tKSA+hlYJA==", null, false, "d79a32ab-3c11-4da5-8130-0ccca7bd36af", false, "good" },
                    { 14, 0, "cda6e988-d51d-49ad-88aa-de9ed24021e3", "Tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAELnDJvI31NSexE9ZmCRxK6cKzyfYWFpsbXPn2HAqerglthNXxgIgA+yaOzw2/L3Dog==", null, false, "692c8792-44c4-4a3a-976e-ab0ebdb24d82", false, "tim" },
                    { 15, 0, "6916b278-9868-4426-9938-0d6cb69122ff", "Adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAELhWEvp7FqhQQIxRA6dtc0nFnbmj4AUbozVRNiw0JoSdIaNggKZ4Ff2hH35hg6E0yw==", null, false, "a68aa38f-0237-4ac6-be1a-aeb94d1792f7", false, "adam" }
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
                    { 1, "To Kill a Mockingbird", 51, 21.54m, 7, 11, 32 },
                    { 2, "1984", 56, 18.59m, 12, 1, 40 },
                    { 3, "James Bond", 30, 8.31m, 8, 1, 45 },
                    { 4, "The Great Gatsby", 56, 14.81m, 4, 13, 45 },
                    { 5, "One Hundred Years of Solitude", 42, 18.2m, 3, 9, 9 },
                    { 6, "The Catcher in the Rye", 82, 5.12m, 11, 1, 16 },
                    { 7, "Brave New World", 51, 22.17m, 11, 8, 44 },
                    { 8, "The Hobbit", 98, 17.3m, 8, 3, 4 },
                    { 9, "Love and Basketball", 52, 11.24m, 11, 9, 9 },
                    { 10, "Pride and Prejudice", 70, 10.59m, 8, 7, 46 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 77, 9.22m, 15, 14, 21 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 76, 24.21m, 6, 11, 25 },
                    { 13, "Harry Potter and the Philosopher's Stone", 41, 15.65m, 14, 13, 8 },
                    { 14, "The Hunger Games", 78, 18.31m, 1, 8, 6 },
                    { 15, "The Da Vinci Code", 57, 8.06m, 6, 4, 4 },
                    { 16, "A Game of Thrones", 59, 18.96m, 4, 11, 22 },
                    { 17, "The Shining", 74, 9.49m, 13, 1, 6 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 54, 6.83m, 13, 13, 6 },
                    { 19, "The Alchemist", 70, 7.93m, 16, 7, 33 },
                    { 20, "War and Peace", 98, 7.15m, 14, 7, 35 },
                    { 21, "Crime and Punishment", 75, 8.67m, 8, 3, 7 },
                    { 22, "The Catch-22", 89, 5.34m, 16, 5, 23 },
                    { 23, "The Grapes of Wrath", 67, 17.01m, 16, 8, 45 },
                    { 24, "Fahrenheit 451", 66, 9.76m, 18, 2, 10 },
                    { 25, "Lord of the Flies", 46, 15.03m, 16, 4, 35 },
                    { 26, "Moby-Dick", 45, 18.46m, 14, 9, 39 },
                    { 27, "Frankenstein", 35, 12.91m, 7, 5, 10 },
                    { 28, "Alice's Adventures in Wonderland", 35, 7.69m, 3, 11, 19 },
                    { 29, "Dracula", 63, 19.42m, 18, 6, 1 },
                    { 30, "The Odyssey", 82, 19.28m, 17, 9, 11 },
                    { 31, "Romeo and Juliet", 64, 14.54m, 6, 9, 9 },
                    { 32, "Hamlet", 82, 16.59m, 2, 13, 16 },
                    { 33, "Macbeth", 98, 11.32m, 14, 1, 43 },
                    { 34, "Othello", 59, 14.04m, 17, 7, 23 },
                    { 35, "The Divine Comedy", 32, 13.95m, 3, 4, 23 },
                    { 36, "Don Quixote", 35, 7.3m, 15, 7, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "PaymentStatus", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8648), 1, 24.84m, 3 },
                    { 2, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8783), 3, 42.77m, 1 },
                    { 3, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8788), 3, 53.64m, 2 },
                    { 4, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8791), 4, 51.77m, 3 },
                    { 5, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8805), 1, 39.03m, 2 },
                    { 6, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8809), 0, 33.91m, 2 },
                    { 7, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8811), 4, 37.08m, 2 },
                    { 8, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8813), 1, 17.17m, 1 },
                    { 9, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8815), 2, 29.27m, 1 },
                    { 10, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8818), 1, 37.47m, 1 },
                    { 11, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8820), 0, 6.98m, 3 },
                    { 12, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8822), 2, 14.26m, 3 },
                    { 13, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8825), 2, 52.4m, 3 },
                    { 14, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8827), 0, 41.88m, 4 },
                    { 15, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8829), 3, 11.79m, 3 },
                    { 16, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8853), 1, 10.58m, 3 },
                    { 17, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8855), 0, 51.46m, 1 },
                    { 18, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8858), 3, 48.72m, 4 },
                    { 19, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8860), 0, 13.14m, 2 },
                    { 20, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8863), 1, 19.3m, 2 },
                    { 21, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8865), 0, 17.69m, 3 },
                    { 22, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8867), 0, 6.56m, 2 },
                    { 23, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8869), 3, 32.72m, 4 },
                    { 24, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8914), 2, 36.49m, 3 },
                    { 25, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8918), 2, 15.62m, 3 },
                    { 26, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8920), 3, 39.04m, 3 },
                    { 27, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8922), 1, 19.99m, 4 },
                    { 28, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8924), 4, 30.27m, 1 },
                    { 29, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8926), 2, 6.59m, 1 },
                    { 30, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8932), 4, 28.37m, 1 },
                    { 31, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8975), 1, 8.04m, 4 },
                    { 32, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8978), 2, 40.11m, 1 },
                    { 33, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8980), 4, 12.59m, 4 },
                    { 34, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8983), 2, 17.71m, 3 },
                    { 35, new DateTime(2024, 1, 6, 1, 28, 14, 962, DateTimeKind.Local).AddTicks(8985), 3, 44.07m, 4 }
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
                    { 2, 16, 5 },
                    { 2, 25, 6 },
                    { 3, 1, 2 },
                    { 3, 26, 8 },
                    { 5, 10, 2 },
                    { 5, 18, 4 },
                    { 5, 19, 9 },
                    { 5, 22, 3 },
                    { 5, 28, 3 },
                    { 6, 8, 5 },
                    { 6, 9, 1 },
                    { 6, 15, 7 },
                    { 7, 6, 1 },
                    { 8, 31, 8 },
                    { 8, 32, 9 },
                    { 9, 35, 5 },
                    { 10, 23, 2 },
                    { 11, 5, 5 },
                    { 11, 29, 3 },
                    { 13, 12, 6 },
                    { 18, 2, 4 },
                    { 18, 3, 9 },
                    { 19, 21, 5 },
                    { 20, 4, 9 },
                    { 21, 13, 9 },
                    { 22, 33, 4 },
                    { 25, 17, 2 },
                    { 25, 24, 8 },
                    { 26, 14, 3 },
                    { 28, 27, 7 },
                    { 29, 30, 9 },
                    { 30, 20, 8 },
                    { 32, 11, 7 },
                    { 32, 34, 9 },
                    { 34, 7, 5 }
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
                    { 1, 18, "Great book but it gave me an existential crisis bigger than I had before", 12, 52 },
                    { 2, 24, "Couldn't put it down, finished it in one sitting!", 3, 31 },
                    { 3, 22, "The plot twists in this book are mind-blowing", 1, 10 },
                    { 4, 5, "A classic that everyone should read", 2, 71 },
                    { 5, 28, "The characters are so well-developed, felt like they were real people", 7, 58 },
                    { 6, 25, "This book made me laugh and cry, a roller coaster of emotions", 5, 75 },
                    { 7, 9, "The writing style is beautiful, every sentence is a work of art", 9, 38 },
                    { 8, 26, "I couldn't guess the ending, kept me guessing until the last page", 1, 12 },
                    { 9, 12, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 8, 68 },
                    { 10, 22, "The themes explored in this book are thought-provoking", 7, 69 },
                    { 11, 9, "The pacing is perfect, kept me engaged from start to finish", 8, 83 },
                    { 12, 7, "This book challenged my perspective on life", 6, 47 },
                    { 13, 2, "The world-building is exceptional, I felt like I was there", 11, 32 },
                    { 14, 30, "A must-read for book lovers", 4, 35 },
                    { 15, 27, "The author's storytelling is captivating", 11, 23 },
                    { 16, 10, "This book is a page-turner, couldn't stop reading", 11, 63 },
                    { 17, 18, "The dialogue between characters is witty and realistic", 2, 23 },
                    { 18, 21, "I've recommended this book to all my friends", 5, 48 },
                    { 19, 23, "It left me with a book hangover, couldn't stop thinking about it", 10, 83 },
                    { 20, 29, "", 7, 15 },
                    { 21, 3, "Couldn't get into the story, found it boring from the start", 4, 68 },
                    { 22, 12, "The characters felt one-dimensional and uninteresting", 6, 96 },
                    { 23, 30, "The plot was predictable, I expected more twists", 13, 89 },
                    { 24, 17, "I didn't connect with the protagonist, lacked depth", 9, 29 },
                    { 25, 6, "The writing style was confusing and hard to follow", 11, 42 },
                    { 26, 10, "This book didn't live up to the hype, very disappointing", 6, 25 },
                    { 27, 34, "The ending felt rushed and unresolved", 3, 69 },
                    { 28, 11, "Too much exposition, not enough action", 10, 96 },
                    { 29, 13, "I found the dialogue unrealistic and forced", 7, 86 },
                    { 30, 14, "The author tried too hard to be profound, came off as pretentious", 14, 84 },
                    { 31, 27, "The pacing was off, some parts dragged on while others felt rushed", 1, 94 },
                    { 32, 21, "The world-building was weak and inconsistent", 6, 71 },
                    { 33, 1, "I couldn't sympathize with any of the characters", 9, 58 },
                    { 34, 1, "The themes explored were cliché and overdone", 14, 14 },
                    { 35, 26, "The book didn't live up to the reviews, a letdown", 3, 56 },
                    { 36, 34, "The grammar and editing were poor, distracting from the story", 3, 66 },
                    { 37, 31, "The book felt like a rip-off of [another popular book]", 6, 31 },
                    { 38, 3, "The author relied too heavily on stereotypes", 2, 62 },
                    { 39, 3, "I regret spending time on this book, wish I chose something else", 10, 33 },
                    { 40, 31, "The climax was anticlimactic, left me unsatisfied", 12, 61 },
                    { 41, 34, "Great book but it gave me an existential crisis bigger than I had before", 5, 74 },
                    { 42, 33, "Couldn't put it down, finished it in one sitting!", 2, 20 },
                    { 43, 24, "The plot twists in this book are mind-blowing", 12, 47 },
                    { 44, 28, "A classic that everyone should read", 6, 27 },
                    { 45, 16, "The characters are so well-developed, felt like they were real people", 6, 56 },
                    { 46, 3, "This book made me laugh and cry, a roller coaster of emotions", 11, 39 },
                    { 47, 23, "The writing style is beautiful, every sentence is a work of art", 8, 65 },
                    { 48, 15, "I couldn't guess the ending, kept me guessing until the last page", 6, 49 },
                    { 49, 23, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 1, 26 },
                    { 50, 28, "The themes explored in this book are thought-provoking", 7, 70 }
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
