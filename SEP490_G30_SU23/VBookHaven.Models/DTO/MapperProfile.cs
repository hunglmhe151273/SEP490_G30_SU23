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
             .ForMember(dest => dest.PresentImage, opt => opt.MapFrom(src => (src.Images.Where(i => i.Status != false).FirstOrDefault() == null ? "/images/img/" + "default_image.png" : "/images/img/" + src.Images.Where(i => i.Status != false).FirstOrDefault().ImageName)));
            CreateMap<ProductDTO, Product>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Order, OrderDTO>()
                  .ForMember(dest => dest.ToTalPayment, opt => opt.MapFrom(src => Math.Ceiling((decimal)totalPayment(src.OrderDetails))))
                  .ForMember(dest => dest.StaffName, opt => opt.MapFrom(src => src.Staff.FullName))
                ;
            CreateMap<OrderDTO, Order>();
            CreateMap<SubCategory, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, SubCategory>();
        }

        // Tổng tiền của đơn hàng - tham số purchasorder.purchaseOrderDetails
        private decimal? totalPayment(ICollection<OrderDetail> orderDetails)
        {
            decimal sum = 0;
            foreach (var detail in orderDetails)
            {
                if (detail.Quantity.HasValue && detail.UnitPrice.HasValue && detail.Discount.HasValue)
                {
                    sum += (decimal)(detail.Quantity * detail.UnitPrice * (decimal)(1 - detail.Discount / 100));
                }
            }
            return sum;
        }
    }
}
