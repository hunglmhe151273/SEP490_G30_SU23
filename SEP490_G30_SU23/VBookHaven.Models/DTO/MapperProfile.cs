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
             .ForMember(dest => dest.UnitInStock, opt => opt.MapFrom(src => src.UnitInStock == null ? 0 : src.UnitInStock))
             .ForMember(dest => dest.PresentImage, opt => opt.MapFrom(src => "../../images/img/" + src.Images.Where(i => i.Status == true).FirstOrDefault().ImageName));
            CreateMap<ProductDTO, Product>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}
