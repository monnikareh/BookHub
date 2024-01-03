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
                    { 1, 0, "feef03e1-15f6-486e-b6d3-e8050e19ae6c", "Roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEH6C+4MamiRLn0ul05P83MjZ7x2ncOWgBllCf0ucSfaLD0XgrkkG6MeCu/Gx1Yf9zA==", null, false, "ea682c0c-84af-4767-90a6-a771fd4574cc", false, "roman" },
                    { 2, 0, "453f2ee2-0c6e-497d-9e9e-7eb925b24792", "Beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEA2XFbfpgfpmgcv0opVWpoLIfy6NT4uk4l1DX/Qe9qIA/jrjBnUKDJ6SWfvGfxv6lw==", null, false, "f7267ce6-de57-43e5-a51c-77db61e2d428", false, "beth" },
                    { 3, 0, "c996284c-c2d4-4240-b30b-1cb39bb3ab2f", "Monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAECbTWylvL6V4Xd17SkATeeSgV/dlOTzE6AR/UhfOOS1Ni+5a+baJNb6AQqpxIG5IVQ==", null, false, "745ded9e-afcc-44e2-9153-404fd05aaf69", false, "monika" },
                    { 4, 0, "df996483-45c6-4708-bd01-0847ccf1a3a0", "John@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEDf8HpIIGlso/0HeZ3u8q3s1PayRQfImtr1mCXP82wn+CXCeDxPntJ1W5z+9Lyd4JA==", null, false, "388843f0-8b9d-408f-9335-9f93233f0cc6", false, "john" },
                    { 5, 0, "b825bed2-76e1-4542-bf34-1446ef2ad02d", "James@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEK4NW6TyGZRHLqpKfeg7A1pzmW8NVJbzAgsEZP49CfpVBXGR8r+DjIBKoYZU1qVgPg==", null, false, "55095d4d-fc8e-451c-b500-83b889b1b86a", false, "james" },
                    { 6, 0, "f5a02965-9131-4d27-adc0-90e7f48f32c0", "Filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAECS4/gQgYj9HrZdp7t3rppx0kTU28ystjN99DvtRJNPSEFUHYWycZlCU+zEzHXtPcg==", null, false, "ff311d5e-638d-47ca-9858-f9cab9e54b70", false, "filip" },
                    { 7, 0, "ca756bf5-190d-4a19-9c21-b36d4593704a", "Random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEJMw55QoFOd4TRhO2fbqUfDLVPCc2Snq6X/SY/51iPcvUtM1HmzigJuUR97UfPuuzg==", null, false, "d22fccd6-c035-43d7-aaa9-343faff01bd8", false, "random" },
                    { 8, 0, "8b715159-fb27-4749-8974-30c552f57329", "Jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEIJQP3vstwaE0V5aApIz6RK39FalfSu2YDxBal/6gTpWir7DtCsGs9i0pE4aa3k43w==", null, false, "3d15d2d0-3179-4679-a061-e62f2b41ac4d", false, "jack" },
                    { 9, 0, "8adbb9b1-a383-49de-909b-99344a5d162b", "Tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEB+zYnPugU0e+fXyQtKniL58C23YikeiPOjYKhYa28zK3+L0B0YZ7KA9+u0jjm4RYg==", null, false, "86b0df7d-638c-4da7-b37b-e0783239bb0e", false, "tom" },
                    { 10, 0, "cc538191-3aac-470b-ae73-c121702797ef", "Ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEIClCQgWNuv0IB2RkCSfcdjdI4zvP36BrrTMOJX859Ic1dJpGeunKVdxsBkoalfdmw==", null, false, "1902db6e-b287-42cd-b130-0874205d0852", false, "ali" },
                    { 11, 0, "5e7c39bf-b40c-4b09-9a2d-a3d7b0f07f23", "Rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAELNznTgncLEpo3rtQgkzg00IPJGq+LbXyzwpR3OfqXr20VOJczU7p53WFdV+d3Lwig==", null, false, "eae9fe62-e4cf-4b3d-ae37-0805ea8f2c7c", false, "rubber" },
                    { 12, 0, "fdd58b9b-fc5e-4cb6-9048-95fdd030ef3e", "Olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEJkF6trIA72hAXejaO+gH/h8ym+OMUMrsjUD7qLIPpP9czN8TmdslRIBWo2AiK0GsQ==", null, false, "94fa9f83-fe00-46c3-8baa-7ac428bcd11a", false, "olaf" },
                    { 13, 0, "cf4c79f2-e48b-46f4-b802-573b13b3a7bd", "Good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEJKG3LjE5aQyUO+e/byEwblBURM90+LEzjsYtRA8Nu8Oid13vgg/VGt9VzQj16hVPg==", null, false, "7957ef14-bad9-407b-bd8c-be9e31d81444", false, "good" },
                    { 14, 0, "2091a370-26c9-4f12-b406-4d4a1d41da25", "Tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAEGseOH13gRV3joZKjfhz/TqhvaBcjPx6eNjREEnCsOzdOiMBf7Wy1pFHseII66832A==", null, false, "a2b1d739-5850-4e14-9f1d-fbf2ccbf76c4", false, "tim" },
                    { 15, 0, "c14156c1-081e-48ad-83af-a1235fb04ca1", "Adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEP8ACMqkaYSgtY/oj8jZ7hIkFGIX3BOpzqseDctNpbfb0hs/+zBxLeVz1xvXnNsaog==", null, false, "ed9e54cb-8359-4924-bdb2-c30716d9ba1b", false, "adam" }
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
                    { 1, "To Kill a Mockingbird", 55, 14.6m, 15, 1, 13 },
                    { 2, "1984", 93, 22.48m, 11, 14, 1 },
                    { 3, "James Bond", 81, 21.49m, 2, 7, 36 },
                    { 4, "The Great Gatsby", 40, 20.25m, 18, 14, 34 },
                    { 5, "One Hundred Years of Solitude", 39, 10.8m, 8, 8, 45 },
                    { 6, "The Catcher in the Rye", 55, 22.61m, 12, 11, 21 },
                    { 7, "Brave New World", 54, 18.03m, 1, 3, 49 },
                    { 8, "The Hobbit", 55, 22.57m, 19, 11, 4 },
                    { 9, "Love and Basketball", 74, 12.11m, 14, 3, 42 },
                    { 10, "Pride and Prejudice", 53, 19.61m, 19, 13, 9 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 47, 23.99m, 18, 5, 49 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 74, 17.23m, 12, 6, 24 },
                    { 13, "Harry Potter and the Philosopher's Stone", 42, 17.48m, 19, 8, 37 },
                    { 14, "The Hunger Games", 55, 12.44m, 2, 9, 6 },
                    { 15, "The Da Vinci Code", 49, 15.94m, 13, 7, 35 },
                    { 16, "A Game of Thrones", 35, 21.1m, 7, 13, 40 },
                    { 17, "The Shining", 35, 5.8m, 5, 3, 11 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 52, 12.25m, 18, 8, 45 },
                    { 19, "The Alchemist", 63, 5.23m, 9, 7, 20 },
                    { 20, "War and Peace", 81, 6.77m, 11, 11, 16 },
                    { 21, "Crime and Punishment", 34, 6.52m, 18, 10, 41 },
                    { 22, "The Catch-22", 53, 15.15m, 17, 10, 29 },
                    { 23, "The Grapes of Wrath", 81, 8.75m, 7, 5, 48 },
                    { 24, "Fahrenheit 451", 83, 9.81m, 13, 5, 6 },
                    { 25, "Lord of the Flies", 36, 19.78m, 11, 14, 8 },
                    { 26, "Moby-Dick", 92, 7.73m, 1, 3, 1 },
                    { 27, "Frankenstein", 93, 5.94m, 7, 4, 30 },
                    { 28, "Alice's Adventures in Wonderland", 46, 20.91m, 16, 5, 49 },
                    { 29, "Dracula", 61, 17.59m, 8, 4, 28 },
                    { 30, "The Odyssey", 74, 21.72m, 7, 5, 15 },
                    { 31, "Romeo and Juliet", 41, 7.56m, 8, 2, 3 },
                    { 32, "Hamlet", 60, 20.43m, 1, 3, 49 },
                    { 33, "Macbeth", 75, 5.14m, 19, 7, 32 },
                    { 34, "Othello", 77, 24.56m, 12, 1, 29 },
                    { 35, "The Divine Comedy", 37, 14.57m, 4, 13, 4 },
                    { 36, "Don Quixote", 99, 20.81m, 5, 12, 48 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7598), 34.88m, 8 },
                    { 2, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7735), 7.56m, 11 },
                    { 3, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7738), 18.28m, 2 },
                    { 4, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7741), 22.1m, 12 },
                    { 5, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7750), 29.82m, 1 },
                    { 6, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7753), 38.2m, 3 },
                    { 7, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7754), 7.13m, 3 },
                    { 8, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7756), 54.35m, 7 },
                    { 9, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7758), 10.6m, 12 },
                    { 10, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7760), 29.92m, 3 },
                    { 11, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7762), 15.1m, 8 },
                    { 12, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7763), 17.76m, 10 },
                    { 13, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7765), 49.15m, 5 },
                    { 14, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7767), 29.23m, 2 },
                    { 15, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7768), 14.9m, 9 },
                    { 16, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7787), 35.49m, 9 },
                    { 17, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7789), 39.67m, 2 },
                    { 18, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7791), 54.46m, 1 },
                    { 19, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7793), 52.73m, 12 },
                    { 20, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7795), 40.01m, 9 },
                    { 21, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7796), 6.26m, 3 },
                    { 22, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7798), 42.97m, 12 },
                    { 23, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7799), 18.03m, 14 },
                    { 24, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7801), 15.86m, 3 },
                    { 25, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7803), 24.07m, 5 },
                    { 26, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7804), 40.92m, 1 },
                    { 27, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7806), 24.8m, 11 },
                    { 28, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7808), 16.64m, 11 },
                    { 29, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7809), 54.41m, 9 },
                    { 30, new DateTime(2024, 1, 3, 8, 39, 44, 934, DateTimeKind.Local).AddTicks(7818), 46.47m, 4 }
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
                    { 1, 4, "Great book but it gave me an existential crisis bigger than I had before", 5, 34 },
                    { 2, 20, "Couldn't put it down, finished it in one sitting!", 8, 17 },
                    { 3, 34, "The plot twists in this book are mind-blowing", 7, 48 },
                    { 4, 20, "A classic that everyone should read", 9, 79 },
                    { 5, 16, "The characters are so well-developed, felt like they were real people", 3, 26 },
                    { 6, 11, "This book made me laugh and cry, a roller coaster of emotions", 5, 10 },
                    { 7, 30, "The writing style is beautiful, every sentence is a work of art", 3, 63 },
                    { 8, 6, "I couldn't guess the ending, kept me guessing until the last page", 10, 38 },
                    { 9, 27, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 9, 74 },
                    { 10, 21, "The themes explored in this book are thought-provoking", 7, 67 },
                    { 11, 7, "The pacing is perfect, kept me engaged from start to finish", 5, 57 },
                    { 12, 21, "This book challenged my perspective on life", 5, 49 },
                    { 13, 20, "The world-building is exceptional, I felt like I was there", 13, 85 },
                    { 14, 25, "A must-read for book lovers", 5, 50 },
                    { 15, 32, "The author's storytelling is captivating", 8, 97 },
                    { 16, 17, "This book is a page-turner, couldn't stop reading", 14, 69 },
                    { 17, 11, "The dialogue between characters is witty and realistic", 13, 65 },
                    { 18, 4, "I've recommended this book to all my friends", 11, 81 },
                    { 19, 33, "It left me with a book hangover, couldn't stop thinking about it", 6, 85 },
                    { 20, 24, "", 8, 54 },
                    { 21, 25, "Couldn't get into the story, found it boring from the start", 7, 25 },
                    { 22, 22, "The characters felt one-dimensional and uninteresting", 11, 29 },
                    { 23, 6, "The plot was predictable, I expected more twists", 11, 20 },
                    { 24, 10, "I didn't connect with the protagonist, lacked depth", 2, 23 },
                    { 25, 15, "The writing style was confusing and hard to follow", 8, 49 },
                    { 26, 24, "This book didn't live up to the hype, very disappointing", 13, 27 },
                    { 27, 33, "The ending felt rushed and unresolved", 4, 50 },
                    { 28, 29, "Too much exposition, not enough action", 8, 95 },
                    { 29, 16, "I found the dialogue unrealistic and forced", 11, 47 },
                    { 30, 18, "The author tried too hard to be profound, came off as pretentious", 1, 71 },
                    { 31, 27, "The pacing was off, some parts dragged on while others felt rushed", 12, 34 },
                    { 32, 4, "The world-building was weak and inconsistent", 4, 89 },
                    { 33, 15, "I couldn't sympathize with any of the characters", 2, 64 },
                    { 34, 17, "The themes explored were cliché and overdone", 14, 88 },
                    { 35, 29, "The book didn't live up to the reviews, a letdown", 6, 76 },
                    { 36, 6, "The grammar and editing were poor, distracting from the story", 12, 15 },
                    { 37, 3, "The book felt like a rip-off of [another popular book]", 10, 42 },
                    { 38, 6, "The author relied too heavily on stereotypes", 2, 87 },
                    { 39, 2, "I regret spending time on this book, wish I chose something else", 1, 29 },
                    { 40, 20, "The climax was anticlimactic, left me unsatisfied", 7, 96 },
                    { 41, 14, "Great book but it gave me an existential crisis bigger than I had before", 8, 37 },
                    { 42, 18, "Couldn't put it down, finished it in one sitting!", 10, 64 },
                    { 43, 16, "The plot twists in this book are mind-blowing", 1, 86 },
                    { 44, 3, "A classic that everyone should read", 2, 43 },
                    { 45, 30, "The characters are so well-developed, felt like they were real people", 1, 85 },
                    { 46, 9, "This book made me laugh and cry, a roller coaster of emotions", 11, 86 },
                    { 47, 11, "The writing style is beautiful, every sentence is a work of art", 13, 57 },
                    { 48, 6, "I couldn't guess the ending, kept me guessing until the last page", 5, 56 },
                    { 49, 3, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 14, 60 },
                    { 50, 21, "The themes explored in this book are thought-provoking", 6, 79 }
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
