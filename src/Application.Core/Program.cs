using Application.Core;
using Infra.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

string bookConnectionString = builder.Configuration.GetConnectionString("booksConnection");
ConfigureContexts.Configure(builder.Services, bookConnectionString);

ConfigureRepositories.Configure(builder.Services);
ConfigureSwagger.Configure(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
