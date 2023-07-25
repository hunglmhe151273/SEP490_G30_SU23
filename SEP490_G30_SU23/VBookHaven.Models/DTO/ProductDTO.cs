﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBookHaven.Models.DTO
{
    public class ProductDTO
    {
        public int? ProductId { get; set; }
        [StringLength(20, ErrorMessage = "Tên sản phẩm không được vượt quá 20 kí tự.")]
        [Required]
        public string? Name { get; set; }
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 kí tự.")]

        public string? Barcode { get; set; }
        [StringLength(100, ErrorMessage = "Đơn vị không được vượt quá 100 kí tự.")]
        [Required]
        public string? Unit { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải là số nguyên không nhỏ hơn 0.")]
        [Required]
        public int? PurchasePrice { get; set; }
        public int? UnitInStock { get; set; }
        public bool IsBook { get; set; }
        [StringLength(200, ErrorMessage = "Tên sản phẩm không được vượt quá 100 kí tự.")]
        public string? PresentImage { get; set; }
    }
}