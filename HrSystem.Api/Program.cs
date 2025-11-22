using FluentValidation;
using HrSystem.Api.Middleware;
using HrSystem.Application.Common.Behaviors;
using HrSystem.Infrastructure.Persistence;
using MediatR;
using System.Reflection;

namespace HrSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Application (لو كنت تستخدم MediatR/AutoMapper)
            builder.Services.AddMediatR(Assembly.Load("HrSystem.Application"));
            builder.Services.AddAutoMapper(Assembly.Load("HrSystem.Application"));

            // FluentValidation (تجميع الفاليديتورز من طبقة Application)
            builder.Services.AddValidatorsFromAssembly(Assembly.Load("HrSystem.Application"));

            // لتفعيل ربط FluentValidation مع ASP.NET ModelState (اختياري لكنه مفيد)
            //builder.Services.AddFluentValidationAutoValidation();

            // Pipeline: أي Request يمر على ValidationBehavior
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // ✅ هنا يستدعي DependencyInjection.AddInfrastructure ويقرأ ConnectionStrings:Default
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseValidationExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
