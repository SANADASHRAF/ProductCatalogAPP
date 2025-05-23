using System.ComponentModel.DataAnnotations;

namespace ProductCatalogMVC.DTO
{
    public class ProductForCreationDto
    {
        [Required(ErrorMessage = "الاسم مطلوب")]
        public string Name { get; set; }

        [Required(ErrorMessage = "تاريخ البدء مطلوب")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "المدة مطلوبة")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "الفئة مطلوبة")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "معرف المستخدم مطلوب")]
        public string CreatedByUserId { get; set; }
    }
}
