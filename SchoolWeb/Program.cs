using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using StoreData;
using StoreData.Models;
using StoreData.Repostiroties;
using StoreData.Repostiroties.School;
using WebStoryFroEveryting.Hubs;
using WebStoryFroEveryting.Middlewares;
using WebStoryFroEveryting.Reflections;
using WebStoryFroEveryting.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(SchoolAuthConstans.AUTH_TYPE)
    .AddCookie(SchoolAuthConstans.AUTH_TYPE, config =>
    {
        config.LoginPath = "/SchoolAuth/Login";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services
    .AddDbContext<SchoolDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(SchoolDbContext))));

// var reflectionRepositories = new ReflectionRepositories();
// reflectionRepositories.AddReflectionRepositories(builder.Services);


builder.Services.AddScoped<ISchoolRoleRepository, SchoolRoleRepository>();


builder.Services.AddScoped<ISchoolUserRepository, SchoolUserRepository>();

builder.Services.AddScoped<ISchoolAuthService, SchoolAuthService>();
builder.Services.AddScoped<IDataToViewModelMapper, DataToViewModelMapper>();
builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<IBannedUserRepository, BannedUserRepository>();
builder.Services.AddScoped<IBanWordRepository, BanWordRepository>();
builder.Services.AddScoped<ILessonCommentRepository, LessonCommentRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapHub<ChatHub>("/hub/chat");
app.MapHub<LessonHub>("/hub/lesson");

app.UseMiddleware<LocalizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lessons}/{action=Index}/{id?}");

app.Run();
