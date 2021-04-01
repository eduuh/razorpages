using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDowloadService;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Areas.Identity.Data;
using UploadandDowloadService.Infratructure;
using UploadandDowloadService.Models;
using uploaddownloadfiles.Models;

namespace uploaddownloadfiles.Areas.Identity.Data
{
    public static class DummyDataSeedData
    {
        private static readonly Random _random = new Random();
        static string[] roles = new[] { "Student", "Teachers", "Parents" };
        private static readonly string v = $"{Guid.NewGuid()}";



        public static Faker<AppUser> FakerParent { get; } = new Faker<AppUser>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.UserName, f => f.Name.LastName())
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.isParent, f => true);

        public static Faker<AppUser> FakerTeacher { get; } = new Faker<AppUser>()
        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
          .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.UserName, f => f.Internet.UserName())
          .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
          .RuleFor(p => p.isTeacher, f => true);

        public static Faker<AppUser> FakerStudent { get; } = new Faker<AppUser>()
             .RuleFor(p => p.FirstName, f => f.Name.FirstName())
             .RuleFor(p => p.LastName, f => f.Name.LastName())
             .RuleFor(p => p.UserName, f => f.Name.LastName())
             .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
             .RuleFor(p => p.isStudent, f => true);


        public static Faker<School> FakeSchool { get; } = new Faker<School>()
            .RuleFor(p => p.Name, f => f.Company.CompanyName())
            .RuleFor(p => p.Contact, f => FakeContact.Generate(1)[0])
            .RuleFor(p => p.Motto, f => f.Company.CatchPhrase());

        public static Faker<Class> FakeClass { get; } = new Faker<Class>()
            .RuleFor(x => x.Id, f => Guid.NewGuid().ToString())
            .RuleFor(x => x.Description, x => x.Company.CatchPhrase())
            .RuleFor(x => x.Name, x => x.Company.CompanyName())
            .RuleFor(x => x.students, x => FakerStudent.Generate(10));

        public static Faker<Contact> FakeContact { get; } = new Faker<Contact>()
            .RuleFor(x => x.Name, f => f.Address.StreetName())
            .RuleFor(x => x.Region, f => f.Address.City())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.Address, f => f.Address.FullAddress())
            .RuleFor(x => x.Pobox, f => f.Address.ZipCode())
            .RuleFor(x => x.State, f => f.Address.State());




        public static async Task InitializeUsers(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<AppDbContext>();

            var fakeschool = FakeSchool.Generate(1)[0];
            fakeschool.Classes = FakeClass.Generate(10);
            var fakeclass = FakeClass.Generate(1)[0];

            fakeschool.Classes.Add(fakeclass);
            var faketeacher = FakerTeacher.Generate(5);
            var fakestudent = FakerStudent.Generate(6);
            var fakeparent = FakerParent.Generate(12);

            var english = createSubject("English", fakeclass, faketeacher[0]);
            var kiswahili = createSubject("Kiswahili", fakeclass, faketeacher[1]);
            var socialstudies = createSubject("Social Studies", fakeclass, faketeacher[2]);
            var science = createSubject("Science", fakeclass, faketeacher[2]);
            var cre = createSubject("CRE", fakeclass, faketeacher[4]);


            context.Subjects.Add(english);
            context.Subjects.Add(kiswahili);
            context.Subjects.Add(socialstudies);
            context.Subjects.Add(science);
            context.Subjects.Add(cre);

            // ensuring roles
            foreach (var appuser in faketeacher)
            {
                try
                {
                    appuser.School = fakeschool;
                    var teacherid = await UsersUtilities.EnsureUser(provider, "Pa$$w0rd", appuser);
                    await UsersUtilities.EnsureRole(provider, teacherid, Role.Teacher.ToString());
                }
                catch (System.Exception)
                {
                }
            }


            foreach (var appuser in fakeparent)
            {

                try
                {
                    appuser.School = fakeschool;
                    var parentid = await UsersUtilities.EnsureUser(provider, "Pa$$w0rd", appuser);
                    await UsersUtilities.EnsureRole(provider, parentid, Role.Teacher.ToString());
                }
                catch (System.Exception)
                {
                }
            }

            foreach (var appuser in fakestudent)
            {

                try
                {
                    appuser.School = fakeschool;
                    var studentid = await UsersUtilities.EnsureUser(provider, "Pa$$w0rd", appuser);
                    await UsersUtilities.EnsureRole(provider, studentid, Role.Student.ToString());
                }
                catch (System.Exception)
                {
                }
            }


            var stakeholder = new List<AppUser>();
            stakeholder.AddRange(faketeacher);
            stakeholder.AddRange(fakestudent);
            stakeholder.AddRange(fakeparent);
            fakeschool.Stakeholders = stakeholder;

            context.Schools.Add(fakeschool);
            await context.SaveChangesAsync();
        }

        private static Subject createSubject(string subjectname, Class classs, AppUser appUser)
        {

            if (!appUser.isTeacher) throw new RestException(System.Net.HttpStatusCode.Forbidden, new { errors = "Only Teacher allowed to Teach" });
            var subject = new Subject();
            subject.Class = classs;
            subject.Name = subjectname;
            subject.Teacher = appUser;
            return subject;
        }
    }
}
