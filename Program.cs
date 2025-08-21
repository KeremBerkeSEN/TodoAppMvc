using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;//"using" kalıbı bir keyword'tür aşağıda kullanacağımız bazı kelimelerin hangi kütüphanelerden geleceğini belirtmek 
// için kütüphane adının başına ekleriz "." ise bir operatördür ve amacı bir nesnenin özelliklerine, metodlarına veya alt üyelerine erişmek için kullanılır.
//"." operatörü buralarda using keywordü ile kullanılacak metotların vs Microsoft namespace'inin AspNetCore alt namespace'inin Authentication alt namespace'inin Cookies namespace'inde olduğunu belirtir.
//veya kullanılacak metotların vs TodoMvcApp namespace'inin Models alt namespace'inde olduğunu belirtir.
using TodoMvcApp.Models;
using TodoMvcApp.Data;

var builder = WebApplication.CreateBuilder(args);//"var" C# dilinde bir anahtar kelimedir ve değişkenin türünü otomatik olarak belirler. Amaç: builder değişkeninin türünü açıkça belirtmeden, derleyicinin bunu otomatik olarak belirlemesini sağlar.
//"WebApplication.CreateBuilder" ASP.NET Core'da bir web uygulaması oluşturmak için kullanılan bir metottur. "args"Programın komut satırı argümanlarını temsil eder. Bu argümanlar, uygulamanın yapılandırmasını etkileyebilir.
builder.Services.AddControllersWithViews();//"builder.Services" ASP.NET Core'da, uygulamaya hizmetler (services) eklemek için kullanılan bir koleksiyondur. Amaç: Uygulamanın ihtiyaç duyduğu bağımlılıkları (örneğin, MVC, veritabanı bağlantısı, kimlik doğrulama) yapılandırmak ve eklemek.

builder.Services.AddDbContext<AppDbContext>(options =>//"builder.Services" ASP.NET Core'da, uygulamaya hizmetler (services) eklemek için kullanılan bir koleksiyondur.
//"AddDbContext<AppDbContext>" Entity Framework Core'da, bir DbContext sınıfını uygulamaya hizmet olarak eklemek için kullanılan bir metottur.
//"AppDbContext" Uygulamanın veritabanı işlemlerini yöneten DbContext sınıfıdır.Amaç: AppDbContext sınıfını uygulamaya bağımlılık olarak eklemek ve yapılandırmak.
//options =>: Bir lambda ifadesidir ve options parametresi üzerinden tablo yapılandırmasını sağlar.
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//"builder.Configuration" ASP.NET Core'da, uygulamanın yapılandırma ayarlarını temsil eden bir nesnedir. Amaç: Uygulamanın yapılandırma dosyalarına (örneğin, appsettings.json) veya çevresel değişkenlere erişmek.
                                                                                          //"GetConnectionString" Configuration nesnesinde, ConnectionStrings bölümünden belirli bir bağlantı dizesini almak için kullanılan bir metottur. "DefaultConnection": appsettings.json dosyasındaki ConnectionStrings bölümünde tanımlanan bir anahtardır. Amaç: Veritabanına bağlanmak için gerekli olan bağlantı dizesini almak.
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));//"options" AddDbContext metodu içinde kullanılan bir parametredir.Amaç: Veritabanı bağlantısı ve yapılandırma ayarlarını tanımlamak için kullanılır.
                                                                                   //"UseMySql" Entity Framework Core'da, MySQL veritabanını kullanmak için kullanılan bir metottur.connectionString: "connectionString"Veritabanına bağlanmak için kullanılan bağlantı dizesidir.
                                                                                   //"ServerVersion.AutoDetect" Bağlantı dizesine göre MySQL sunucu sürümünü otomatik olarak algılar. Amaç: MySQL sunucusunun sürümüne uygun ayarların otomatik olarak yapılandırılmasını sağlamak.
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//AddAuthentication: ASP.NET Core'da kimlik doğrulama mekanizmasını yapılandırmak için kullanılan bir metottur. "CookieAuthenticationDefaults.AuthenticationScheme" Kimlik doğrulama için kullanılacak şemayı belirtir. Bu durumda, çerez tabanlı kimlik doğrulama (cookie-based authentication) kullanılmaktadır.
//Amaç: Uygulamaya kimlik doğrulama mekanizmasını eklemek ve yapılandırmak.
    .AddCookie(options =>//AddCookie: Çerez tabanlı kimlik doğrulama için kullanılan bir metottur.
    {
        options.LoginPath = "/Home/Login";//"options.LoginPath" Kullanıcı kimlik doğrulaması gerektiğinde yönlendirilecek giriş (login) sayfasının yolunu belirtir.
        //"/Home/Login": Giriş sayfasının yolu. Kullanıcı kimlik doğrulaması yapılmadığında bu sayfaya yönlendirilir.
        options.LogoutPath = "/Home/Logout";//"options.LogoutPath" Kullanıcı çıkış yaptığında yönlendirilecek çıkış (logout) sayfasının yolunu belirtir.
        //"/Home/Logout": Çıkış sayfasının yolu.
    });

