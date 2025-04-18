public Task StartAsync(CancellationToken cancellationToken)
{
    _logger.LogInformation("RabbitMQ Consumer Service is starting.");

    var consumer = new EventingBasicConsumer(_channel);

    consumer.Received += async (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        _logger.LogInformation($"Mensaje recibido: {message}");

        try
        {
            var poliza = JsonConvert.DeserializeObject<Poliza>(message);

            if (poliza == null)
            {
                _logger.LogError("Error: No se pudo deserializar la póliza. Mensaje vacío o mal formado.");
                return;
            }

            _dbContext.Polizas.Add(poliza);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Póliza guardada en BD: {poliza.NumeroPoliza}");

            // Confirmar al Rabbit que ya fue procesado (si cambiaras a autoAck: false)
            //_channel.BasicAck(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al procesar el mensaje: {ex}");
            // Si quieres manejar fallos, podrías rechazar el mensaje en Rabbit:
            //_channel.BasicNack(ea.DeliveryTag, false, true); // true = requeue
        }
    };

    _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
    return Task.CompletedTask;
}
