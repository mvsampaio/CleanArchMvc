using CleanArchMvc.Infra.IoC;

namespace CleanArchMvc.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.            
            builder.Services.AddInfraStructureAPI(builder.Configuration);                        
            builder.Services.AddInfraStructureJWT(builder.Configuration);
            builder.Services.AddInfraStructureSwagger();

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
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseAuthorization();            

            app.MapControllers();

            app.Run();
        }
    }
}