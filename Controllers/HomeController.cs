using System.Diagnostics; //"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtirmek 
using Microsoft.AspNetCore.Mvc; // için kütüphane adının başına ekleriz "." ise bir operatördür burada system kütüphanesinin diagnostics ksımında olduğunu belirtir.
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
        {
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

                    new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index");
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
            return View();//Return keyword'ü bir metottan bir değer döndürmek için kullanılır.View() bir metot ismidir ve () operatörü, bir metodu çağırmak için kullanılır.
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult AddTodo([FromBody] Todo todo) 
        {
            try
            {
                if (!string.IsNullOrEmpty(todo.Title))
                {
                    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                    
                    var newTodo = new Todo 
                    {
                        Title = todo.Title,
                        Description = todo.Description,
                        IsCompleted = false,
                        CreatedDate = DateTime.Now,
                        UserId = userId
                    };

                    _context.Todos.Add(newTodo);
                    _context.SaveChanges();
                    
                    return Json(new { success = true, todo = newTodo });
                }
                return Json(new { success = false, message = "Başlık boş olamaz" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görev eklenirken hata oluştu"); 
                return Json(new { success = false, message = "Bir hata oluştu" });
            }
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult ToggleTodo(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);
            
            if (todo != null)
            {
                todo.IsCompleted = !todo.IsCompleted;
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Görev bulunamadı" });
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult DeleteTodo([FromBody] int id) 
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                
                if (todo != null)
                {
                    _context.Todos.Remove(todo);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Görev bulunamadı" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görev silinirken hata oluştu");
                return Json(new { success = false, message = "Bir hata oluştu" });
            }
        }

        [HttpPost]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        [Authorize]//[]  Bir attribute'u tanımlamak için kullanılır. Attribute'lar bir sınıf, metot veya başka bir öğe hakkında ek bilgi sağlar.
        public IActionResult CompleteTodo([FromBody] int id) 
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);
                
                if (todo != null)
                {
                    _context.Todos.Remove(todo);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Görev bulunamadı" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görev tamamlanırken hata oluştu");
                return Json(new { success = false, message = "Bir hata oluştu" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
