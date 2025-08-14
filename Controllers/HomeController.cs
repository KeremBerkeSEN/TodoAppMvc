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
    //HomeController : Controller ise bu sınıfın diğer sınıflarlarla karışmaması için sadece HomeController'daki Controller sınıfının olduğunu belirtir
    {
        private readonly ILogger<HomeController> _logger;//burada readonly keyword'ü burada tanımlanan _logger ve _context değişkenlerinin
        private readonly AppDbContext _context;//aşağıda  _logger= logger şeklinde tanımlanmasına rağmen _logger değişkenine başka bir değer atanamaz
        //ILogger arayüzü HomeController sınıfı için bir kayıt tutucu görevi görür
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;//"=" operatörü burada logger değerini _logger değişkenine atama görevi görür
            _context = context;//"=" operatörü ile context değerini _context değişkenine atama
        }

        [Authorize]//kimlik doğrulama işlemi yapıldıktan sonra yetkilendirmek için kullanılır
        public IActionResult Index()//burada IActionResult bir arayüzdür ve bu arayüzü uygulayan sınıflar, HTTP isteklerine karşılık gelen sonuçları temsil eder
        {//Index() bir metottur.
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var todos = _context.Todos.Where(t => t.UserId == userId).ToList();
            return View(todos);
        }

        public IActionResult Privacy()
        {
            return View();//
        }

        [HttpGet]
        public IActionResult Login()//
        {
            return View();//
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
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
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
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
