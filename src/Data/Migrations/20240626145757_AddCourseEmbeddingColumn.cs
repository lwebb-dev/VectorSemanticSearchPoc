using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace VectorSemanticSearchPoc.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseEmbeddingColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Vector>(
                name: "Embedding",
                table: "courses",
                type: "vector(256)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Embedding",
                table: "courses");
        }
    }
}
