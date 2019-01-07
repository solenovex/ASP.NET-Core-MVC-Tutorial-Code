using System;
using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.ViewModels
{
    public class AlbumCreateViewModel
    {
        [Display(Name = "专辑名")]
        [Required(ErrorMessage = "{0}是必填项"), MaxLength(100, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Title { get; set; }

        [Display(Name = "艺人")]
        [Required(ErrorMessage = "{0}是必填项"), MaxLength(100, ErrorMessage = "{0}的长度不可超过{1}")]
        public string Artist { get; set; }

        [Display(Name = "发行日期")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "价格")]
        [Required(ErrorMessage = "{0}是必填项"), Range(0.01, 100, ErrorMessage = "{0}的价格必须在{1}和{2}之间")]
        public decimal Price { get; set; }

        [Display(Name = "封面地址")]
        [Required(ErrorMessage = "{0}是必填项"), MaxLength(200, ErrorMessage = "{0}的长度不可超过{1}")]
        public string CoverUrl { get; set; }
    }
}
