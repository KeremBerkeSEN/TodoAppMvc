using System.ComponentModel.DataAnnotations;

namespace TodoMvcApp.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Başlık alanı zorunludur")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        public string? Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
