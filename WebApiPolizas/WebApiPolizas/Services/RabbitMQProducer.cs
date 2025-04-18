using RabbitMQ.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using WebApiPolizas.Models;

public class RabbitMQProducer
{
    private readonly string _hostname = "rabbitmq";  // Cambia esto si usas otro host
    private readonly string _queueName = "polizaQueue"; // Nombre de la cola

    public void EnviarMensaje(Poliza poliza)
    {
        var factory = new ConnectionFactory() { HostName = _hostname };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: _queueName,
                                 durable: true,  // Asegúrate de que la cola sea durable
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var jsonMessage = JsonConvert.SerializeObject(poliza);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            // Publicar mensaje en la cola
            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("Mensaje enviado: " + poliza.NumeroPoliza);
        }
    }
}
