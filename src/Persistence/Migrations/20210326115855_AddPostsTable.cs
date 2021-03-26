using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
  /// <summary>
  /// Class AddPostsTable
  /// </summary>
  public partial class AddPostsTable : Migration
  {
    /// <summary>
    /// Method performs migration
    /// </summary>
    /// <param name="migrationBuilder">migrationBuilder</param>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Posts",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Title = table.Column<string>(type: "text", nullable: true),
            Body = table.Column<string>(type: "text", nullable: true),
            Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Posts", x => x.Id);
          });
    }

    /// <summary>
    /// Method rollovers migrationBuilder
    /// </summary>
    /// <param name="migrationBuilder">migrationBuilder</param>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Posts");
    }
  }
}
