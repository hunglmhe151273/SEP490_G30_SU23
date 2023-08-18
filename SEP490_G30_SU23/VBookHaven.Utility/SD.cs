using System.Numerics;

namespace VBookHaven.Utility
{
    public static class SD
    {
        public const string Role_Owner = "Chủ cửa hàng";
        //public const string Role_Storekeeper = "Thủ kho";
        //public const string Role_Seller = "Nhân viên bán hàng";
        public const string Role_Staff = "Nhân viên";
        public const string Role_Customer = "Khách hàng";
        public const string PurchaseOrder_Created = "Tạo đơn hàng";
        public const string PurchaseOrder_Imported = "Đã nhập";
        public const string PurchaseOrder_Complete = "Hoàn thành";
        public const string PurchaseOrder_Canceled = "Đã Hủy";
        public static int POStatusToNum(string status)
        {
            switch (status)
            {
                case PurchaseOrder_Created: return 1;
                case PurchaseOrder_Imported: return 2;
                case PurchaseOrder_Complete: return 3;
                case PurchaseOrder_Canceled: return 4;
                default: return 0;
            }
        }
    }
}
