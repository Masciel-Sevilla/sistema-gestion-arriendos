
using ArriendosApi.Context;
using Microsoft.EntityFrameworkCore;
using ArriendosApi.Middlewares;
using ArriendosApi.Services;

namespace ArriendosApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var allowedOrigins = builder.Configuration.GetSection("AllowedCorsOrigins").Get<string[]>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReglasCors", policy =>
                {
                    if (allowedOrigins != null && allowedOrigins.Length > 0)
                    {
                        policy.WithOrigins(allowedOrigins) 
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    }
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            
            builder.Services.AddDbContext<AppDBContext>(options=>
            options.UseNpgsql(builder.Configuration.GetConnectionString("CadenaConexion")));

            builder.Services.AddAutoMapper(typeof(ArriendosApi.Mappings.MappingProfile));
            builder.Services.AddScoped<IEdificioService, EdificioService>();
            builder.Services.AddScoped<IInquilinoService, InquilinoService>();
            builder.Services.AddScoped<IInmuebleService, InmuebleService>();
            builder.Services.AddScoped<IContratoService,ContratoService>();

            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("ReglasCors");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
