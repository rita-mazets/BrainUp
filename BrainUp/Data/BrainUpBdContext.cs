using System;
using System.Collections.Generic;
using BrainUp.Models;
using Microsoft.EntityFrameworkCore;
using Task = BrainUp.Models.Task;

namespace BrainUp.Data;

public partial class BrainUpBdContext : DbContext
{
    public BrainUpBdContext()
    {
    }

    public BrainUpBdContext(DbContextOptions<BrainUpBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Cource> Cources { get; set; }

    public virtual DbSet<CreditCard> CreditCards { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FullCource> FullCources { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuSubmenuContentName> MenuSubmenuContentNames { get; set; }

    public virtual DbSet<MenuSubmenuTaskName> MenuSubmenuTaskNames { get; set; }

    public virtual DbSet<Option> Options { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<Rank> Ranks { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubMenu> SubMenus { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProgress> UserProgresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BrainUpBD;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category", tb =>
                {
                    tb.HasTrigger("LogCategoryDelete");
                    tb.HasTrigger("LogCategoryInsert");
                    tb.HasTrigger("LogCategoryUpdate");
                });

            entity.HasIndex(e => e.Name, "CategoryNameIndex").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_CategoryName").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.ToTable("Content");

            entity.HasIndex(e => e.SubMenuId, "IX_Content_SubMenuId");

            entity.HasOne(d => d.SubMenu).WithMany(p => p.Contents)
                .HasForeignKey(d => d.SubMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Content_SubMenuId");
        });

        modelBuilder.Entity<Cource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Couce");

            entity.ToTable("Cource", tb =>
                {
                    tb.HasTrigger("LogCourceDelete");
                    tb.HasTrigger("LogCourceInsert");
                    tb.HasTrigger("LogCourceUpdate");
                });

            entity.HasIndex(e => e.CategoryId, "IX_Cource_CategoryId");

            entity.HasIndex(e => e.LanguageId, "IX_Cource_LanguageId");

            entity.HasIndex(e => e.LevelId, "IX_Cource_LevelId");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShotDiscription).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Cources)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Language).WithMany(p => p.Cources)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Level).WithMany(p => p.Cources)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Price).WithMany(p => p.Cources)
                .HasForeignKey(d => d.PriceId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Cources)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.Students).WithMany(p => p.CourcesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "CourceStudent",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_CourceStudent_Cource_StudentId"),
                    l => l.HasOne<Cource>().WithMany().HasForeignKey("CourceId"),
                    j =>
                    {
                        j.HasKey("CourceId", "StudentId");
                        j.ToTable("CourceStudent", tb =>
                            {
                                tb.HasTrigger("LogCourceStudentDelete");
                                tb.HasTrigger("LogCourceStudentInsert");
                                tb.HasTrigger("LogCourceStudentUpdate");
                            });
                        j.HasIndex(new[] { "CourceId" }, "IX_CourceStudent_CourceId");
                        j.HasIndex(new[] { "StudentId" }, "IX_CourceStudent_StudentId");
                    });
        });

        modelBuilder.Entity<CreditCard>(entity =>
        {
            entity.ToTable("CreditCard", tb =>
                {
                    tb.HasTrigger("LogCreditCardDelete");
                    tb.HasTrigger("LogCreditCardInsert");
                    tb.HasTrigger("LogCreditCardUpdate");
                });

            entity.HasIndex(e => e.CurrencySymbol, "IX_CreditCard_CurrencySymbol");

            entity.HasIndex(e => e.UserId, "IX_CreditCard_UserId");

            entity.HasIndex(e => e.Number, "UQ_Number").IsUnique();

            entity.Property(e => e.Balance).HasDefaultValueSql("((0))");
            entity.Property(e => e.CurrencySymbol).HasMaxLength(3);
            entity.Property(e => e.Cvvhash).HasColumnName("CVVHash");
            entity.Property(e => e.Number).HasMaxLength(19);
            entity.Property(e => e.OwnerName).HasMaxLength(50);

            entity.HasOne(d => d.CurrencySymbolNavigation).WithMany(p => p.CreditCards).HasForeignKey(d => d.CurrencySymbol);

            entity.HasOne(d => d.User).WithMany(p => p.CreditCards).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Symbol);

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("LogCurrenciesDelete");
                    tb.HasTrigger("LogCurrenciesInsert");
                    tb.HasTrigger("LogCurrenciesUpdate");
                });

            entity.HasIndex(e => e.Symbol, "Symbol_Index").IsUnique();

            entity.Property(e => e.Symbol).HasMaxLength(3);
            entity.Property(e => e.Usdequivalent).HasColumnName("USDEquivalent");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.ToTable("Feedback", tb =>
                {
                    tb.HasTrigger("LogFeedbackDelete");
                    tb.HasTrigger("LogFeedbackInsert");
                    tb.HasTrigger("LogFeedbackUpdate");
                });

            entity.HasIndex(e => e.CourceId, "IX_Feedback_CourceId");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cource).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CourceId)
                .HasConstraintName("FK_Feedback_Cource_CourceID");
        });

        modelBuilder.Entity<FullCource>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FullCource");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("categoryName");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsPaid).HasColumnName("isPaid");
            entity.Property(e => e.LanguageName)
                .HasMaxLength(50)
                .HasColumnName("languageName");
            entity.Property(e => e.LevelName)
                .HasMaxLength(50)
                .HasColumnName("levelName");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.ShortDiscription).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Symbol)
                .HasMaxLength(3)
                .HasColumnName("symbol");
            entity.Property(e => e.TeacherDiscription).HasColumnName("teacherDiscription");
            entity.Property(e => e.TeacherFirstName).HasColumnName("teacherFirstName");
            entity.Property(e => e.TeacherImage).HasColumnName("teacherImage");
            entity.Property(e => e.TeacherLastName).HasColumnName("teacherLastName");
            entity.Property(e => e.Usdequivalent).HasColumnName("USDEquivalent");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Language", tb =>
                {
                    tb.HasTrigger("LogLanguageDelete");
                    tb.HasTrigger("LogLanguageInsert");
                    tb.HasTrigger("LogLanguageUpdate");
                });

            entity.HasIndex(e => e.Name, "LanguageNameIndex").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_LanguageName").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Symbol).HasMaxLength(10);
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.ToTable("Level", tb =>
                {
                    tb.HasTrigger("LogLevelDelete");
                    tb.HasTrigger("LogLevelInsert");
                    tb.HasTrigger("LogLevelUpdate");
                });

            entity.HasIndex(e => e.Name, "LevelNameIndex").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_LevelName").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.HasIndex(e => e.CourceId, "IX_Menu_CourceId");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Cource).WithMany(p => p.Menus)
                .HasForeignKey(d => d.CourceId)
                .HasConstraintName("FK_Menu_Cource_CourceID");
        });

        modelBuilder.Entity<MenuSubmenuContentName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MenuSubmenuContentName");

            entity.Property(e => e.ContentId).HasColumnName("contentId");
            entity.Property(e => e.CourceId).HasColumnName("courceId");
            entity.Property(e => e.MenuName).HasMaxLength(50);
            entity.Property(e => e.SubmenuName).HasMaxLength(50);
        });

        modelBuilder.Entity<MenuSubmenuTaskName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MenuSubmenuTaskName");

            entity.Property(e => e.CourceId).HasColumnName("courceId");
            entity.Property(e => e.MenuName).HasMaxLength(50);
            entity.Property(e => e.SubmenuName).HasMaxLength(50);
            entity.Property(e => e.TaskId).HasColumnName("taskId");
        });

        modelBuilder.Entity<Option>(entity =>
        {
            entity.ToTable("Option", tb =>
                {
                    tb.HasTrigger("LogOptionDelete");
                    tb.HasTrigger("LogOptionInsert");
                    tb.HasTrigger("LogOptionUpdate");
                    tb.HasTrigger("Tasks_SetConfirm");
                });

            entity.HasIndex(e => e.TaskId, "IX_Option_TaskId");

            entity.Property(e => e.Option1).HasColumnName("Option");

            entity.HasOne(d => d.Task).WithMany(p => p.Options).HasForeignKey(d => d.TaskId);
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.ToTable("Price", tb =>
                {
                    tb.HasTrigger("LogPriceDelete");
                    tb.HasTrigger("LogPriceInsert");
                    tb.HasTrigger("LogPriceUpdate");
                });

            entity.Property(e => e.CurrencySymbol).HasMaxLength(3);
            entity.Property(e => e.Price1)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money")
                .HasColumnName("Price");

            entity.HasOne(d => d.CurrencySymbolNavigation).WithMany(p => p.Prices)
                .HasForeignKey(d => d.CurrencySymbol)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Rank>(entity =>
        {
            entity.ToTable("Rank", tb =>
                {
                    tb.HasTrigger("LogRankDelete");
                    tb.HasTrigger("LogRankInsert");
                    tb.HasTrigger("LogRankUpdate");
                });

            entity.HasIndex(e => e.CourceId, "IX_Rank_CourceId");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Cource).WithMany(p => p.Ranks)
                .HasForeignKey(d => d.CourceId)
                .HasConstraintName("FK_Rank_Cource_CourceID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role", tb =>
                {
                    tb.HasTrigger("LogRoleDelete");
                    tb.HasTrigger("LogRoleInsert");
                    tb.HasTrigger("LogRoleUpdate");
                });

            entity.HasIndex(e => e.Name, "RoleNameIndex").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<SubMenu>(entity =>
        {
            entity.ToTable("SubMenu");

            entity.HasIndex(e => e.MenuId, "IX_SubMenu_MenuId");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Menu).WithMany(p => p.SubMenus).HasForeignKey(d => d.MenuId);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task", tb =>
                {
                    tb.HasTrigger("LogTaskDelete");
                    tb.HasTrigger("LogTaskInsert");
                    tb.HasTrigger("LogTaskUpdate");
                });

            entity.HasIndex(e => e.SubMenuId, "IX_Task_SubMenuId");

            entity.HasOne(d => d.SubMenu).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.SubMenuId)
                .HasConstraintName("FK_Task_SubMenuId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", tb =>
                {
                    tb.HasTrigger("LogUserDelete");
                    tb.HasTrigger("LogUserInsert");
                    tb.HasTrigger("LogUserUpdate");
                });

            entity.HasIndex(e => e.Email, "EmailIndex");

            entity.HasIndex(e => e.RoleId, "IX_User_RoleId");

            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0764221A82");

            entity.ToTable("UserProgress");

            entity.HasIndex(e => e.CourceId, "IX_UserProgress_CourceId");

            entity.HasIndex(e => e.UserId, "IX_UserProgress_UserId");

            entity.Property(e => e.Point).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Cource).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.CourceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProgress_Cource_CourceID");

            entity.HasOne(d => d.Task).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserProgress_Task_TaskID");

            entity.HasOne(d => d.User).WithMany(p => p.UserProgresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
