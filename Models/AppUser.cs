using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace UploadandDowloadService.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isStudent { get; set; } = false;
        public bool isAdmin { get; set; } = false;
        public bool isTeacher { get; set; } = false;
        public bool isParent { get; set; } = false;
        public bool isRep { get; set; } = false;
        public string Region { get; set; }
        public virtual School School { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<StudentSubjectEnrolled> SubjectEnrolled { get; set; }
        public virtual ICollection<StudentParent> Parents { get; set; }
        public virtual ICollection<StudentParent> Childrens { get; set; }
        public virtual Class Class { get; set; }



        public void SetRole(string rolestring)
        {
            try
            {
                var role = (Role)Enum.Parse(typeof(Role), rolestring);
                switch (role)
                {
                    case Role.Student:
                        isStudent = true;
                        break;
                    case Role.Teacher:
                        isTeacher = true;
                        break;
                    case Role.Admin:
                        isAdmin = true;
                        break;
                    case Role.Parent:
                        isParent = true;
                        break;
                    case Role.ClassRep:
                        isRep = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }


        public Role Type
        {
            get
            {
                if (this.isParent)
                {
                    return Role.Parent;
                }
                else if (this.isTeacher)
                {
                    return Role.Teacher;
                }

                return Role.Student;
            }
        }
    }
}