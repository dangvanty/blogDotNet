using Razor9_identity.Models;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using blogDotNet.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// add option -- them service sendmail 
builder.Services.AddOptions();
var mailSettingsUrl= builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailServiceSettings>(mailSettingsUrl);
builder.Services.AddSingleton<IEmailSender,SendMailServices>();

// Eject dang nhap bang google:
builder.Services.AddAuthentication()
        .AddGoogle(googleOptions=>{
            // doc thong tin authentication: google tu appsettings.json
            var googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
            googleOptions.ClientId= googleAuthSection["ClientID"];
            googleOptions.ClientSecret = googleAuthSection["Clientsecret"];

            // Cau hinh Url callback lai tu google(khong thiet lap thi ma dinh la signin-google)
            googleOptions.CallbackPath = "/dang-nhap-tu-google";
        });

// Add services to the container.
builder.Services.AddRazorPages();
var connectString = builder.Configuration.GetSection("ConnectionStrings")["MyBlogContext"];
builder.Services.AddDbContext<MyBlogContext>(options => options.UseMySQL(connectString)); 



// đăng ký identity   
builder.Services.AddIdentity<AppUser,IdentityRole>()
        .AddEntityFrameworkStores<MyBlogContext>()
        .AddDefaultTokenProviders(); // Thêm Token Provider - nó sử dụng để phát sinh token (reset password, confirm email ...)
// cái dưới sử dụng identity ui
// builder.Services.AddDefaultIdentity<AppUser>()
//         .AddEntityFrameworkStores<MyBlogContext>()
//         .AddDefaultTokenProviders();


// Truy cập IdentityOptions
builder.Services.Configure<IdentityOptions> (options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
    options.SignIn.RequireConfirmedAccount=true;

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
