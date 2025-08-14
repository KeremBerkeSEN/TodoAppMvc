using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TodoMvcApp.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        [EmailAddress]
        public string? Email { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        

        public virtual ICollection<Todo>? Todos { get; set; }
    }
}
