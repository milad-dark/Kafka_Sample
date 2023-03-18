using Confluent.Kafka;
using Kafka.Interfaces;
using Kafka.Producer;

namespace Kafka_ProducerApplication
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Kafka Producer API", Version = "v1" });
                c.EnableAnnotations();
            });

            ConfigurationManager configuration = builder.Configuration;
            var producerConfig = new ProducerConfig(new ClientConfig
            {
                BootstrapServers = configuration["Kafka:ClientConfigs:BootstrapServers"]
            });

            builder.Services.AddSingleton(producerConfig);
            builder.Services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Kafka Producer API");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}