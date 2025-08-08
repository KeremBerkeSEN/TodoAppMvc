using TodoMvcApp.Models;

namespace TodoMvcApp.Data
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;

        public DatabaseSeeder(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { 
                        Username = "admin", 
                        Password = "admin123", 
                        Email = "admin@example.com",
                        CreatedDate = DateTime.Now
                    },
                    new User { 
                        Username = "test", 
                        Password = "test123", 
                        Email = "test@example.com",
                        CreatedDate = DateTime.Now
                    }
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }
    }
}
