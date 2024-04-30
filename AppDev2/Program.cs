using AppDev2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AppDev2.Utility;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ vào container
builder.Services.AddControllersWithViews();

// Cấu hình cơ sở dữ liệu
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Cấu hình Identity với `IdentityUser` và `ApplicationDbContext`
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm Razor Pages
builder.Services.AddRazorPages();

// Cấu hình chính sách ủy quyền (`Authorization`)
builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => policy.RequireRole(SD.Admin));
    options.AddPolicy("Customer", policy => policy.RequireRole(SD.Customer));
});

// Cấu hình cookie ứng dụng
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// Cấu hình pipeline cho HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Bật HSTS trong môi trường sản xuất
}

app.UseHttpsRedirection(); // Chuyển hướng HTTP sang HTTPS
app.UseStaticFiles(); // Sử dụng tệp tĩnh

app.UseRouting(); // Sử dụng định tuyến

// Cấu hình xác thực và ủy quyền
app.UseAuthentication(); // Sử dụng xác thực
app.UseAuthorization(); // Sử dụng ủy quyền

// Định tuyến cho Razor Pages
app.MapRazorPages();

// Định tuyến mặc định cho ứng dụng
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Chạy ứng dụng
