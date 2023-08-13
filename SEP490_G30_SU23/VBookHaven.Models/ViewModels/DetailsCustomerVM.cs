using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VBookHaven.Models.DTO;
using VBookHaven.ViewModels;

namespace VBookHaven.Models.ViewModels
{
    public class DetailsCustomerVM
    {
        public DetailsCustomerVM()
        {
            OrderDTOs = new List<OrderDTO>();
            ShippingInfos = new List<ShippingInfo>();
            Customer = new Customer();
        }
        public List<OrderDTO> OrderDTOs { get; set; }
        public List<ShippingInfo> ShippingInfos { get; set; }
        public Customer Customer { get; set; }
        //tổng số đơn hàng
        public int? totalOrderQuantity { get; set; }
        //số lượng sản phẩm đã mua - tổng quantity in orderdetails
        public int? totalBuyProduct { get; set; }
    }
}
