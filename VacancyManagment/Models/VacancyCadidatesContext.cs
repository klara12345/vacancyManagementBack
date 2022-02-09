using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VacancyManagment.Models
{
    public partial class VacancyCadidatesContext : DbContext
    {
        public VacancyCadidatesContext()
        {
        }

        public VacancyCadidatesContext(DbContextOptions<VacancyCadidatesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlackList> BlackLists { get; set; } = null!;
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<Interview> Interviews { get; set; } = null!;
        public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;
        public virtual DbSet<VacancyAction> VacancyActions { get; set; } = null!;
        public virtual DbSet<VacancyCandidate> VacancyCandidates { get; set; } = null!;
        public virtual DbSet<VacancyRoleAction> VacancyRoleActions { get; set; } = null!;
        public virtual DbSet<VacancyUser> VacancyUsers { get; set; } = null!;
        public virtual DbSet<VacancyUserRole> VacancyUserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=rbal-one;Database=VacancyCadidates;user Id=user247;password=user247;Trusted_Connection=false; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlackList>(entity =>
            {
                entity.HasKey(e => e.IdBlackListCandidate);

                entity.ToTable("BlackList");

                entity.Property(e => e.DatetimeCreated).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.BlackLists)
                    .HasForeignKey(d => d.IdCandidate)
                    .HasConstraintName("FK_BlackList_Candidate");

                entity.HasOne(d => d.IdUserCreatedNavigation)
                    .WithMany(p => p.BlackLists)
                    .HasForeignKey(d => d.IdUserCreated)
                    .HasConstraintName("FK_BlackList_VacancyUser");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.IdCandidate);

                entity.ToTable("Candidate");

                entity.Property(e => e.CandidateCv).HasColumnName("CandidateCV");

                entity.Property(e => e.CandidateEmail)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.CandidateMobile)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.CandidateName)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.CandidateSsn)
                    .HasMaxLength(20)
                    .HasColumnName("CandidateSSN")
                    .IsFixedLength();

                entity.Property(e => e.CandidateSurname)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateSaved).HasColumnType("datetime");

                entity.Property(e => e.IdUserCreated).HasColumnName("idUserCreated");

                entity.HasOne(d => d.IdUserCreatedNavigation)
                    .WithMany(p => p.CandidateIdUserCreatedNavigations)
                    .HasForeignKey(d => d.IdUserCreated)
                    .HasConstraintName("FK_Candidate_VacancyUser");

                entity.HasOne(d => d.IdUserLastSavedNavigation)
                    .WithMany(p => p.CandidateIdUserLastSavedNavigations)
                    .HasForeignKey(d => d.IdUserLastSaved)
                    .HasConstraintName("FK_Candidate_VacancyUser1");
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(e => e.IdInterview);

                entity.ToTable("Interview");

                entity.Property(e => e.CandidateComments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DatetimeCreated).HasColumnType("datetime");

                entity.Property(e => e.IdTlparticipant).HasColumnName("IdTLParticipant");

                entity.Property(e => e.InterviewComments)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InterviewDateTime).HasColumnType("datetime");

                entity.Property(e => e.OfferedSalary).HasColumnType("money");

                entity.Property(e => e.RequestedSalary).HasColumnType("money");

                entity.Property(e => e.ResultFromHr)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ResultFromHR");

                entity.Property(e => e.ResultFromTl)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ResultFromTL");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.IdCandidate)
                    .HasConstraintName("FK_Interview_Vacancy");
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.HasKey(e => e.IdVacancy);

                entity.ToTable("Vacancy");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateSaved).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Vacancy1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Vacancy");

                entity.HasOne(d => d.IdTeamLeadNavigation)
                    .WithMany(p => p.VacancyIdTeamLeadNavigations)
                    .HasForeignKey(d => d.IdTeamLead)
                    .HasConstraintName("FK_Vacancy_VacancyUser");

                entity.HasOne(d => d.IdUserCreatedNavigation)
                    .WithMany(p => p.VacancyIdUserCreatedNavigations)
                    .HasForeignKey(d => d.IdUserCreated)
                    .HasConstraintName("FK_Vacancy_VacancyUser1");

                entity.HasOne(d => d.IdUserSavedNavigation)
                    .WithMany(p => p.VacancyIdUserSavedNavigations)
                    .HasForeignKey(d => d.IdUserSaved)
                    .HasConstraintName("FK_Vacancy_VacancyUser2");
            });

            modelBuilder.Entity<VacancyAction>(entity =>
            {
                entity.HasKey(e => e.IdAction)
                    .HasName("PK_UC03_Modules");

                entity.ToTable("VacancyAction");

                entity.Property(e => e.IdAction).HasColumnName("ID_Action");

                entity.Property(e => e.CanLog).HasColumnName("canLog");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LogMsg).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VacancyCandidate>(entity =>
            {
                entity.HasKey(e => e.IdVacancyCanddate);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DatetimeValidated).HasColumnType("datetime");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.VacancyCandidates)
                    .HasForeignKey(d => d.IdCandidate)
                    .HasConstraintName("FK_VacancyCandidates_Candidate");

                entity.HasOne(d => d.IdUserReferedNavigation)
                    .WithMany(p => p.VacancyCandidateIdUserReferedNavigations)
                    .HasForeignKey(d => d.IdUserRefered)
                    .HasConstraintName("FK_VacancyCandidates_VacancyUser");

                entity.HasOne(d => d.IdUserValidateNavigation)
                    .WithMany(p => p.VacancyCandidateIdUserValidateNavigations)
                    .HasForeignKey(d => d.IdUserValidate)
                    .HasConstraintName("FK_VacancyCandidates_VacancyUser1");

                entity.HasOne(d => d.IdVacancyNavigation)
                    .WithMany(p => p.VacancyCandidates)
                    .HasForeignKey(d => d.IdVacancy)
                    .HasConstraintName("FK_VacancyCandidates_Vacancy");
            });

            modelBuilder.Entity<VacancyRoleAction>(entity =>
            {
                entity.HasKey(e => e.IdRoleAction)
                    .HasName("PK_U03_GroupModules");

                entity.ToTable("VacancyRoleAction");

                entity.Property(e => e.IdRoleAction).HasColumnName("ID_RoleAction");

                entity.Property(e => e.IdAction)
                    .HasColumnName("ID_Action")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.HasOne(d => d.IdActionNavigation)
                    .WithMany(p => p.VacancyRoleActions)
                    .HasForeignKey(d => d.IdAction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_U02_RoleAction_UC02_Action");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.VacancyRoleActions)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_U02_RoleAction_UC01_Role");
            });

            modelBuilder.Entity<VacancyUser>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_U01_Users");

                entity.ToTable("VacancyUser");

                entity.HasIndex(e => e.User, "IX_U01_User")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Comment)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(128);

                entity.Property(e => e.Salt)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.User).HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.VacancyUsers)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_U01_User_UC01_Role");
            });

            modelBuilder.Entity<VacancyUserRole>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK_UC01_Roles");

                entity.ToTable("VacancyUserRole");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
