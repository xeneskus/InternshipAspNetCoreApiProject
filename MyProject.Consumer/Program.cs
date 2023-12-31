using MassTransit;
using MyProject.Consumer;
using MyProject.Consumer.Configurations;
using MyProject.Consumer.Data;
using System.Text.Json;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // MassTransit yapılandırması
        services.AddMassTransit(x =>
        {
            // RabbitMQ ayarlarını alıyoruz
            var rabbitMQSettings = hostContext.Configuration.GetSection("RabbitMQSettings").Get<RabbitMQSettings>();
            x.AddConsumer<Consumer>();
            // RabbitMQya bağlantı sağlıyoruz
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMQSettings.HostName), h =>
                {
                    h.Username(rabbitMQSettings.UserName);
                    h.Password(rabbitMQSettings.Password);

                });

                // Endpoint yapılandırması
                //cfg.ConfigureEndpoints(context);

                cfg.ReceiveEndpoint(rabbitMQSettings.QueueName, e =>
                {
                    e.Consumer<Consumer>(context);
                });
            });
        });


        //services.AddScoped<IMongoDatabase>(sp =>
        //{
        //    var connectionString = hostContext.Configuration.GetConnectionString("mongodb://localhost:27017");
        //    var mongoClient = new MongoClient(connectionString);
        //    return mongoClient.GetDatabase("logsDb");
        //    //return mongoClient.GetDatabase(MongoDBSettings);
        //});

        services.Configure<MongoDbSettings>(hostContext.Configuration.GetSection("MongoDBSettings"));
        //services.AddScoped<IMongoDatabase>(sp =>
        //{
        //    var options = sp.GetRequiredService<IOptions<MongoDbSettings>>();
        //    var settings = options.Value;
        //    var mongoClient = new MongoClient(settings.ConnectionString);
        //    return mongoClient.GetDatabase(settings.DatabaseName);
        //});

        services.AddScoped<ILogMongoDbDal, LogMongoDbDal>();




    })
    .Build();

await host.RunAsync();
