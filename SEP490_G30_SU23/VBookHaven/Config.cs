using System.ComponentModel;

namespace VBookHaven
{
	public static class Config
	{
		private const string imageRoot = "https://localhost:7123";

		public static string ImageRoot
		{
			get { return imageRoot; }
		}
	}

	public static class OrderStatus
	{
		public const string Create = "Tạo đơn";
		public const string Wait = "Chờ xác nhận";
		public const string Process = "Đang xử lí";
		public const string Shipping = "Đang vận chuyển";
		public const string Shipped = "Đã giao hàng";
		public const string Done = "Đã hoàn thành";
		public const string Cancel = "Đã hủy";

		public static int ToNum(string status)
		{
			switch (status)
			{
				case Create: return 0;
				case Wait: return 1;
				case Process: return 2;
				case Shipping: return 3;
				case Shipped: return 4;
				case Done: return 5;
				case Cancel: return 6;
				default: return -1;
			}
		}
	}
}
