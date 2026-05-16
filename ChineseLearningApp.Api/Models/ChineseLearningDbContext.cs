using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChineseLearningApp.Api.Models;

public partial class ChineseLearningDbContext : DbContext
{
    public ChineseLearningDbContext()
    {
    }

    public ChineseLearningDbContext(DbContextOptions<ChineseLearningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    public virtual DbSet<WordList> WordLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MINHMINH\\MINH;Database=ChineseLearningDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC07C47F0B70");

            entity.Property(e => e.IconUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.TotalExercises).HasDefaultValue(0);
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flashcar__3214EC071B7CF90D");

            entity.Property(e => e.FrontText).HasMaxLength(255);
            entity.Property(e => e.Pinyin).HasMaxLength(255);
            entity.Property(e => e.WordType).HasMaxLength(50);

            entity.HasOne(d => d.WordList).WithMany(p => p.Flashcards)
                .HasForeignKey(d => d.WordListId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Flashcard__WordL__32E0915F");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Levels__3214EC07C2E736B7");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07EA0BF8BA");

            entity.Property(e => e.VietnameseMeaning).HasMaxLength(500);

            entity.HasOne(d => d.Category).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Categories");

            entity.HasOne(d => d.Level).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Questions_Levels");
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Words__3214EC07C9589F6D");

            entity.Property(e => e.Hanzi).HasMaxLength(50);
            entity.Property(e => e.Pinyin)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Words)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Words_Questions");
        });

        modelBuilder.Entity<WordList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WordList__3214EC07CE1311F3");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .HasDefaultValue("Ti?ng Trung");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
