using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-RDR0BO5\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True");
        }

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }

        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }

        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }

        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }

        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }

        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }

        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }

        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }

        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }

        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }

        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }

        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }

        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }

        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }

        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }

        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }

        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }

        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(ae => ae.ApplicantProfile)
                .WithMany(ap => ap.ApplicantEducations)
                .HasForeignKey(ae => ae.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(aja => aja.ApplicantProfile)
                .WithMany(ap => ap.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(aja => aja.CompanyJob)
                .WithMany(cj => cj.ApplicantJobApplications)
                .HasForeignKey(aja => aja.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(ap => ap.SecurityLogin)
                .WithMany(sl => sl.ApplicantProfiles)
                .HasForeignKey(ap => ap.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(ap => ap.SystemCountryCode)
                .WithMany(scc => scc.ApplicantProfiles)
                .HasForeignKey(ap => ap.SystemCountryCode);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(ar => ar.ApplicantProfile)
                .WithMany(ap => ap.ApplicantResumes)
                .HasForeignKey(ar => ar.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(aps => aps.ApplicantProfile)
                .WithMany(ap => ap.ApplicantSkills)
                .HasForeignKey(aps => aps.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(awh => awh.SystemCountryCode)
                .WithMany(scc => scc.ApplicantWorkHistories)
                .HasForeignKey(awh => awh.CountryCode);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(awh => awh.ApplicantProfile)
                .WithMany(ap => ap.ApplicantWorkHistories)
                .HasForeignKey(awh => awh.Applicant);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.CompanyProfile)
                .WithMany(cp => cp.CompanyDescriptions)
                .HasForeignKey(cd => cd.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(cd => cd.SystemLanguageCode)
                .WithMany(slc => slc.CompanyDescriptions)
                .HasForeignKey(cd => cd.LanguageId);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(cjd => cjd.CompanyJob)
                .WithMany(cj => cj.CompanyJobDescriptions)
                .HasForeignKey(cjd => cjd.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(cje => cje.CompanyJob)
                .WithMany(cj => cj.CompanyJobEducations)
                .HasForeignKey(cje => cje.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(cj => cj.CompanyProfile)
                .WithMany(cp => cp.CompanyJobs)
                .HasForeignKey(cj => cj.Company);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(cjs => cjs.CompanyJob)
                .WithMany(cj => cj.CompanyJobSkills)
                .HasForeignKey(cjs => cjs.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(cl => cl.CompanyProfile)
                .WithMany(cp => cp.CompanyLocations)
                .HasForeignKey(cl => cl.Company);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(sll => sll.SecurityLogin)
                .WithMany(sl => sl.SecurityLoginsLogs)
                .HasForeignKey(sll => sll.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(slr => slr.SecurityLogin)
                .WithMany(sl => sl.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(slr => slr.SecurityRole)
                .WithMany(sr => sr.SecurityLoginsRoles)
                .HasForeignKey(slr => slr.Role);
                
        }
    }
}
