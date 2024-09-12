using Microsoft.AspNetCore.Identity;
using RickMortyApplication.IRepository;
using RickMortyApplication.IServices;
using RickMortyDomain.Entities;
using RickMortyPersistence;
using RickMortyPersistence.Repository;
using RickMortyPersistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<RickMortyDbContext>();
builder.Services.AddIdentity<AppUser,IdentityRole<int>>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<RickMortyDbContext>();

#region Ioc Ýstekleri
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ICharacterEpisodeRepository, CharacterEpisodeRepository>();
builder.Services.AddScoped<ICharacterEpisodeService, CharacterEpisodeService>();

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
