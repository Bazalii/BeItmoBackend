using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeItmoBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attended_university_events",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attended_university_events", x => new { x.user_id, x.event_id });
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "happiness_checkpoints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    note = table.Column<string>(type: "text", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_happiness_checkpoints", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "interests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_statistics",
                columns: table => new
                {
                    type_value_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    tap_counter = table.Column<int>(type: "integer", nullable: false),
                    prize_counter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_statistics", x => new { x.type_value_id, x.user_id });
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    friendliness_score = table.Column<int>(type: "integer", nullable: false),
                    health_score = table.Column<int>(type: "integer", nullable: false),
                    fit_score = table.Column<int>(type: "integer", nullable: false),
                    eco_score = table.Column<int>(type: "integer", nullable: false),
                    open_score = table.Column<int>(type: "integer", nullable: false),
                    pro_score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "university_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    creator_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    place = table.Column<string>(type: "text", nullable: false),
                    contacts = table.Column<string>(type: "text", nullable: false),
                    picture_link = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_university_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_university_events_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "category_db_model_user_db_model",
                columns: table => new
                {
                    categories_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_db_model_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_db_model_user_db_model", x => new { x.categories_id, x.user_db_model_id });
                    table.ForeignKey(
                        name: "fk_category_db_model_user_db_model_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_db_model_user_db_model_users_user_db_model_id",
                        column: x => x.user_db_model_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interest_db_model_user_db_model",
                columns: table => new
                {
                    interests_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_db_model_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interest_db_model_user_db_model", x => new { x.interests_id, x.user_db_model_id });
                    table.ForeignKey(
                        name: "fk_interest_db_model_user_db_model_interests_interests_id",
                        column: x => x.interests_id,
                        principalTable: "interests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_interest_db_model_user_db_model_users_user_db_model_id",
                        column: x => x.user_db_model_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events_interests",
                columns: table => new
                {
                    interests_id = table.Column<Guid>(type: "uuid", nullable: false),
                    university_event_db_model_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events_interests", x => new { x.interests_id, x.university_event_db_model_id });
                    table.ForeignKey(
                        name: "fk_events_interests_interests_interests_id",
                        column: x => x.interests_id,
                        principalTable: "interests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_events_interests_university_events_university_event_db_mode",
                        column: x => x.university_event_db_model_id,
                        principalTable: "university_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_category_db_model_user_db_model_user_db_model_id",
                table: "category_db_model_user_db_model",
                column: "user_db_model_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_interests_university_event_db_model_id",
                table: "events_interests",
                column: "university_event_db_model_id");

            migrationBuilder.CreateIndex(
                name: "ix_interest_db_model_user_db_model_user_db_model_id",
                table: "interest_db_model_user_db_model",
                column: "user_db_model_id");

            migrationBuilder.CreateIndex(
                name: "ix_university_events_category_id",
                table: "university_events",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attended_university_events");

            migrationBuilder.DropTable(
                name: "category_db_model_user_db_model");

            migrationBuilder.DropTable(
                name: "events_interests");

            migrationBuilder.DropTable(
                name: "happiness_checkpoints");

            migrationBuilder.DropTable(
                name: "interest_db_model_user_db_model");

            migrationBuilder.DropTable(
                name: "user_statistics");

            migrationBuilder.DropTable(
                name: "university_events");

            migrationBuilder.DropTable(
                name: "interests");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
