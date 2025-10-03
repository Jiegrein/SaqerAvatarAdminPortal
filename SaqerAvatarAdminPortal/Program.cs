using SaqerAvatarAdminPortal.Services;
using SaqerAvatarAdminPortal.Services.Interfaces;
using SaqerAvatarAdminPortal.Models.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add controllers for API endpoints
builder.Services.AddControllers();

// Configure dashboard settings
builder.Services.Configure<DashboardSettings>(
    builder.Configuration.GetSection(DashboardSettings.SectionName));

// Register custom services
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IChatService, ChatService>();

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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // Enable API controllers

app.Run();
