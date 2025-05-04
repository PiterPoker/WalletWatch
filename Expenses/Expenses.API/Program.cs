using Expenses.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCustomDbContext(builder.Configuration);
builder.Services.AddCustomRepositories();
builder.Services.AddCustomServices();
builder.Services.AddCustomFactories();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomMapping();
builder.Services.AddCustomMassTransit();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expenses API v1");
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
