using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoMvcApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TodoMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var todos = _context.Todos.Where(t => t.UserId == userId).ToList();
            return View(todos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
