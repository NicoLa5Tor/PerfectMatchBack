using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Models.Common;
using PerfectMatchBack.Services.Contract;
using PerfectMatchBack.Services.Implementation;
using PerfectMatchBack.Utilitles;
using System.Reflection.Metadata.Ecma335;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region services
builder.Services.AddDbContext<PerfectMatchContext>(option => option.UseSqlServer
(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccessService, AccessService>();
builder.Services.AddScoped<IAnimalTypeService,AnimalTypeService>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<ICityService, CityService>();    
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRoleService, RoleService>();    
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    });
}
);
#region JWT
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();

var key = Encoding.ASCII.GetBytes(appSettings.secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("NuevaPolitica");
app.Run();
