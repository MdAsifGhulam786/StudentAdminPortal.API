﻿using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Profiles.AfterMaps;
using DataModels = StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DataModels.Student, Student>().ReverseMap();

            CreateMap<DataModels.Gender, Gender>().ReverseMap();

            CreateMap<DataModels.Address, Address>().ReverseMap();

            CreateMap<UpdateStudentRequest, DataModels.Student>()
                .AfterMap<UpdateStudentRequestMap>().ReverseMap();

            CreateMap<AddStudentRequest, DataModels.Student>()
                .AfterMap<AddStudentRequestMap>().ReverseMap();

        }
    }
}
