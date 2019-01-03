using System;
using System.ComponentModel.DataAnnotations;

namespace Heavy.Web.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Display(Name = "专辑名")]
        public string Title { get; set; }

        [Display(Name = "艺人")]
        public string Artist { get; set; }

        [Display(Name = "发行日期")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }

        [Display(Name = "封面地址")]
        public string CoverUrl { get; set; }
    }
}

