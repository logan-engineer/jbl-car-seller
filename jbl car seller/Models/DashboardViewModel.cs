using System.ComponentModel.DataAnnotations;

namespace jbl_car_seller.Models
{
    public class DashboardViewModel
    {
        public int CarCount { get; set; }
        public int OrderCount { get; set; }
        public int ReviewCount { get; set; }
        public int CategoryCount { get; set; }
    }

}
