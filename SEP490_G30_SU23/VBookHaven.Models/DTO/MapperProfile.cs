using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBookHaven.ViewModels;

namespace VBookHaven.Models.DTO
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<SupplierDTO, Supplier>();
            CreateMap<Product, ProductDTO>()
             .ForMember(dest => dest.PresentImage, opt => opt.MapFrom(src => "\\images\\img\\" + src.Images.FirstOrDefault().ImageName));
            CreateMap<ProductDTO, Product>();
        }
    }
}
