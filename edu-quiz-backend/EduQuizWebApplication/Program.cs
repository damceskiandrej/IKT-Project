using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EduQuiz.DomainEntities.Identity;
using EduQuiz.Repository;
using EduQuiz.Repository.Implementation;
using EduQuiz.Repository.Interface;
using EduQuiz.Service.Implementation;
using EduQuiz.Service.Interface;
using EduQuizWebApplication.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using EduQuiz.DomainEntities.Roles;
using EduQuiz.Service.BackgroundWorker;

var builder = WebApplication.CreateBuilder(args);

//secrets
builder.Configuration.AddJsonFile("openrouterconfig.json", optional: true, reloadOnChange: true); 
builder.Configuration.AddEnvironmentVariables(); 

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<EduQuizUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
;
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()    ;
                      });
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IQuizRepository), typeof(QuizRepository));
builder.Services.AddScoped(typeof(IReccomendationRepository), typeof(ReccomendationRepository));
builder.Services.AddScoped<IResultRepository, ResultRepository>();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IExportService, ExportService>();
builder.Services.AddTransient<IReccomendationService, ReccomendationService>();
builder.Services.AddTransient<IImportService, ImportService>();
builder.Services.AddTransient<IQuizService, QuizService>();
builder.Services.AddTransient<IResultService, ResultService>();  
builder.Services.AddHttpClient<IAIService, AIService>();
builder.Services.AddScoped<IAIService, AIService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IQuizProcessingQueue, QuizProcessingQueue>();
builder.Services.AddHostedService<QuizProcessingService>();

static async Task SeedRolesAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    foreach (var role in Enum.GetNames(typeof(EduQuizRole)))
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

builder.Services.AddHttpClient<AIService>(client =>
{
    client.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
    client.Timeout = TimeSpan.FromMinutes(2);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAsync(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors(MyAllowSpecificOrigins);



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
