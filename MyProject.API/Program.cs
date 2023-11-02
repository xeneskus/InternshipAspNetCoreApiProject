using MassTransit;
using Microsoft.EntityFrameworkCore;
using MyProject.Cache;
using MyProject.Consumer.Configurations;
using MyProject.Data;
using MyProject.Data.Repositories.Abstract;
using MyProject.Data.Repositories.Concrete;
using MyProject.Data.UnitOfWorks;
using MyProject.Service;
using MyProject.Service.Mapping;
using MyProject.Service.Services.Abstract;
using MyProject.Service.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<ICityRepository>(sp =>
{
    var appDbContext = sp.GetRequiredService<AppDbContext>();
    var cityRepository = new CityRepository(appDbContext);
    var redisService = sp.GetRequiredService<RedisService>();
    return new CityServiceWithCacheDecorator(cityRepository, redisService);
});



builder.Services.AddMassTransit(x =>
{
     var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri(rabbitMQSettings.HostName), h =>
        {
            h.Username(rabbitMQSettings.UserName);
            h.Password(rabbitMQSettings.Password);

        });

    });
});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityService, CityService>();



builder.Services.AddAutoMapper(typeof(DtoMapper));




builder.Services.AddSingleton<RedisService>(sp =>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));

});

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new()
//    {


//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Appsettings:SecurityKey")),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//    };
//});


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
