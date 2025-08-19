using TodoMvcApp.Models;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtirmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü yukarıda TodoMvcApp kütüphanesinin Models kısmında olduğunu belirtir.
namespace TodoMvcApp.Data// ";" satırı sonlandırır ve compiler'a yazdığımız deyimin bittiğini ifade ederiz 
{// Yukarıda namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
    public class DatabaseSeeder//public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
    //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar
    {
        private readonly AppDbContext _context;//readonly keyword'ü bir alanın (field) yalnızca tanımlandığı sırada veya constructor içinde atanabileceğini belirtir

        public DatabaseSeeder(AppDbContext context)// "_context" değişkenin adıdır. AppDbContext türünde bir nesneyi tutar. "private" bir erişim belirtecidir. Bu değişkenin yalnızca tanımlandığı sınıf içinde erişilebilir olduğunu belirtir amaç "_context" değişkeninin yalnızca DatabaseSeeder "sınıfı" içinde kullanılmasını sağlamak.
        {
            _context = context;//"=" operatörü burada sağdaki değeri soldaki değişkene atar. AppDbContext türünde bir nesneyi tutar.
        }

        public void Seed()//"Seed" bir metodun adıdır. "void" keyword'ü  metodun bir dönüş değeri olmadığını belirtir. Bu durumda, "Seed" metodu herhangi bir değer döndürmez.
        {
            if (!_context.Users.Any())//if bir koşul ifadesidir,() içerisinde koşullar belirtilir, belirtilen koşul doğruysa {} içinde tanımlanan kod bloğu çalıştırılır.
            {//"!" mantıksal "değil" operatörüdür ve koşulun tersini alır. "." bir nesnenin özelliklerine veya metodlarına erişmek için kullanılır.Bu durumda, _context nesnesinin tersinin Users özelliğine ve ardından Any metoduna erişmek için kullanılmıştır.
                var users = new List<User>//"var" değişkenin türünü otomatik olarak belirlemek için kullanılan bir keyword'dür. "users" değişkenin adıdır. "new" yeni bir nesne oluşturmak için kullanılan bir keyword'dür.
                //"List<User>" C# dilinde bir generic koleksiyon türüdür. "User" türündeki nesneleri saklayacak bir listeyi temsil eder.
                {
                    new User {
                        Username = "admin",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        Password = "admin123",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        Email = "admin@example.com",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        CreatedDate = DateTime.Now//"=" operatörü burada sağdaki değeri soldaki değişkene atar. "DateTime" C# dilinde tarih ve saat bilgilerini temsil eden bir sınıftır.
                    },//"Now" DateTime sınıfının bir özelliğidir. Bu özellik, şu anki tarih ve saati döndürür.
                    new User {
                        Username = "test",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        Password = "test123",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        Email = "test@example.com",//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        CreatedDate = DateTime.Now//"=" operatörü burada sağdaki değeri soldaki değişkene atar. "DateTime" C# dilinde tarih ve saat bilgilerini temsil eden bir sınıftır.
                    }//"Now" DateTime sınıfının bir özelliğidir. Bu özellik, şu anki tarih ve saati döndürür.
                };

                _context.Users.AddRange(users);//"_context" veritabanı işlemleri için kullanılan bir DbContext nesnesidir. "." bir nesnenin özelliklerine veya metodlarına erişmek için kullanılır.Bu durumda, _context nesnesinin Users özelliğine ve ardından AddRange metoduna erişmek için kullanılmıştır.
                _context.SaveChanges();//SaveChanges() veritabanındaki değişiklikleri kayıt etmek için kullanılan bir metottur.
            }
        }
    }
}
