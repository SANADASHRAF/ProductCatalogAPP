using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Extensions;
using Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureCors();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureIdentity();

builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
    dbContext.Database.Migrate();
    dbContext.SeedInitialData();
}

if (app.Environment.IsProduction() || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles();
}


app.UseRouting();
app.UseCors("CorsPolicy");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.MapControllers();

app.Run();
