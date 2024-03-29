﻿using AutoMapper;
using DataModels = StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;


namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class UpdateStudentRequestMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
    {
       

        public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
