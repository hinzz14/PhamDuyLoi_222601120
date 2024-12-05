using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhamDuyLoi_222601120.Models;

public partial class HangHoa
{
    public int MaHang { get; set; }

    public int MaLoai { get; set; }
    [Required]
    public string TenHang { get; set; } = null!;
    [Required]
    [Range(100, 5000)]
    public decimal? Gia { get; set; }
    [Required]
    [RegularExpression(@"^.*\.(jpg|png|gif|tiff)$")]
    public string? Anh { get; set; }

    public virtual LoaiHang MaLoaiNavigation { get; set; } = null!;
}
