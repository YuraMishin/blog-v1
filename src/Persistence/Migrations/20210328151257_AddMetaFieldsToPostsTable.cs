using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
  /// <summary>
  /// Class AddMetaFieldsToPostsTable
  /// </summary>
  public partial class AddMetaFieldsToPostsTable : Migration
  {
    /// <summary>
    /// Method performs the migration
    /// </summary>
    /// <param name="migrationBuilder">migrationBuilder</param>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "Category",
          table: "Posts",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Description",
          table: "Posts",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Tags",
          table: "Posts",
          type: "text",
          nullable: true);
    }

    /// <summary>
    /// Method rollovers the migration
    /// </summary>
    /// <param name="migrationBuilder">migrationBuilder</param>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Category",
          table: "Posts");

      migrationBuilder.DropColumn(
          name: "Description",
          table: "Posts");

      migrationBuilder.DropColumn(
          name: "Tags",
          table: "Posts");
    }
  }
}
