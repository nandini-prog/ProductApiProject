using AutoMapper;
using Application.DTOs;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Mapping
{
    /// <summary>
    /// AutoMapper profile to map between Domain entities and DTOs.
    /// Keep mapping rules centralized here.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain -> DTO
            CreateMap<Product, ProductDto>();

            // DTO -> Domain (for create)
            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore());

            // DTO -> Domain (for update) - only map fields allowed to be updated
            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedOn, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore());
        }
    }
}
