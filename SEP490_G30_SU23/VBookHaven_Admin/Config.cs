namespace VBookHaven_Admin
{
    public class Config
    {
    }

	public static class OrderStatus
	{
		public const string Create = "Tạo đơn";
		public const string Process = "Đang xử lí";
		public const string Payment = "Chờ thanh toán";
		public const string Packaging = "Đóng gói";
		public const string Ship = "Đang giao hàng";
		public const string Done = "Đã giao";
		public const string Cancel = "Đã hủy";

		public static int ToNum(string status)
		{
			switch (status)
			{
				case Create: return 0;
				case Process: return 1;
				case Payment: return 2;
				case Packaging: return 3;
				case Ship: return 4;
				case Done: return 5;
				case Cancel: return 6;
				default: return -1;
			}
		}
	}
}
