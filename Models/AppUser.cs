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
     public bool isParent {get; set;} = false;
     public bool isRep {get; set;} = false;
     public School School { get; set; }

     public ICollection<Subject> Subjects {get; set;}
     public ICollection<StudentSubjectEnrolled> SubjectEnrolled {get; set;}
     public ICollection<StudentParent> Parents {get; set;}
     public ICollection<StudentParent> Childrens {get; set;}
     public Class Class {get; set;}

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