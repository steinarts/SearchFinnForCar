using Microsoft.Extensions.Hosting.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();

//ConfigureServices(builder.Services);

//void ConfigureServices(IServiceCollection services)
//{
//    services.AddControllers();
//    services.AddDirectoryBrowser();
//    services.AddSingleton<IWebHostEnvironment>(env => new HostingEnvironment
//    {
//        ContentRootPath = Directory.GetCurrentDirectory(),
//        WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
//        EnvironmentName = "Development"
//    });
//}
