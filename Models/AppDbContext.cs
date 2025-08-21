using Microsoft.EntityFrameworkCore;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü yukarıda using keywordü ile kullanılacak metotların vs Microsoft namespace'inin EntityFrameworkCore alt namespace'inde olduğunu belirtir.

namespace TodoMvcApp.Models//namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
//
{
    public class AppDbContext : DbContext// public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
    //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar
    //AppDbContext : DbContext ise bu sınıfın diğer sınıflarla karışmaması için sadece AppDbContext'daki DbContext sınıfının olduğunu belirtir
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)//AppDbContext: Sınıfın adıdır ve bu bir constructor'dır.
     //Amaç: AppDbContext sınıfından bir nesne oluşturulduğunda, bu constructor çalıştırılır.
        {//DbContextOptions<T>: Entity Framework Core'da, bir DbContext sınıfının yapılandırma seçeneklerini temsil eden bir sınıftır. AppDbContext: Bu DbContext'in türünü belirtir. options: Constructor'a dışarıdan geçirilen bir parametredir ve veritabanı bağlantı bilgileri gibi yapılandırma ayarlarını içerir.
         //Amaç: AppDbContext'in nasıl yapılandırılacağını belirtmek.
        }//": base(options)" Bu, DbContext sınıfının constructor'ını çağırır ve options parametresini temel sınıfa (base class) iletir. Amaç: AppDbContext sınıfının, DbContext sınıfının özelliklerini ve davranışlarını devralmasını sağlamak.

        public DbSet<User> Users { get; set; }// "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                             //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                             //get accessor'ı ile User özelliğinin(property) değeri okunabilir. set accessor'ı ile User özelliğine bir değer atanabilir.
                                                             //"DbSet<User>" C# dilinde bir koleksiyondur (collection). "User" Koleksiyonun tutacağı veri türüdür. Bu durumda, User sınıfına ait nesneler saklanır.
                                                             //"DbSet<User>" bir liste, dizi veya başka bir koleksiyon türü olabilir.
        public DbSet<Todo> Todos { get; set; }// "get" Özelliğin değerini okumak için kullanılan bir accessor'dır. "set" özelliğin değerini yazmak (atamak) için kullanılan bir accessor'dır.
                                                             //C# dilinde property (özellik), bir sınıfın dış dünyaya sunduğu kontrollü bir erişim noktasıdır. Property'ler, bir sınıfın içindeki alanlara (fields) erişimi kontrol etmek için kullanılır. Property'ler genellikle getter ve setter accessor'ları ile tanımlanır.
                                                             //get accessor'ı ile Todos özelliğinin(property) değeri okunabilir. set accessor'ı ile Todos özelliğine bir değer atanabilir.
                                                             //"DbSet<Todo>" C# dilinde bir koleksiyondur (collection). "Todo" Koleksiyonun tutacağı veri türüdür. Bu durumda, Todo sınıfına ait nesneler saklanır.
                                                             //"DbSet<Todo>" bir liste, dizi veya başka bir koleksiyon türü olabilir."?" işareti, özelliğin null değer alabileceğini belirtir.

        protected override void OnModelCreating(ModelBuilder modelBuilder)//"protected" Bir erişim belirtecidir. Bu metodun yalnızca bu sınıfın içinde veya bu sınıftan türetilmiş sınıflar tarafından erişilebilir olduğunu belirtir.
        {//"override" bir keyword'dür ve bir temel sınıfta (base class) tanımlanmış bir sanal (virtual) veya soyut (abstract) metodu geçersiz kılmak (override etmek) için kullanılır.
         //"void" metodun bir dönüş değeri olmadığını belirtir. "OnModelCreating" Entity Framework Core'da veritabanı modeli oluşturulurken çağrılan bir metottur.
        // "ModelBuilder" Entity Framework Core'da, veritabanı modelini yapılandırmak için kullanılan bir sınıftır. "modelBuilder" metodun parametresidir ve veritabanı modelini yapılandırmak için kullanılır.
            modelBuilder.Entity<User>(entity =>//"Entity<User>" "Veritabanında bir tabloyu temsil eden bir sınıfı yapılandırmak için kullanılır."User" Veritabanında yapılandırılacak olan tabloyu temsil eden sınıftır. 
            //Amaç: User sınıfını temel alarak veritabanındaki Users tablosunu yapılandırmak.
            //entity =>: Bir lambda ifadesidir ve entity parametresi üzerinden tablo yapılandırmasını sağlar.
            {
                entity.HasIndex(e => e.Username).IsUnique();// entity.HasIndex Framework Core'da, bir tabloya indeks eklemek için kullanılan bir metottur.
                //"e => e.Username" Lambda ifadesidir ve Username sütununa indeks eklenmesini belirtir.
                //"IsUnique()" Eklenen indeksin benzersiz (unique) olmasını sağlar.
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");//entity.Property Bir tablo sütununu yapılandırmak için kullanılan bir metottur.
                                                                                            //"e => e.CreatedDate" Lambda ifadesidir ve CreatedDate sütununa varsayılan bir değer atanmasını belirtir.
                                                                                            //"HasDefaultValueSql()" Metodu, sütunun varsayılan değerini SQL ifadesi olarak ayarlamak için kullanılır.
                                                                                            //"CURRENT_TIMESTAMP": SQL'de geçerli tarih ve saat anlamına gelir.
            });

            modelBuilder.Entity<Todo>(entity =>//"Entity<Todo>" "Veritabanında bir tabloyu temsil eden bir sınıfı yapılandırmak için kullanılır."Todo" Veritabanında yapılandırılacak olan tabloyu temsil eden sınıftır. 
            //Amaç: Todo sınıfını temel alarak veritabanındaki Todos tablosunu yapılandırmak.
            //entity =>: Bir lambda ifadesidir ve entity parametresi üzerinden tablo yapılandırmasını sağlar.
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");//entity.Property Bir tablo sütununu yapılandırmak için kullanılan bir metottur.
                                                                                            //"e => e.CreatedDate" Lambda ifadesidir ve CreatedDate sütununa varsayılan bir değer atanmasını belirtir.
                                                                                            //"HasDefaultValueSql()" Metodu, sütunun varsayılan değerini SQL ifadesi olarak ayarlamak için kullanılır.
                                                                                            //"CURRENT_TIMESTAMP": SQL'de geçerli tarih ve saat anlamına gelir.
                entity.HasOne(d => d.User)// entity.HasOne, bir varlık ile başka bir varlık arasındaki bire bir ilişkiyi tanımlamak için kullanılır.
                //"d => d.User" Lambda ifadesidir ve Todo tablosundaki User özelliğini temsil eder.
                
                
                
                    .WithMany(p => p.Todos)//"WithMany(p => p.Todos)" İlişkinin diğer tarafını tanımlar. Burada, bir kullanıcının birden fazla Todo'su olabileceği belirtilir.
                    .HasForeignKey(d => d.UserId)//"HasForeignKey(d => d.UserId)" Yabancı anahtarın hangi özellik olacağını belirtir.
                    .OnDelete(DeleteBehavior.Cascade);//"OnDelete(DeleteBehavior.Cascade)" İlişkinin silinme davranışını tanımlar. Burada, kullanıcı silindiğinde ona ait tüm Todo'ların da silinmesi sağlanır.
            });
        }
    }
}