using AssetManagementTeam6.API.Dtos.Requests;
using AssetManagementTeam6.API.Extensions;
using AssetManagementTeam6.API.Heplers;
using AssetManagementTeam6.API.Services.Implements;
using AssetManagementTeam6.API.Services.Interfaces;
using AssetManagementTeam6.API.Validation;
using AssetManagementTeam6.Data;
using AssetManagementTeam6.Data.Repositories.Implements;
using AssetManagementTeam6.Data.Repositories.Interfaces;
using Common.Constants;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Add cors

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://nashtech-rookies-hn06-gr06-api.azurewebsites.net/api",
                            "http://localhost:3000");
    });
});

// Add services to the container.

builder.Services.AddControllers();


// Add config
var configuration = builder.Configuration;
builder.Services.AddDbContext<AssetManagementContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
// End add config

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAssetService, AssetService>();
builder.Services.AddTransient<IAssetRepository, AssetRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddTransient<IAssignmentService, AssignmentService>();
builder.Services.AddTransient<IRequestForReturningRepository, RequestForReturningRepository>();
builder.Services.AddTransient<IRequestForReturningService, RequestForReturningService>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IUserProvider, UserProvider>();
builder.Services.AddTransient<IRemoveService, RemoveService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator<UserRequest>, UserValidator>();
builder.Services.AddScoped<IValidator<ChangePasswordRequest>, ChangePasswordRequestValidator>();
builder.Services.AddScoped<IValidator<CategoryRequest>, CategoryRequestValidatior>();
builder.Services.AddScoped<IValidator<AssetRequest>, AssetRequestValidator>();
builder.Services.AddScoped<IValidator<AssignmentRequest>, AssignmentRequestValidator>();
builder.Services.AddFluentValidation();

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
 options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidIssuer = JwtConstant.Issuer,
         ValidAudience = JwtConstant.Audience,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstant.Key))
     };
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler(); 

app.Run();
