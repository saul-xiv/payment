using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
        //
        // c.SwaggerEndpoint("http://paymentservice:5000/swagger/v1/swagger.json", "Payment Service");
        // c.SwaggerEndpoint("http://loginservice:5002/swagger/v1/swagger.json", "Login Service");
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway V1");
        c.SwaggerEndpoint("/payment/swagger/v1/swagger.json", "Payment Service");
        c.SwaggerEndpoint("/login/swagger/v1/swagger.json", "Login Service");
        c.RoutePrefix = string.Empty;
    });
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run("http://0.0.0.0:5000");