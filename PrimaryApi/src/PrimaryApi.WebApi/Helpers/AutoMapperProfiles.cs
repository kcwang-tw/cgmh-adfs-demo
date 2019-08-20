using AutoMapper;
using PrimaryApi.Core.DomainModels;
using PrimaryApi.WebApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaryApi.WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserRank, UserRankResource>();
            CreateMap<SeatingPlan, SeatingPlanResource>();
        }
    }
}
