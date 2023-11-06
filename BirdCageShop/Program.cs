//using BirdCageShopDbContext;
//using BirdCageShopInterface;
//using BirdCageShopInterface.IRepositories;
//using BirdCageShopInterface.IServices;
//using BirdCageShopReposiory;
//using BirdCageShopReposiory.Repositories;
//using BirdCageShopService;
//using BirdCageShopService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
//using BirdCageShopOther.Mapper;
//using BirdCageShopInterface.IValidator;
//using BirdCageShopOther.Validator;
//using BirdCageShopViewModel.Role;
using BirdCageShopDbContext.Models;
using Microsoft.AspNetCore.Identity;
using BirdCageShopViewModel.User;
using BirdCageShopViewModel.Voucher;
using BirdCageShopViewModel.Category;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using BirdCageShopViewModel.Order;
using Stripe;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BirdCageShopOther.Email;
using BirdCageShopInterface.IServices;
using BirdCageShopService.Service;
using BirdCageShopInterface.IRepositories;
using BirdCageShopReposiory.Repositories;
using BirdCageShopInterface;
using BirdCageShopInterface.IValidator;
using BirdCageShopOther.Validator;
using BirdCageShopViewModel.Role;
using BirdCageShopReposiory;
using BirdCageShopOther.Mapper;

var builder = WebApplication.CreateBuilder(args);

//LocalDB
builder.Services.AddDbContext<BirdCageShopContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<BirdCageShopContext>().AddDefaultTokenProviders();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BirdCageShopContext>().AddDefaultTokenProviders();
    


//Add services to the container.
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.AllowAnyOrigin() 
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//configure author jwt in swagger
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Bird Cage Shop", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});








// add configure require email
//Add Config for Required Email
//builder.Services.Configure<IdentityOptions>(
//opts => opts.SignIn.RequireConfirmedEmail = true
//);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

//Add Email Configs

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddSingleton<ITimeService, TimeService>();

// Repo

//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
builder.Services.AddScoped<IBirdCageTypeRepository, BirdCageTypeRepository>();
builder.Services.AddScoped<IProductSpecificationsRepository, ProductSpecificationsRepository>();
builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<ISpecificationRepository, SpecificationRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();


//// Service 
////builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVourcherService, VourcherService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, BirdCageShopService.Service.ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IProductReviewService, ProductReviewService>();



builder.Services.AddScoped<IClaimService, ClaimService>();


//
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

// Validator
builder.Services.AddScoped<IRoleValidator, RoleValidator>();
builder.Services.AddScoped<RoleAddRule>();

builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<UserSignUpRule>();
builder.Services.AddScoped<UserChangePasswordRule>();
builder.Services.AddScoped<UpdateProfileRule>();
builder.Services.AddScoped<ResetPasswordRule>();

builder.Services.AddScoped<IVoucherValidator, VoucherValidator>();
builder.Services.AddScoped<VourcherAddRule>();

builder.Services.AddScoped<ICategoryValidator, CategoryValidator>();
builder.Services.AddScoped<CategoryCreateRule>();
builder.Services.AddScoped<IConfirmOrderValidator, ConfirmOrderValidator>();
builder.Services.AddScoped<ConfirmOrderAddRule>();

builder.Services.AddHttpContextAccessor();
//

//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
