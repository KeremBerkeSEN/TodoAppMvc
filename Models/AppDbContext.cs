using Microsoft.EntityFrameworkCore;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü yukarıda using keywordü ile kullanılacak metotların vs Microsoft namespace'inin EntityFrameworkCore alt namespace'inde olduğunu belirtir.

namespace TodoMvcApp.Models//namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
//
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Todos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}