var app = builder.Build();//"var" C# dilinde bir anahtar kelimedir ve değişkenin türünü otomatik olarak belirler. Amaç: builder değişkeninin türünü açıkça belirtmeden, derleyicinin bunu otomatik olarak belirlemesini sağlar.
//"builder.Build()" ASP.NET Core'da, WebApplicationBuilder nesnesini kullanarak bir WebApplication nesnesi oluşturur.
using (var scope = app.Services.CreateScope())//app.Services.CreateScope(): ASP.NET Core'da, bağımlılık enjeksiyonu (Dependency Injection) için bir kapsam (scope) oluşturur.
{//using: Kapsamın (scope) otomatik olarak temizlenmesini sağlar.
    var services = scope.ServiceProvider;//"var" C# dilinde bir anahtar kelimedir ve değişkenin türünü otomatik olarak belirler. Amaç: builder değişkeninin türünü açıkça belirtmeden, derleyicinin bunu otomatik olarak belirlemesini sağlar.
    //scope.ServiceProvider: Kapsam içinde tanımlanan hizmetlere erişmek için kullanılan bir nesnedir. Amaç: Bağımlılık enjeksiyonu ile tanımlanan hizmetlere erişmek.
    try//"try" hata yönetimi için kullanılan bir keyword'tür. "try" bloğu içinde hata oluşabilecek kodlar yazılır. Eğer bir hata oluşursa catch bloğu çalıştırılır.
    {
        var context = services.GetRequiredService<AppDbContext>();//GetRequiredService<T>(): Bağımlılık enjeksiyonu (Dependency Injection) ile tanımlanan bir hizmetin örneğini alır. AppDbContext: Veritabanı işlemlerini yöneten DbContext sınıfıdır.
        //Amaç: Veritabanı işlemleri için AppDbContext örneğini almak.
        //Not: Eğer AppDbContext hizmeti tanımlanmamışsa veya bulunamazsa, bu metot bir hata (exception) fırlatır.
        context.Database.Migrate();//context: AppDbContext sınıfının bir örneğidir. Amaç: Veritabanı işlemlerini gerçekleştirmek için kullanılan bir DbContext nesnesidir.
        //Bu örnek, services.GetRequiredService<AppDbContext>() ile bağımlılık enjeksiyonu (Dependency Injection) yoluyla alınmıştır.
        var seeder = new DatabaseSeeder(context);//DatabaseSeeder: Veritabanını başlangıç verileriyle doldurmak için kullanılan bir sınıftır.
        //context: AppDbContext sınıfının bir örneğidir ve veritabanı işlemlerini gerçekleştirmek için kullanılır.
        //Amaç: DatabaseSeeder sınıfından bir nesne oluşturmak ve bu nesneye AppDbContext örneğini iletmek.
        seeder.Seed();//seeder: DatabaseSeeder sınıfından oluşturulmuş bir nesnedir. Amaç: Veritabanını başlangıç verileriyle doldurmak için kullanılır.
        //Seed(): DatabaseSeeder sınıfında tanımlı bir metottur.
        //Amaç: Veritabanına başlangıç verilerini eklemek.
        //Bu metot, genellikle veritabanında eksik olan verileri kontrol eder ve gerekli verileri ekler.
    }
    catch (Exception ex)//"catch" "try" bloğunda bir hata oluşursa çalıştırılır. "Exeption" oluşan hatayı temsil eden bir sınıftır.
    {//"ex" hata nesnesinin adıdır.
        {
            var logger = services.GetRequiredService<ILogger<Program>>();//services: app.Services.CreateScope() ile oluşturulan kapsamın (scope) hizmet sağlayıcısını temsil eder.
            //GetRequiredService<T>(): Bağımlılık enjeksiyonu (Dependency Injection) ile tanımlanan bir hizmetin örneğini alır.
            //ILogger<Program>: ASP.NET Core'da loglama işlemleri için kullanılan bir arayüzdür. Program sınıfına özgü loglama işlemleri için kullanılır.
            //Amaç: Loglama işlemleri için bir ILogger örneği almak.
            //Not: Eğer ILogger<Program> hizmeti tanımlanmamışsa veya bulunamazsa, bu metot bir hata (exception) fırlatır.
            logger.LogError(ex, "Veritabanı başlatılırken hata oluştu.");//"_logger" bir nesnedir ve LogError metodu, hata mesajını kaydetmek için kullanılır. "ex" catch bloğunda yakalanan hata nesnesidir.
                                                                         //"Veritabanı başlatılırken hata oluştu." loglama sırasında kullanılacak açıklayıcı bir mesajdır. Bu mesaj, log kaydında hata ile ilgili bağlam sağlamak için kullanılır
        }
    }

    if (!app.Environment.IsDevelopment())//app.Environment: ASP.NET Core'da, uygulamanın çalışma ortamını (environment) temsil eden bir özelliktir.
    //IsDevelopment(): Uygulamanın çalışma ortamının "Development" (geliştirme) ortamı olup olmadığını kontrol eden bir metottur.
    //!: Mantıksal "değil" operatörüdür. Eğer uygulama "Development" ortamında değilse, if bloğu çalıştırılır.
    //Amaç: Uygulama geliştirme ortamında değilse, hata yönetimi ve güvenlik özelliklerini etkinleştirmek.
    {
        app.UseExceptionHandler("/Home/Error");//UseExceptionHandler: ASP.NET Core'da, uygulama genelinde hata yönetimi için kullanılan bir middleware'dir.
        //"/Home/Error": Hata oluştuğunda kullanıcıların yönlendirileceği hata sayfasının yoludur.
        //Amaç: Hata durumunda kullanıcı dostu bir hata sayfası göstermek.
        app.UseHsts();//UseHsts: HTTP Strict Transport Security (HSTS) protokolünü etkinleştiren bir middleware'dir.
        //Amaç: Uygulamanın yalnızca HTTPS üzerinden erişilmesini sağlamak ve güvenliği artırmak.
        //Not: HSTS, yalnızca üretim (production) ortamında etkinleştirilmelidir.
    }

    app.UseHttpsRedirection();//UseHttpsRedirection: HTTP isteklerini otomatik olarak HTTPS'ye yönlendiren bir middleware'dir.
    app.UseStaticFiles();//Uygulamanın statik dosyaları (CSS, JavaScript, resimler vb.) sunmasını sağlayan bir middleware'dir.
    //Amaç: wwwroot dizinindeki statik dosyaları istemcilere sunmak.

    app.UseRouting(); //UseRouting: Uygulamanın yönlendirme (routing) işlemlerini etkinleştiren bir middleware'dir.
    //Amaç: Gelen HTTP isteklerini uygun bir rota ile eşleştirmek.

    app.UseAuthentication(); //UseAuthentication: Uygulamada kimlik doğrulama (authentication) işlemlerini etkinleştiren bir middleware'dir.
    //Amaç: Kullanıcıların kimlik doğrulama işlemlerini gerçekleştirmek.
    app.UseAuthorization(); //UseAuthorization: Uygulamada yetkilendirme (authorization) işlemlerini etkinleştiren bir middleware'dir.
    //Amaç: Kullanıcıların belirli kaynaklara erişim yetkilerini kontrol etmek.

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");//MapControllerRoute: Uygulamanın rotalarını (routes) tanımlamak için kullanılan bir metottur.
    //name: "default": Rotaya verilen isimdir.
    //pattern: "{controller=Home}/{action=Index}/{id?}": Rota şablonudur.
    //controller=Home: Varsayılan controller.
    //action=Index: Varsayılan action.
    //id?: Opsiyonel bir parametredir.

    app.Run();//Run: Uygulamayı çalıştıran bir metottur.
              //Amaç: Middleware işlem hattını sonlandırmak ve uygulamayı başlatmak.
}
