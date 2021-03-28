using Bogus;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Models;

namespace uploaddownloadfiles.Areas.Identity.Data
{
    public class DummyDataSeedData
    {
        private static readonly Random _random = new Random();
        string[] roles = new[] { "Student", "Teachers", "Parents" };
        private static readonly string v = $"{Guid.NewGuid()}";



        public static Faker<AppUser> FakerParent { get; } = new Faker<AppUser>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.UserName, f => f.Name.LastName())
            .RuleFor(p => p.PhoneNumber, f => f.Internet.Email())
            .RuleFor(p=> p.Contact, f => FakeContact)
            .RuleFor(p => p.isParent, f => true);

        public static Faker<AppUser> FakerTeacher { get; } = new Faker<AppUser>()
        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
          .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.UserName, f => f.Internet.UserName())
          .RuleFor(p => p.PhoneNumber, f => f.Internet.Email())
             .RuleFor(p => p.Contact, f => FakeContact)
          .RuleFor(p => p.isTeacher, f => true);

        public static Faker<AppUser> FakerStudent { get; } = new Faker<AppUser>()
             .RuleFor(p => p.FirstName, f => f.Name.FirstName())
             .RuleFor(p => p.LastName, f => f.Name.LastName())
             .RuleFor(p => p.UserName, f => f.Name.LastName())
             .RuleFor(p => p.Contact, f => FakeContact)
             .RuleFor(p => p.PhoneNumber, f => f.Internet.Email())
             .RuleFor(p => p.isStudent, f => true);


        public static Faker<School> FakeSchool { get; } = new Faker<School>()
            .RuleFor(p => p.Name, f => f.Company.CompanyName())
            .RuleFor(p => p.Motto, f => f.Company.CatchPhrase())
            .RuleFor(p => p.Stakeholders, f => FakerParent.Generate(10))
            .RuleFor(p => p.Stakeholders, f => FakerStudent.Generate(100))
            .RuleFor(p => p.Stakeholders, f => FakerTeacher.Generate(10));

        public static Faker<Contact> FakeContact { get; } = new Faker<Contact>()
            .RuleFor(x => x.ContactId, v)
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.State, f => f.Address.State());

        ////public static Faker<Subject> Subjects { get; } = new Faker<Subject>()
        ////    .RuleFor(x => x.Teacher, f => FakerTeacher)
        ////    .RuleFor(x => x.TotalMarks, f => );
    );


        public static async Task InitializeUsers(UserManager<AppUser> usermanager, AppDbContext context)
        {
            var userfake = FakerParent.Generate(100);
          //  var fake
        }
    }
}
