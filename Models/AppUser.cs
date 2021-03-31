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
        public bool isTeacher { get; set; } = false;
        public bool isParent { get; set; } = false;
        public bool isRep { get; set; } = false;
        public virtual School School { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<StudentSubjectEnrolled> SubjectEnrolled { get; set; }
        public virtual ICollection<StudentParent> Parents { get; set; }
        public virtual ICollection<StudentParent> Childrens { get; set; }
        public virtual Class Class { get; set; }

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

                return Role.Admin;
            }
        }
    }
}