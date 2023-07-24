using System.Text.Json;
using Reservation.Api.Filters;
using Reservation.Core.Repositories.Abstract;
using Reservation.Core.Repositories.Concrete;
using Reservation.Core.Services.Abstract;
using Reservation.Core.Services.Base.Abstract;
using Reservation.Core.Services.Base.Concrete;
using Reservation.Core.Services.Concrete;
using Reservation.Domain.Models.ConfigurationModels.Abstract;
using Reservation.Domain.Models.ConfigurationModels.Concrete;
using Reservation.Infrastructure.Email;
using Reservation.Infrastructure.Email.Abstract;
using Reservation.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.Filters.Add(new DbContextTransactionFilter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

#region Db Context DI Configuration
builder.Services.AddDbContext<ReservationDbContext>();
#endregion

#region Services And Repositories DI Configuration
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IBaseService, BaseService>();
#endregion

#region Email Configuration Information Model DI Configuration

builder.Services.AddSingleton<IEmailConfigurationInformationModel, EmailConfigurationInformationModel>(option => builder.Configuration.GetSection("EmailInformation").Get<EmailConfigurationInformationModel>());

builder.Services.AddSingleton<IEmailSender, EmailSender>();
#endregion

var app = builder.Build();

#region Exception Handler Zone
app.UseExceptionHandler(option => option.Run(async context =>
{
    var content = JsonSerializer.Serialize(
        new
        {
            IsOk = false,
            Message = "Ýþlem Sýrasýnda Teknik Sorunlar Oluþmuþtur."
        });
    context.Response.ContentType = "application/json; charset=utf-8";
    await context.Response.WriteAsync(content);
}));
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
