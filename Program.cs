using Microsoft.Extensions.Options;
using policystore.Configurations;
using policystore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
//                builder =>
//                {
//                    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
//                }));

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddSingleton<IMongoDBSettings>(options => options.GetRequiredService<IOptions<MongoDBSettings>>().Value);
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

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
