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
                name: "BookOrder",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "integer", nullable: false),
                    OrdersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrder", x => new { x.BooksId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_BookOrder_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
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
                    { 1, 0, "87c4f939-f940-4849-aa44-3d1b0b9dfadd", "Roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEGDqLPqqhlF9J/CigwKU6mq4TRl6FiCjyFKhIZpZgIpqmZMAs/Q7Yp/UxwWUy8RXow==", null, false, "bbabc82e-1543-46f6-b863-905878af9dc9", false, "roman" },
                    { 2, 0, "78619fb3-d5e9-4769-bc0e-df2c32169c46", "Beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEJgj2r0dWZ4uJlmJegr8qrdSl0vrb+xcjQwmZBrgBb1YQ+oMGWqpS4z9uf5NbY1fIg==", null, false, "f387f34d-20e1-47a0-8aa0-f8b855dba9e3", false, "beth" },
                    { 3, 0, "53f4aabf-c878-441a-8657-d4b804cbb9b9", "Monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAEL+rzquUbJWLRnYkbIthyd6554/wMsJk/1utcHDfByTxDljNW4ZQ5xJgqmE36lFGVg==", null, false, "fa228dca-d79f-457a-8218-278843dcc687", false, "monika" },
                    { 4, 0, "c3c9099a-3529-4eb0-911b-5132961b2ae5", "John@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEPXMwTg9KGZz+zQgzjKphWo1a6aTXGjKLHzz/KIsQq0Zhn3WomsUM+UfoUYJwro8tw==", null, false, "c601fdbf-8774-4da0-bd15-2627884cbec3", false, "john" },
                    { 5, 0, "9c786dae-87fb-4496-be45-447210d6a5d2", "James@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEJsVVq96QHOvMTiaOj+BOgRw8Q2YSbuNCU9qJSa2muu4wTo+/TA4yWZIu9f9S++0WQ==", null, false, "d24f20fe-d4d5-4afb-9dab-0031d7e82b37", false, "james" },
                    { 6, 0, "c1ef4b60-c5ee-43db-ac8f-eeb89b9f5eb4", "Filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAELhH+RTmw28/YxdVKUpmvFpqoLHAjloinZ0xaOvKcpfDFB7nl9FykZwgzhHedp2NIw==", null, false, "b8c14e02-400c-4529-b9e1-90f4bb2f6543", false, "filip" },
                    { 7, 0, "a4b11c75-b37c-4367-be2b-d7c835971e0f", "Random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEET+luGa9SroCr/xeW8qpmG3U0KdLdyWFTk1T3oSRW+EAKwxjwf/xLc/3jW2ZHND6w==", null, false, "b6f485a2-58c5-4410-b957-58a49eccd28e", false, "random" },
                    { 8, 0, "a7b5add9-6010-48d1-bf69-b1ba0b242050", "Jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEGpgN+bTf+Mz1IWh515NBl/qD1c1RI7it3Lrz7hgi2QfElUmuN76HUPfalPGnOO6Og==", null, false, "4e1259f0-0732-4814-aefb-23c9d0d62ef2", false, "jack" },
                    { 9, 0, "9c34f033-b40d-456b-8f66-3deaa7c714a0", "Tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEC0YUXF3tC9NOj/ksWoBOpqAaCCZ6dDlk1hU+mH5Kbt1bQ74ut127hdvl6q4hDNbYw==", null, false, "56b55472-64a5-4c92-a804-22954e0f3df5", false, "tom" },
                    { 10, 0, "82a81a86-7afb-432c-a87b-4c8e7de13dee", "Ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEFouqz+3jNlqU1F2IoMiFiP8olAn2N9grxB012DYmY3Nv/BKMcaDKn2ONzq/i36y1w==", null, false, "328e63f7-0ad7-45a9-b094-4e31a77cc810", false, "ali" },
                    { 11, 0, "bf481069-0649-4855-ba21-84f1a484b46a", "Rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEKUxjCm38yE0Q65hISqkFn5O1IbaOls6goaSXrLHxdFpjg7Kb0QjXPft14r1R3qiJg==", null, false, "08258aa7-823d-4890-8557-966ba11739ce", false, "rubber" },
                    { 12, 0, "73a08af0-2d13-4375-9144-efee090b7873", "Olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEDiuQbPnimn4hmya5bPjzuQBGIuQL0Z+AaWV/QrjpTulWCQhvD6ya3L+SlkJMvEXJw==", null, false, "37e537c5-e4f0-446a-bd36-2946c7698d0d", false, "olaf" },
                    { 13, 0, "ff50ea07-9d20-4c55-896e-70822b0da9c7", "Good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEOj0CTW+F3/8geiWlRFcZOvNdBIJqHnKoyHAzH2amHfONMxsS+F3wSxf4tfrIY4qHA==", null, false, "0a68ab60-b362-40a8-bbfc-505cbd9ea20b", false, "good" },
                    { 14, 0, "135ef7e7-4e3f-4c14-9d36-207ea5a2bee0", "Tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAEJtGKusqILPxMRnx1qgkboaMNN9wIZfHrn/boUVBx5ioWaFpszOiIIXh0ZrNcLW0lw==", null, false, "0dedf43c-22b3-401d-bfd3-c5ab23ec3773", false, "tim" },
                    { 15, 0, "b42e8a4e-d4c0-491a-afe4-7fa320daaeb5", "Adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEHjJ30nB51b+JR+tgGWSz86WQ275IT1DYKlnWNm8T85hKi0z/9m+Aqgi7eGUdN0OxQ==", null, false, "86d18f2c-75b5-497c-a8b9-664eba2031b2", false, "adam" }
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
                    { 1, "To Kill a Mockingbird", 52, 12.44m, 18, 1, 44 },
                    { 2, "1984", 88, 19.8m, 15, 1, 3 },
                    { 3, "James Bond", 75, 9.01m, 16, 1, 35 },
                    { 4, "The Great Gatsby", 82, 9.2m, 15, 1, 26 },
                    { 5, "One Hundred Years of Solitude", 99, 23.04m, 19, 1, 44 },
                    { 6, "The Catcher in the Rye", 61, 9.5m, 18, 1, 35 },
                    { 7, "Brave New World", 41, 19.28m, 2, 1, 36 },
                    { 8, "The Hobbit", 98, 7.69m, 1, 1, 3 },
                    { 9, "Love and Basketball", 95, 8.3m, 17, 1, 3 },
                    { 10, "Pride and Prejudice", 71, 9.11m, 7, 1, 47 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 46, 10.73m, 5, 1, 6 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 87, 14.07m, 18, 1, 37 },
                    { 13, "Harry Potter and the Philosopher's Stone", 39, 7.12m, 14, 1, 28 },
                    { 14, "The Hunger Games", 46, 16.49m, 13, 1, 4 },
                    { 15, "The Da Vinci Code", 92, 18.57m, 13, 1, 47 },
                    { 16, "A Game of Thrones", 43, 13.6m, 19, 1, 35 },
                    { 17, "The Shining", 49, 21.74m, 16, 1, 1 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 51, 6.55m, 13, 1, 26 },
                    { 19, "The Alchemist", 95, 24.9m, 13, 1, 19 },
                    { 20, "War and Peace", 89, 14.53m, 1, 1, 20 },
                    { 21, "Crime and Punishment", 55, 7.47m, 10, 1, 35 },
                    { 22, "The Catch-22", 55, 8.18m, 13, 1, 40 },
                    { 23, "The Grapes of Wrath", 43, 21.65m, 4, 1, 11 },
                    { 24, "Fahrenheit 451", 90, 22.95m, 3, 1, 15 },
                    { 25, "Lord of the Flies", 72, 20.47m, 3, 1, 42 },
                    { 26, "Moby-Dick", 83, 9.35m, 13, 1, 14 },
                    { 27, "Frankenstein", 37, 7.35m, 5, 1, 20 },
                    { 28, "Alice's Adventures in Wonderland", 56, 20.25m, 3, 1, 35 },
                    { 29, "Dracula", 58, 9.8m, 6, 1, 4 },
                    { 30, "The Odyssey", 81, 18.9m, 15, 1, 44 },
                    { 31, "Romeo and Juliet", 65, 10.96m, 4, 1, 38 },
                    { 32, "Hamlet", 43, 11.87m, 18, 1, 25 },
                    { 33, "Macbeth", 87, 16.32m, 1, 1, 12 },
                    { 34, "Othello", 40, 14.43m, 8, 1, 47 },
                    { 35, "The Divine Comedy", 37, 18.63m, 9, 1, 21 },
                    { 36, "Don Quixote", 79, 11.51m, 10, 1, 43 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8123), 25.29m, 9 },
                    { 2, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8240), 40m, 3 },
                    { 3, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8244), 6.36m, 7 },
                    { 4, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8246), 15.02m, 6 },
                    { 5, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8256), 20.89m, 14 },
                    { 6, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8259), 10.52m, 8 },
                    { 7, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8261), 34.51m, 13 },
                    { 8, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8263), 38.04m, 5 },
                    { 9, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8265), 43.77m, 11 },
                    { 10, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8267), 45.61m, 4 },
                    { 11, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8268), 23.09m, 11 },
                    { 12, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8270), 21.27m, 10 },
                    { 13, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8272), 39.84m, 3 },
                    { 14, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8274), 32.71m, 2 },
                    { 15, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8276), 27.03m, 12 },
                    { 16, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8300), 13.17m, 13 },
                    { 17, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8302), 6.94m, 10 },
                    { 18, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8304), 11.12m, 9 },
                    { 19, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8306), 45.39m, 14 },
                    { 20, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8308), 29.64m, 1 },
                    { 21, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8310), 42.34m, 6 },
                    { 22, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8312), 53.04m, 10 },
                    { 23, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8314), 34.77m, 1 },
                    { 24, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8316), 45.18m, 4 },
                    { 25, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8317), 21.67m, 8 },
                    { 26, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8319), 6.54m, 13 },
                    { 27, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8321), 23.22m, 9 },
                    { 28, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8323), 6.16m, 1 },
                    { 29, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8325), 28.87m, 3 },
                    { 30, new DateTime(2023, 12, 26, 16, 46, 9, 304, DateTimeKind.Local).AddTicks(8339), 15.17m, 4 }
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
                table: "BookOrder",
                columns: new[] { "BooksId", "OrdersId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 8 },
                    { 3, 13 },
                    { 4, 3 },
                    { 5, 4 },
                    { 5, 30 },
                    { 6, 5 },
                    { 7, 6 },
                    { 7, 29 },
                    { 8, 7 },
                    { 9, 28 },
                    { 10, 27 },
                    { 11, 10 },
                    { 12, 9 },
                    { 12, 11 },
                    { 13, 26 },
                    { 14, 25 },
                    { 15, 12 },
                    { 16, 24 },
                    { 17, 23 },
                    { 18, 14 },
                    { 19, 12 },
                    { 19, 15 },
                    { 19, 22 },
                    { 20, 20 },
                    { 20, 21 },
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
                    { 32, 16 },
                    { 33, 16 },
                    { 33, 17 },
                    { 34, 18 },
                    { 35, 3 },
                    { 35, 8 },
                    { 35, 19 }
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
                    { 1, 19, "Great book but it gave me an existential crisis bigger than I had before", 1, 27 },
                    { 2, 30, "Couldn't put it down, finished it in one sitting!", 9, 12 },
                    { 3, 31, "The plot twists in this book are mind-blowing", 3, 77 },
                    { 4, 21, "A classic that everyone should read", 12, 27 },
                    { 5, 12, "The characters are so well-developed, felt like they were real people", 9, 85 },
                    { 6, 1, "This book made me laugh and cry, a roller coaster of emotions", 8, 95 },
                    { 7, 12, "The writing style is beautiful, every sentence is a work of art", 12, 71 },
                    { 8, 13, "I couldn't guess the ending, kept me guessing until the last page", 13, 69 },
                    { 9, 4, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 12, 16 },
                    { 10, 18, "The themes explored in this book are thought-provoking", 5, 35 },
                    { 11, 7, "The pacing is perfect, kept me engaged from start to finish", 9, 94 },
                    { 12, 5, "This book challenged my perspective on life", 2, 79 },
                    { 13, 8, "The world-building is exceptional, I felt like I was there", 7, 40 },
                    { 14, 25, "A must-read for book lovers", 4, 39 },
                    { 15, 29, "The author's storytelling is captivating", 5, 12 },
                    { 16, 16, "This book is a page-turner, couldn't stop reading", 7, 28 },
                    { 17, 28, "The dialogue between characters is witty and realistic", 6, 16 },
                    { 18, 1, "I've recommended this book to all my friends", 4, 50 },
                    { 19, 19, "It left me with a book hangover, couldn't stop thinking about it", 10, 29 },
                    { 20, 9, "", 2, 10 },
                    { 21, 29, "Couldn't get into the story, found it boring from the start", 4, 59 },
                    { 22, 7, "The characters felt one-dimensional and uninteresting", 2, 34 },
                    { 23, 19, "The plot was predictable, I expected more twists", 2, 49 },
                    { 24, 1, "I didn't connect with the protagonist, lacked depth", 2, 16 },
                    { 25, 10, "The writing style was confusing and hard to follow", 8, 20 },
                    { 26, 1, "This book didn't live up to the hype, very disappointing", 13, 16 },
                    { 27, 22, "The ending felt rushed and unresolved", 12, 63 },
                    { 28, 23, "Too much exposition, not enough action", 2, 10 },
                    { 29, 31, "I found the dialogue unrealistic and forced", 13, 43 },
                    { 30, 22, "The author tried too hard to be profound, came off as pretentious", 2, 50 },
                    { 31, 26, "The pacing was off, some parts dragged on while others felt rushed", 12, 21 },
                    { 32, 29, "The world-building was weak and inconsistent", 11, 42 },
                    { 33, 5, "I couldn't sympathize with any of the characters", 1, 51 },
                    { 34, 11, "The themes explored were cliché and overdone", 3, 73 },
                    { 35, 18, "The book didn't live up to the reviews, a letdown", 13, 38 },
                    { 36, 28, "The grammar and editing were poor, distracting from the story", 12, 63 },
                    { 37, 13, "The book felt like a rip-off of [another popular book]", 7, 12 },
                    { 38, 32, "The author relied too heavily on stereotypes", 8, 63 },
                    { 39, 28, "I regret spending time on this book, wish I chose something else", 2, 42 },
                    { 40, 4, "The climax was anticlimactic, left me unsatisfied", 11, 50 },
                    { 41, 20, "Great book but it gave me an existential crisis bigger than I had before", 5, 80 },
                    { 42, 23, "Couldn't put it down, finished it in one sitting!", 10, 29 },
                    { 43, 16, "The plot twists in this book are mind-blowing", 8, 82 },
                    { 44, 22, "A classic that everyone should read", 6, 93 },
                    { 45, 31, "The characters are so well-developed, felt like they were real people", 3, 12 },
                    { 46, 16, "This book made me laugh and cry, a roller coaster of emotions", 9, 31 },
                    { 47, 15, "The writing style is beautiful, every sentence is a work of art", 3, 49 },
                    { 48, 20, "I couldn't guess the ending, kept me guessing until the last page", 14, 33 },
                    { 49, 29, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 8, 11 },
                    { 50, 2, "The themes explored in this book are thought-provoking", 3, 25 }
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
                name: "IX_BookOrder_OrdersId",
                table: "BookOrder",
                column: "OrdersId");

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
                name: "BookOrder");

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
