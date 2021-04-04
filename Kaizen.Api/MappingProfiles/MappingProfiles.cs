using AutoMapper;
using Kaizen.Models;
using Kaizen.Models.Dto;
using Kaizen.Models.Models;
using UploadandDowloadService.Dto.AppUser;

namespace UploadandDowloadService.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Subject, SubjectsDtos>();
            CreateMap<SubjectsDtos, Subject>();
            CreateMap<Class, ClassDto>();
            CreateMap<ClassDto, Class>();
            CreateMap<School, SchoolDto>();
            CreateMap<SchoolDto, School>();
            CreateMap<Content, ContentDto>();
            CreateMap<ContentDto, Content>();
            CreateMap<ContentResult, Content>();
            CreateMap<Content, ContentResult>();
            CreateMap<TrainingSubject, TrainingContentDto>();
            CreateMap<TrainingContentDto, TrainingSubject>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
            CreateMap<CultureReportDto, CultureReport>();
            CreateMap<CultureReport, CultureReportDto>();
        }
    }
}