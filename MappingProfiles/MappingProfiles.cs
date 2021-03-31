using AutoMapper;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Dto.MappingProfiles;
using UploadandDowloadService.Models;

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
            CreateMap<TrainingSubject, TrainingContentDto>();
            CreateMap<TrainingContentDto, TrainingSubject>();
        }
    }
}