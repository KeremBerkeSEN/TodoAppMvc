using System.Diagnostics; //"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtirmek 
using Microsoft.AspNetCore.Mvc; // için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü yukarıda system kütüphanesinin diagnostics kısmında olduğunu belirtir.
using TodoMvcApp.Models;// ";" satırı sonlandırır ve compiler'a yazdığımız deyimin bittiğini ifade ederiz 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TodoMvcApp.Controllers   // burada namespace bir keyword'tür namespace burada ifade edilecek tanımların başka yerlerdeki tanımlarla
//karıştırılmaması ve buraya özgü olduğunu belirtmek için kullanılır. 
{
    public class HomeController : Controller// public,private ve protected gibi keyword'ler önüne yazıldığı sınıf vb. seylerin erişilebilme türlerini ifade eder
    //class ise bir nesneyi düzenlemek ve özel bir şekilde inşaa etmek için parçalarına (class'larına) bölüp bu parçalardan tekrar bir bütün elde etmemizi sağlar
    //HomeController : Controller ise bu sınıfın diğer sını flarlarla karışmaması için sadece HomeController'daki Controller sınıfının olduğunu belirtir
    {
        private readonly ILogger<HomeController> _logger;//Burada <> operatörleri, bir sınıfın belirli bir türle çalışmasını sağlar. 
        private readonly AppDbContext _context;//readonly keyword'ü bir alanın (field) yalnızca tanımlandığı sırada veya constructor içinde atanabileceğini belirtir
        public HomeController(ILogger<HomeController> logger, AppDbContext context)//Burada <> operatörleri, bir sınıfın belirli bir türle çalışmasını sağlar. 
        {//"," bir operatördür ve Yukarıda "," birden fazla parametreyi/metodu ayırmak için kullanılmıştır
            _logger = logger;//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
            _context = context;//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
        }

        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult Index()//burada IActionResult bir arayüzdür ve bu arayüzü uygulayan sınıflar, HTTP isteklerine karşılık gelen sonuçları temsil eder
        {//Index() bir metottur.() operatörü, bir metodu çağırmak için kullanılır.
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");//Burada "var" keyword'ü değişkeni bildirmek ve derleyicinin değişkenin türünü otomatik belirlemesi için kullanılır.
            //userId değişkeni, User nesnesi içerisinden FindFirst fonksiyonu ile ClaimTypes.NameIdentifier değerini bulur ve bu değeri int türüne dönüştürür. Eğer bu değer null ise, "0" olarak varsayılır.
            var todos = _context.Todos.Where(t => t.UserId == userId).ToList();
            return View(todos);//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.View() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.  
        }//Parantezler arasında yazılanlar metot için gerekli parametrelerin iletilmesini sağlar.

        public IActionResult Privacy()//Burada Public bir erişim belirtecidir, IActionResult ASP.NET Core'da bir Arayüz ismidir,Privacy() ise bir methottur
        {
            return View();//View() bir methottur return ise bu  View() metodu çağrılmasını ve değer döndürmesini sağlayan bir keyword'dür
        }

        [HttpGet]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult Login()//Burada Public bir erişim belirtecidir, IActionResult ASP.NET Core'da bir Arayüz ismidir,Login() ise bir methottur
        {
            return View();//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.View() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public async Task<IActionResult> Login(string username, string password)//async bu metotun asenkron çalışacağını belirtir.Asenkron işlemler, uygulamanın diğer işlemleri engellemeden uzun süren işlemleri gerçekleştirmesine olanak tanır.
        //Login bit methot adıdır bu methodun aldığı parametrelerde string keyword'ü metin değeri tutan bir veri türüdür 
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);//user değişkenin adıdır.
            //FirstOrDefault bğir methottur."u =>" Lambda ifadesidir ve LINQ sorgularında kullanılır."==" Eşitlik operatörüdür."&&" Mantıksal VE operatörüdür.
            if (user != null)//if bir koşul ifadesidir,() içerisinde koşullar belirtilir, belirtilen koşul doğruysa {} içinde tanımlanan kod bloğu çalıştırılır.
            {//burada "!=" bir karşılaştırma operatörüdür. İki değerin eşit olmadığını kontrol eder. "null" bir değerdir ve bir değişkenin herhangi bir nesneye işaret etmediğini belirtir. Burada user değişkeninin değeri null değilse {} içindeki kod satırları çalıştıralacaktır
                var claims = new List<Claim>//claims: Değişkenin adıdır. "new" yeni bir nesne oluşturmak için kullanılan bir keyword'dür.
                {//Bu durumda, claims değişkeninin türü List<Claim> olacaktır. Derleyici, sağ taraftaki ifadeye bakarak türü otomatik olarak belirler.

                    new Claim(ClaimTypes.Name, user.Username ?? string.Empty),//"," bir operatördür ve yan tarafta "," liste öğelerini ayırmak için kullanılmıştır. "??" Null kontrolü yapan bir operatördür. Eğer user.Username null ise, "string.Empty" değeri kullanılır. "string.Empty" Boş bir string değeridir
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())//.ToString()  bir değeri string formatına dönüştürmek için kullanılan bir metottur.
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperties);
                //await biir asenkron işlemi beklemek için kullanılan bir keyword'dür. "await" keyword'ü  yalnızca async olarak işaretlenmiş bir metot içinde kullanılabilir.
                return RedirectToAction("Index");//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.RedirectToAction() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.
            }//"Index" RedirectToAction metoduna parametre olarak verilen bir string'dir.

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";//ViewBag nesnesine Error özelliğinin eklenmesini ve bu özelliğe "Kullanıcı adı veya şifre hatalı" değerinin atanmasını ifade eder
            return View();//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.View() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.
        }

        public async Task<IActionResult> Logout()//async bu metotun asenkron çalışacağını belirtir.Asenkron işlemler, uygulamanın diğer işlemleri engellemeden uzun süren işlemleri gerçekleştirmesine olanak tanır.
        //Logout bir methot adıdır bu methodun aldığı parametrelerde string keyword'ü metin değeri tutan bir veri türüdür 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//await biir asenkron işlemi beklemek için kullanılan bir keyword'dür. "await" keyword'ü  yalnızca async olarak işaretlenmiş bir metot içinde kullanılabilir.
            return RedirectToAction("Login");//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.RedirectToAction() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.
        //"Login" RedirectToAction metoduna parametre olarak verilen bir string'dir.
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult AddTodo([FromBody] Todo todo)//"[FromBody]" bir attribute'dur. "Todo" bir sınıf ismidir."todo" ise metodun aldığı parametrenin adıdır.
        {
            try//"try" hata yönetimi için kullanıla nbir keyword'tür. "try" bloğu içinde hata oluşabilecek kodlar yazılır. Eğer bir hata olulursa catch bloğu çalıştırılır.
            {
                if (!string.IsNullOrEmpty(todo.Title))//"string.IsNullOrEmpty" bir string'in null veya boş olup olmadığını kontrol eden bir metottur.
                {
                    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");//" var" değişkenin türünü otomatik olarak belirler. "int.Parse" string bir değeri tam sayıya (int) dönüştürür.
                    var newTodo = new Todo//"new" yeni bir nesne oluşturmak için kullanılan bir keyword'dür.
                    {//"{}" Yeni bir nesne oluştururken, bu nesnenin özelliklerini başlatmak için kullanılır. "," ise Nesne başlatıcı içinde birden fazla özelliği ayırmak için kullanılır (son özellik hariç).

                        Title = todo.Title,//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        Description = todo.Description,//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        IsCompleted = false,//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                        CreatedDate = DateTime.Now,//"=" operatörü burada sağdaki değeri soldaki değişkene atar. "DateTime" C# dilinde tarih ve saat bilgilerini temsil eden bir sınıftır.
                        //"Now" DateTime sınıfının bir özelliğidir. Bu özellik, şu anki tarih ve saati döndürür.
                        UserId = userId//"=" operatörü burada sağdaki değeri soldaki değişkene atar.
                    };

                    _context.Todos.Add(newTodo);//Add() veritabanına yeni bir kayıt eklemek için kullanılan bir metottur. "newTodo" veritabanına eklenmek istenen nesnedir. 
                    _context.SaveChanges();//SaveChanges() veritabanındaki değişiklikleri kayıt etmek için kullanılan bir metottur.
                    
                    return Json(new { success = true, todo = newTodo });//"Json" JSON formatında bir yanıt döndürmek için kullanılan bir metottur.
                }//"success = true" işlemin başarılı olduğunu belirtir.
                return Json(new { success = false, message = "Başlık boş olamaz" });//"success = false" işlemin başarısız olduğunu belirtir.
            }   //message = "Başlık boş olamaz" message değerini "Başlık boş olamaz" olarak değiştirir.
            catch (Exception ex)//"catch" "try" bloğunda bir hata oluşursa çalıştırılır. "Exeption" oluşan hatayı temsil eden bir sınıftır.
            {//"ex" hata nesnesinin adıdır.
                _logger.LogError(ex, "Görev eklenirken hata oluştu");//"_logger" bir nesnedir ve LogError metodu, hata mesajını kaydetmek için kullanılır. "ex" catch bloğunda yakalanan hata nesnesidir.
                //"Görev eklenirken hata oluştu" loglama sırasında kullanılacak açıklayıcı bir mesajdır. Bu mesaj, log kaydında hata ile ilgili bağlam sağlamak için kullanılır
                return Json(new { success = false, message = "Bir hata oluştu" });//"success = false" işlemin başarısız olduğunu belirtir.
            }//message = "Başlık boş olamaz" message değerini "Başlık boş olamaz" olarak değiştirir.
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult ToggleTodo(int id)//IActionResult bir interface(arayüz)'dir."int id" ToggleTodo() metotunun parametresidir ve id değişkeninin veri türünün belirlenmesini ifade eder.
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");//Burada "var" keyword'ü değişkeni bildirmek ve derleyicinin değişkenin türünü otomatik belirlemesi için kullanılır.
            //userId değişkeni, User nesnesi içerisinden FindFirst fonksiyonu ile ClaimTypes.NameIdentifier değerini bulur ve bu değeri int türüne dönüştürür. Eğer bu değer null ise, "0" olarak varsayılır.
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);//"_context.Todos" veritabanındaki Todos tablosuna erişim sağlar. "FirstOrDefault" metodu, todos koleksiyonunda belirtilen koşula uyan ilk öğeyi bulur.
                //Todos koleksiyonundaki her bir öğe için şu koşullar kontrol edilir. "t.Id" özelliği, "id" parametresine eşit mi? "t.UserId" özelliği, "userId" değişkenine eşit mi? Eğer belirtilen koşullara uyan bir öğe varsa, bu öğe "todo" değişkenine atanır. Eğer böyle bir öğe yoksa, "todo" değişkeni null olur.
            
            if (todo != null)//if bir koşul ifadesidir,() içerisinde koşullar belirtilir, belirtilen koşul doğruysa {} içinde tanımlanan kod bloğu çalıştırılır.
            {//"!=" mantıksal eşit değil operatörüdür. "todo" değişkeni "null" değerinden farklı bir değer ise {} içindeki kod bloğu çalıştırılır.
                todo.IsCompleted = !todo.IsCompleted;//"!" operatörü mantıksal "değil" operatörüdür. Burada todo.IsCompleted özelliğinin değerini tersine çevirir.
                _context.SaveChanges();//SaveChanges() veritabanındaki değişiklikleri kayıt etmek için kullanılan bir metottur.
                return Json(new { success = true });//"Json" JSON formatında bir yanıt döndürmek için kullanılan bir metottur.
            }//"success = true" işlemin başarılı olduğunu belirtir.
            return Json(new { success = false, message = "Görev bulunamadı" });//"success = false" işlemin başarısız olduğunu belirtir. message = "Görev bulunamadı" ,message değerini "Görev bulunamadı" olarak değiştirir.
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult DeleteTodo([FromBody] int id) //IActionResult bir interface(arayüz)'dir."int id" DeleteTodo() metotunun parametresidir ve id değişkeninin veri türünün belirlenmesini ifade eder.
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");//Burada "var" keyword'ü değişkeni bildirmek ve derleyicinin değişkenin türünü otomatik belirlemesi için kullanılır.
                //userId değişkeni, User nesnesi içerisinden FindFirst fonksiyonu ile ClaimTypes.NameIdentifier değerini bulur ve bu değeri int türüne dönüştürür. Eğer bu değer null ise, "0" olarak varsayılır.
                var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);//"_context.Todos" veritabanındaki Todos tablosuna erişim sağlar. "FirstOrDefault" metodu, todos koleksiyonunda belirtilen koşula uyan ilk öğeyi bulur.
                //Todos koleksiyonundaki her bir öğe için şu koşullar kontrol edilir. "t.Id" özelliği, "id" parametresine eşit mi? "t.UserId" özelliği, "userId" değişkenine eşit mi? Eğer belirtilen koşullara uyan bir öğe varsa, bu öğe "todo" değişkenine atanır. Eğer böyle bir öğe yoksa, "todo" değişkeni null olur.
                
                if (todo != null)//if bir koşul ifadesidir,() içerisinde koşullar belirtilir, belirtilen koşul doğruysa {} içinde tanımlanan kod bloğu çalıştırılır.
                //"!=" mantıksal eşit değil operatörüdür. "todo" değişkeni "null" değerinden farklı bir değer ise {} içindeki kod bloğu çalıştırılır.
                {
                    _context.Todos.Remove(todo);//Remove() veritabanına yeni bir kayıt eklemek için kullanılan bir metottur. "todo" veritabanından silinmek istenen nesnedir. 
                    _context.SaveChanges();//SaveChanges() veritabanındaki değişiklikleri kayıt etmek için kullanılan bir metottur.
                    return Json(new { success = true });//"Json" JSON formatında bir yanıt döndürmek için kullanılan bir metottur.
                }//"success = true" işlemin başarılı olduğunu belirtir.
                return Json(new { success = false, message = "Görev bulunamadı" });//"success = false" işlemin başarısız olduğunu belirtir. message = "Görev bulunamadı" ,message değerini "Görev bulunamadı" olarak değiştirir.
            }
            catch (Exception ex)//"catch" "try" bloğunda bir hata oluşursa çalıştırılır. "Exeption" oluşan hatayı temsil eden bir sınıftır.
            {//"ex" hata nesnesinin adıdır.
                _logger.LogError(ex, "Görev silinirken hata oluştu");//"_logger" bir nesnedir ve LogError metodu, hata mesajını kaydetmek için kullanılır. "ex" catch bloğunda yakalanan hata nesnesidir.
                //"Görev eklenirken hata oluştu" loglama sırasında kullanılacak açıklayıcı bir mesajdır. Bu mesaj, log kaydında hata ile ilgili bağlam sağlamak için kullanılır
                return Json(new { success = false, message = "Bir hata oluştu" });//"success = false" işlemin başarısız olduğunu belirtir. message = "Bir hata oluştu" ,message değerini "Bir hata oluştu" olarak değiştirir.
            }
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult CompleteTodo([FromBody] int id) //IActionResult bir interface(arayüz)'dir."int id" CompleteTodo() metotunun parametresidir ve id değişkeninin veri türünün belirlenmesini ifade eder.
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");//Burada "var" keyword'ü değişkeni bildirmek ve derleyicinin değişkenin türünü otomatik belirlemesi için kullanılır.
                //userId değişkeni, User nesnesi içerisinden FindFirst fonksiyonu ile ClaimTypes.NameIdentifier değerini bulur ve bu değeri int türüne dönüştürür. Eğer bu değer null ise, "0" olarak varsayılır.
                var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);//"_context.Todos" veritabanındaki Todos tablosuna erişim sağlar. "FirstOrDefault" metodu, todos koleksiyonunda belirtilen koşula uyan ilk öğeyi bulur.
                //Todos koleksiyonundaki her bir öğe için şu koşullar kontrol edilir. "t.Id" özelliği, "id" parametresine eşit mi? "t.UserId" özelliği, "userId" değişkenine eşit mi? Eğer belirtilen koşullara uyan bir öğe varsa, bu öğe "todo" değişkenine atanır. Eğer böyle bir öğe yoksa, "todo" değişkeni null olur.
                if (todo != null)//if bir koşul ifadesidir,() içerisinde koşullar belirtilir, belirtilen koşul doğruysa {} içinde tanımlanan kod bloğu çalıştırılır.
            //"!=" mantıksal eşit değil operatörüdür. "todo" değişkeni "null" değerinden farklı bir değer ise {} içindeki kod bloğu çalıştırılır
                {
                    _context.Todos.Remove(todo);//Remove() veritabanına yeni bir kayıt eklemek için kullanılan bir metottur. "todo" veritabanından silinmek istenen nesnedir. 
                    _context.SaveChanges();//SaveChanges() veritabanındaki değişiklikleri kayıt etmek için kullanılan bir metottur.
                    return Json(new { success = true });//"Json" JSON formatında bir yanıt döndürmek için kullanılan bir metottur.
                }//"success = true" işlemin başarılı olduğunu belirtir.
                return Json(new { success = false, message = "Görev bulunamadı" });//"success = false" işlemin başarısız olduğunu belirtir. message = "Görev bulunamadı" ,message değerini "Görev bulunamadı" olarak değiştirir.
            }
            catch (Exception ex)//"catch" "try" bloğunda bir hata oluşursa çalıştırılır. "Exeption" oluşan hatayı temsil eden bir sınıftır.
            {//"ex" hata nesnesinin adıdır.
                _logger.LogError(ex, "Görev tamamlanırken hata oluştu");//"Görev eklenirken hata oluştu" loglama sırasında kullanılacak açıklayıcı bir mesajdır. Bu mesaj, log kaydında hata ile ilgili bağlam sağlamak için kullanılır
                return Json(new { success = false, message = "Bir hata oluştu" });//"success = false" işlemin başarısız olduğunu belirtir. message = "Bir hata oluştu" ,message değerini "Bir hata oluştu" olarak değiştirir.
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]//[ResponseCache] bir attribute'dur ve ASP.NET Core'da yanıtların önbelleğe alınmasını kontrol etmek için kullanılır.Duration = 0: Yanıtın önbellekte ne kadar süreyle saklanacağını belirtir. 0 değeri, yanıtın önbelleğe alınmayacağını ifade eder.
        //Location = ResponseCacheLocation.None: Yanıtın hiçbir yerde (istemci veya proxy) önbelleğe alınmayacağını belirtir. NoStore = true: Yanıtın hiçbir şekilde önbelleğe alınmamasını zorunlu kılar.
        public IActionResult Error()//IActionResult bir interface(arayüz)'dir. "Error()" bir metot adıdır ve bu metot hata durumunda çağrılır.
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });// "return" bir metottan bir değer döndürmek için kullanılan bir keyword'dür.
        }// "View" bir Razor View döndürmek için kullanılan bir metottur. "new" yeni bir nesne oluşturmak için kullanılan bir keyword'dür."ErrorViewModel" hata bilgilerini taşımak için kullanılan bir sınıftır.
    }//"?." Null kontrolü yapan bir operatördür. Eğer Activity.Current null ise, Id özelliğine erişmeye çalışmaz.
//"Id" mevcut işlemin benzersiz kimliğini temsil eder."??"  null coalescing operatörüdür. Eğer Activity.Current?.Id null ise, HttpContext.TraceIdentifier değeri kullanılır.
}
