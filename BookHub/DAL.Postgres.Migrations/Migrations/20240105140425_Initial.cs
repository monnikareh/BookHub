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
                    { 1, 0, "24bfe389-1692-48bb-885e-141785333ec9", "Roman@gmail.com", true, false, null, "Roman Mario", "ROMAN@GMAIL.COM", "ROMAN", "AQAAAAIAAYagAAAAEImrHSyVtb6JPZpy05KjeegJJEd2bMkSxxtWiBB7ZatbF6VKvHzo63fpb5eOUMtuEw==", null, false, "1fa4a21e-a758-45b8-aa98-a26a16933c3c", false, "roman" },
                    { 2, 0, "bcf605f7-cf08-47e3-a553-93e4cfe549e1", "Beth@gmail.com", true, false, null, "Beth Story", "BETH@GMAIL.COM", "BETH", "AQAAAAIAAYagAAAAEHOw+tjVsQEkbfH1ABg0+s6oUtKeUyjjxj3D46uSK4H1VhZl9aTVqQqMKbkzct/NuQ==", null, false, "5544c974-374e-4efe-bdd1-a6e73a6c688b", false, "beth" },
                    { 3, 0, "4f415759-2aa2-4fd2-9956-da4b88ddfe0b", "Monika@gmail.com", true, false, null, "Monika Reha", "MONIKA@GMAIL.COM", "MONIKA", "AQAAAAIAAYagAAAAEP1bN0f3HJ+49mZbqr+aPkpJzqH1PmKYHCQQc3DniVJ1/hzIB+NlgY+vRaD9v2nbCA==", null, false, "dfe4256e-769a-424f-b669-1394d0a22750", false, "monika" },
                    { 4, 0, "fb45c3eb-8672-456e-9258-d687a677ebc1", "John@gmail.com", true, false, null, "John Smith", "JOHN@GMAIL.COM", "JOHN", "AQAAAAIAAYagAAAAEOtEojKMr1xgqGtgSXiZMcdk0csJxCMRTfZhN7/1CXz1H5C4VlVUBd1Tq17JX+4i0A==", null, false, "b5c998c8-91b2-49ca-84fc-9530ead2541e", false, "john" },
                    { 5, 0, "16b117cf-5fbd-42c3-88c3-40543d0b11de", "James@gmail.com", true, false, null, "James Bond", "JAMES@GMAIL.COM", "JAMES", "AQAAAAIAAYagAAAAEG47HjRfPrVUzQzvGEzEs+7Kw6akalc84TVUW/FYLFtiRYUArQ57EcsM6+eeEoW/Iw==", null, false, "ccee497a-7878-4c18-a596-5f746154cc61", false, "james" },
                    { 6, 0, "fb15ff10-4c00-4b36-87d2-eafbdf1ea2ba", "Filip@gmail.com", true, false, null, "Filip Strong", "FILIP@GMAIL.COM", "FILIP", "AQAAAAIAAYagAAAAEA1+SIKGpE19KU3SCxmbYg5HLlnorPiw3GNSZVW1br/04GFtcCCJNzvr4ne7T6UhvA==", null, false, "7e1375f4-80f0-4593-a6a1-0e8891daae03", false, "filip" },
                    { 7, 0, "c15f7614-50f2-4833-8485-d0a6f0e97e40", "Random@gmail.com", true, false, null, "Random Guy", "RANDOM@GMAIL.COM", "RANDOM", "AQAAAAIAAYagAAAAEBGTI1Uf/mbXPb/tdTfRIX3rGBeyoKV0YJHiv3YCEHNCa3cIcwbyKLO+qsw47VIzvQ==", null, false, "d8a21ca8-0b97-4dfb-b080-1e595c05bf20", false, "random" },
                    { 8, 0, "5bc492bd-76a0-4948-97be-06410c79ba5f", "Jack@gmail.com", true, false, null, "Jack Black", "JACK@GMAIL.COM", "JACK", "AQAAAAIAAYagAAAAEKn/mb12syPXFYRq/yX1p/gburqG/8677PymtAQpEar0JmyYv2mTYsgfpnLtZbcSwA==", null, false, "4501e8e0-4825-4476-9460-af6fd3eee62d", false, "jack" },
                    { 9, 0, "d9daa098-f77e-4076-8b8e-c2e0a85d0041", "Tom@gmail.com", true, false, null, "Tom Smart", "TOM@GMAIL.COM", "TOM", "AQAAAAIAAYagAAAAEMQbhNfeetT/4kAvGWJHGPj3IZXEgxiyzCU0prU77WIEF71OT0vtwKkowpCvzVinFQ==", null, false, "a862d170-83c7-4201-9b16-535db049c70b", false, "tom" },
                    { 10, 0, "6fde1fab-d134-4bcd-b97d-adfd9de00529", "Ali@gmail.com", true, false, null, "Ali Willy", "ALI@GMAIL.COM", "ALI", "AQAAAAIAAYagAAAAEKXdOmYFV743M8+j7C6LN06bTuegIETk+/1mvxUYW0icOSW5Wmx50KfbTugB9VWPSg==", null, false, "39bf7781-c3f1-42bf-8e30-2b8fe0d9a0e8", false, "ali" },
                    { 11, 0, "8eadfc09-a3bf-4e19-9ad9-8f6f0665497c", "Rubber@gmail.com", true, false, null, "Rubber Duck", "RUBBER@GMAIL.COM", "RUBBER", "AQAAAAIAAYagAAAAEMT7Do4eel8/7MAEMJujWbc2GwhyKl8fTNkLKFSpWteauEJ6UlbY7QE5eUJjwkptow==", null, false, "21caf7bf-7b97-4313-a6d3-5a7295eb36ed", false, "rubber" },
                    { 12, 0, "a14eb183-dd54-40e6-bbf6-757caee98572", "Olaf@gmail.com", true, false, null, "Olaf Snow", "OLAF@GMAIL.COM", "OLAF", "AQAAAAIAAYagAAAAEILbecrEM/mByyzWfEd1Ne3P8ySvts0QYV1Zx2TiOjOmFwOKXqUXt3PDZr3X34J97w==", null, false, "0b185c3a-0226-4e92-9b20-ebb79cbe0b49", false, "olaf" },
                    { 13, 0, "3b338295-6723-475a-b6f9-a5f6a2375ba0", "Good@gmail.com", true, false, null, "Good Programmer", "GOOD@GMAIL.COM", "GOOD", "AQAAAAIAAYagAAAAEEzvSrI5KuUsLEbfzTpm7VaB1ZyhQazm5xSQoxDAfiWVFe6MHQ93sfilWd+Iv13ZoQ==", null, false, "0d68a75a-4cf8-4911-b86d-dcefd25e0384", false, "good" },
                    { 14, 0, "c14c0739-ec0b-46dc-9fd7-284fc83a9e8b", "Tim@gmail.com", true, false, null, "Tim King", "TIM@GMAIL.COM", "TIM", "AQAAAAIAAYagAAAAEEYzrjqou7HtAqYgAg7CY8wDLd++BdabasNOnzJlzfSDy1dGmwC9CCW5QOZs0gaiSA==", null, false, "628f1456-5a5b-4586-856a-6db5633a43ac", false, "tim" },
                    { 15, 0, "5b45d7d5-53f3-491a-9098-40114a99e189", "Adam@gmail.com", true, false, null, "Adam Queen", "ADAM@GMAIL.COM", "ADAM", "AQAAAAIAAYagAAAAEGTPIkunI39aZ2p+tflHnctg0mow6A31+MBZOu6bAByNKeNiYv+qmv3apAwyvV7GtQ==", null, false, "b43fc8f4-0945-4bc7-a3d4-a0ba6dacbe7a", false, "adam" }
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
                    { 1, "To Kill a Mockingbird", 34, 23.01m, 7, 6, 21 },
                    { 2, "1984", 56, 7.71m, 13, 4, 33 },
                    { 3, "James Bond", 33, 10.59m, 12, 2, 20 },
                    { 4, "The Great Gatsby", 79, 15.45m, 15, 3, 33 },
                    { 5, "One Hundred Years of Solitude", 40, 12.33m, 2, 13, 14 },
                    { 6, "The Catcher in the Rye", 46, 8.83m, 10, 2, 21 },
                    { 7, "Brave New World", 86, 23.92m, 1, 4, 8 },
                    { 8, "The Hobbit", 85, 24.61m, 11, 11, 10 },
                    { 9, "Love and Basketball", 94, 9.35m, 15, 13, 42 },
                    { 10, "Pride and Prejudice", 71, 21.08m, 3, 1, 29 },
                    { 11, "The Lord of the Rings: The Fellowship of the Ring", 54, 16.32m, 13, 11, 19 },
                    { 12, "The Chronicles of Narnia: The Lion, the Witch and the Wardrobe", 81, 6.8m, 8, 6, 30 },
                    { 13, "Harry Potter and the Philosopher's Stone", 68, 19.66m, 18, 13, 34 },
                    { 14, "The Hunger Games", 65, 7.56m, 4, 7, 1 },
                    { 15, "The Da Vinci Code", 40, 14.02m, 1, 8, 13 },
                    { 16, "A Game of Thrones", 41, 12.44m, 17, 10, 35 },
                    { 17, "The Shining", 70, 19.17m, 3, 6, 34 },
                    { 18, "The Hitchhiker's Guide to the Galaxy", 45, 10.46m, 17, 9, 18 },
                    { 19, "The Alchemist", 33, 6.01m, 7, 9, 11 },
                    { 20, "War and Peace", 91, 10.15m, 12, 13, 25 },
                    { 21, "Crime and Punishment", 51, 7.42m, 4, 13, 7 },
                    { 22, "The Catch-22", 86, 18.37m, 10, 4, 19 },
                    { 23, "The Grapes of Wrath", 78, 18.18m, 2, 14, 23 },
                    { 24, "Fahrenheit 451", 68, 15.56m, 14, 13, 8 },
                    { 25, "Lord of the Flies", 98, 17.68m, 8, 5, 19 },
                    { 26, "Moby-Dick", 72, 13.65m, 15, 6, 5 },
                    { 27, "Frankenstein", 62, 8.67m, 19, 8, 6 },
                    { 28, "Alice's Adventures in Wonderland", 67, 13.15m, 4, 8, 7 },
                    { 29, "Dracula", 85, 12.99m, 9, 10, 46 },
                    { 30, "The Odyssey", 86, 19.18m, 16, 12, 36 },
                    { 31, "Romeo and Juliet", 67, 24.76m, 7, 12, 8 },
                    { 32, "Hamlet", 30, 17.54m, 16, 11, 46 },
                    { 33, "Macbeth", 31, 13.15m, 1, 3, 16 },
                    { 34, "Othello", 31, 22.04m, 5, 14, 13 },
                    { 35, "The Divine Comedy", 39, 20.48m, 12, 14, 10 },
                    { 36, "Don Quixote", 71, 7.01m, 17, 14, 15 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "PaymentStatus", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(6979), 2, 42.33m, 7 },
                    { 2, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(7079), 2, 43.29m, 6 },
                    { 3, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(7084), 0, 12.85m, 12 },
                    { 4, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(7087), 1, 14.85m, 4 },
                    { 5, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8255), 1, 42.03m, 9 },
                    { 6, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8262), 0, 7.62m, 9 },
                    { 7, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8266), 4, 38.8m, 11 },
                    { 8, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8268), 4, 52.12m, 8 },
                    { 9, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8271), 2, 38m, 7 },
                    { 10, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8274), 4, 53.98m, 10 },
                    { 11, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8276), 1, 18.11m, 12 },
                    { 12, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8279), 2, 5.98m, 11 },
                    { 13, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8281), 1, 27.3m, 11 },
                    { 14, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8284), 3, 24.18m, 10 },
                    { 15, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8286), 2, 20.57m, 6 },
                    { 16, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8308), 0, 29.45m, 7 },
                    { 17, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8311), 2, 36.22m, 12 },
                    { 18, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8314), 0, 50.3m, 14 },
                    { 19, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8317), 0, 11.23m, 3 },
                    { 20, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8320), 2, 42.19m, 10 },
                    { 21, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8322), 4, 20.29m, 9 },
                    { 22, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8324), 1, 24.86m, 13 },
                    { 23, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8326), 4, 46.98m, 9 },
                    { 24, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8368), 2, 23.16m, 10 },
                    { 25, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8370), 3, 33.81m, 7 },
                    { 26, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8373), 1, 24.01m, 6 },
                    { 27, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8375), 2, 10.94m, 12 },
                    { 28, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8377), 1, 41.31m, 7 },
                    { 29, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8379), 3, 35.12m, 6 },
                    { 30, new DateTime(2024, 1, 5, 15, 4, 24, 915, DateTimeKind.Local).AddTicks(8395), 4, 48.11m, 14 }
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
                    { 1, 9, "Great book but it gave me an existential crisis bigger than I had before", 13, 26 },
                    { 2, 24, "Couldn't put it down, finished it in one sitting!", 4, 54 },
                    { 3, 2, "The plot twists in this book are mind-blowing", 7, 71 },
                    { 4, 10, "A classic that everyone should read", 5, 82 },
                    { 5, 4, "The characters are so well-developed, felt like they were real people", 7, 65 },
                    { 6, 30, "This book made me laugh and cry, a roller coaster of emotions", 9, 16 },
                    { 7, 23, "The writing style is beautiful, every sentence is a work of art", 2, 53 },
                    { 8, 25, "I couldn't guess the ending, kept me guessing until the last page", 6, 34 },
                    { 9, 8, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 5, 70 },
                    { 10, 25, "The themes explored in this book are thought-provoking", 3, 17 },
                    { 11, 34, "The pacing is perfect, kept me engaged from start to finish", 10, 10 },
                    { 12, 33, "This book challenged my perspective on life", 9, 66 },
                    { 13, 22, "The world-building is exceptional, I felt like I was there", 1, 84 },
                    { 14, 21, "A must-read for book lovers", 3, 51 },
                    { 15, 12, "The author's storytelling is captivating", 5, 17 },
                    { 16, 33, "This book is a page-turner, couldn't stop reading", 3, 37 },
                    { 17, 16, "The dialogue between characters is witty and realistic", 9, 79 },
                    { 18, 22, "I've recommended this book to all my friends", 6, 49 },
                    { 19, 28, "It left me with a book hangover, couldn't stop thinking about it", 4, 35 },
                    { 20, 11, "", 9, 68 },
                    { 21, 15, "Couldn't get into the story, found it boring from the start", 2, 25 },
                    { 22, 1, "The characters felt one-dimensional and uninteresting", 8, 22 },
                    { 23, 1, "The plot was predictable, I expected more twists", 4, 31 },
                    { 24, 5, "I didn't connect with the protagonist, lacked depth", 11, 52 },
                    { 25, 11, "The writing style was confusing and hard to follow", 14, 26 },
                    { 26, 33, "This book didn't live up to the hype, very disappointing", 12, 37 },
                    { 27, 1, "The ending felt rushed and unresolved", 1, 11 },
                    { 28, 33, "Too much exposition, not enough action", 12, 78 },
                    { 29, 6, "I found the dialogue unrealistic and forced", 1, 57 },
                    { 30, 26, "The author tried too hard to be profound, came off as pretentious", 5, 18 },
                    { 31, 17, "The pacing was off, some parts dragged on while others felt rushed", 4, 42 },
                    { 32, 21, "The world-building was weak and inconsistent", 13, 79 },
                    { 33, 27, "I couldn't sympathize with any of the characters", 11, 34 },
                    { 34, 9, "The themes explored were cliché and overdone", 8, 75 },
                    { 35, 12, "The book didn't live up to the reviews, a letdown", 6, 21 },
                    { 36, 20, "The grammar and editing were poor, distracting from the story", 5, 82 },
                    { 37, 18, "The book felt like a rip-off of [another popular book]", 1, 42 },
                    { 38, 3, "The author relied too heavily on stereotypes", 8, 69 },
                    { 39, 6, "I regret spending time on this book, wish I chose something else", 10, 79 },
                    { 40, 18, "The climax was anticlimactic, left me unsatisfied", 9, 66 },
                    { 41, 23, "Great book but it gave me an existential crisis bigger than I had before", 10, 41 },
                    { 42, 17, "Couldn't put it down, finished it in one sitting!", 14, 47 },
                    { 43, 32, "The plot twists in this book are mind-blowing", 5, 99 },
                    { 44, 25, "A classic that everyone should read", 14, 48 },
                    { 45, 24, "The characters are so well-developed, felt like they were real people", 1, 37 },
                    { 46, 22, "This book made me laugh and cry, a roller coaster of emotions", 12, 53 },
                    { 47, 31, "The writing style is beautiful, every sentence is a work of art", 10, 90 },
                    { 48, 15, "I couldn't guess the ending, kept me guessing until the last page", 7, 78 },
                    { 49, 18, "I wish there was a sequel, I'm not ready to say goodbye to these characters", 7, 17 },
                    { 50, 31, "The themes explored in this book are thought-provoking", 14, 42 }
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
