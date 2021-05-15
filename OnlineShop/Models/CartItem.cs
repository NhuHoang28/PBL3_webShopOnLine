using Model.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
	[Serializable]
	public class CartItem
	{
		public SanPham SanPham { get; set; }
		public int SoLuong { get; set; }

	}
}