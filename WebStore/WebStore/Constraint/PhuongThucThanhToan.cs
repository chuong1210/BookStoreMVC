using System.ComponentModel.DataAnnotations;

namespace WebStore.Constraint
{
    public enum PhuongThucThanhToan
    {
        [Display(Name = "Thẻ Tín Dụng")]
        TheTinDung ,
        [Display(Name = "Chuyển Khoản")]

        ChuyenKhoan,
        [Display(Name = "Tiền Mặt")]

        TienMat,
        [Display(Name = "Thẻ MoMo")]

        TheMomo
    }

}
