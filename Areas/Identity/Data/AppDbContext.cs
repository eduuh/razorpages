using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Controllers;
using UploadandDowloadService.Models;

namespace UploadandDowloadService.Areas.Identity
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<TrainingSubject> TrainingContents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //student, parent, teacher and school
            builder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();
            builder.Entity<AppUser>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<AppUser>().HasOne(s => s.School).WithMany(st => st.Stakeholders);
            // student and class
            builder.Entity<AppUser>().HasOne(s => s.Class).WithMany(c => c.students);
            // subject and content
            builder.Entity<Subject>().HasMany(content => content.Contents).WithOne(s => s.Subject);
            // teacher and subject
            builder.Entity<AppUser>().HasMany(s => s.Subjects).WithOne(s => s.Teacher);
            // class and  classrep

            builder.Entity<TrainingSubject>().HasMany(s => s.Contents).WithOne(s => s.TraingSubject);

            builder.Entity<School>().HasMany(s => s.Classes).WithOne(s => s.School);

            //   builder.Entity<Class>().HasMany(s => s.ClassRep).WithOne(s => s.Class);

            // student and subject
            // a student can take many subject
            // a subject is taken by manyt teachers
            // we need a join table
            //builder.Entity<AppUser>().HasMany(s => s.Subjects).WithMany
            builder.Entity<StudentSubjectEnrolled>(x => x.HasKey(ss => new { ss.AppUserId, ss.SubjectId }));
            builder.Entity<StudentSubjectEnrolled>().HasOne(u => u.Appuser).WithMany(a => a.SubjectEnrolled).HasForeignKey(f => f.AppUserId);
            builder.Entity<StudentSubjectEnrolled>().HasOne(u => u.Subject).WithMany(u => u.StudentEnrolled).HasForeignKey(u => u.SubjectId);

            // Student and parent
            // a student can have more parents
            // a parent can have many children
            // This is self referencing entity

            builder.Entity<StudentParent>(x => x.HasKey(sp => new { sp.ParentId, sp.StudentId }));
            builder.Entity<StudentParent>().HasOne(s => s.Student).WithMany(x => x.Parents).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<StudentParent>().HasOne(s => s.Parent).WithMany(x => x.Childrens).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);


        }
    }
}
