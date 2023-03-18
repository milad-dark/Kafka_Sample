using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Interfaces;
using Kafka.Messages;
using Kafka.Producer;
using Kafka_ConsumerApplication.Core.kafkaEvents;

namespace Kafka_ConsumerApplication
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
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Kafka Consumer API", Version = "v1" });
                c.EnableAnnotations();
            });

            ConfigurationManager configuration = builder.Configuration;
            var clientConfig = new ClientConfig()
            {
                BootstrapServers = configuration["Kafka:ClientConfigs:BootstrapServers"]
            };

            var producerConfig = new ProducerConfig(clientConfig);
            var consumerConfig = new ConsumerConfig(clientConfig)
            {
                GroupId = "SourceApp",
                EnableAutoCommit = true,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000
            };

            builder.Services.AddSingleton(producerConfig);
            builder.Services.AddSingleton(consumerConfig);

            builder.Services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));

            builder.Services.AddScoped<IKafkaHandler<string, CreateCustomer>, RegisterCustomerHandler>();
            builder.Services.AddSingleton(typeof(IKafkaConsumer<,>), typeof(KafkaConsumer<,>));
            builder.Services.AddHostedService<RegisterCustomerConsumer>();

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