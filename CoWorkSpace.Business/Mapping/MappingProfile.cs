using AutoMapper;
using CoWorkSpace.Domain.DTOs.Responses;
using CoWorkSpace.Domain.Entities;

namespace CoWorkSpace.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponseDTO>();
    }
}