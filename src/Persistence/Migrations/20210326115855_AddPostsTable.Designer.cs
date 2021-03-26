// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Persistence.Migrations
{
  [DbContext(typeof(AppDbContext))]
  [Migration("20210326115855_AddPostsTable")]
  partial class AddPostsTable
  {
    /// <summary>
    /// Method executes migration
    /// </summary>
    /// <param name="modelBuilder">modelBuilder</param>
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
      modelBuilder
          .HasAnnotation("Relational:MaxIdentifierLength", 63)
          .HasAnnotation("ProductVersion", "5.0.4")
          .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

      modelBuilder.Entity("Domain.Post", b =>
          {
            b.Property<int>("Id")
                      .ValueGeneratedOnAdd()
                      .HasColumnType("integer")
                      .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            b.Property<string>("Body")
                      .HasColumnType("text");

            b.Property<DateTime>("Created")
                      .HasColumnType("timestamp without time zone");

            b.Property<string>("Title")
                      .HasColumnType("text");

            b.HasKey("Id");

            b.ToTable("Posts");
          });
#pragma warning restore 612, 618
    }
  }
}
