WebApiPolizasRabbit

Aplicación .NET 8 Web API para gestión de pólizas de seguros, conectada a una base de datos en Azure SQL Database y enviando mensajes a una cola RabbitMQ.
El proyecto está dockerizado para facilitar su despliegue y pruebas locales.

🚀 Tecnologías usadas
.NET 8

SQL Server (Azure SQL Database)

RabbitMQ

Docker

Entity Framework Core

Swagger

📂 Estructura del proyecto
Controllers: Lógica de API (CRUD de pólizas, envío de mensajes a RabbitMQ).

Models: Modelos de base de datos y DTOs.

Services: Servicio RabbitMQProducer para publicar en la cola.

📦 Instalación y ejecución local (Docker)
Clonar el repositorio

bash
Copy
Edit
git clone https://github.com/tuusuario/WebApiPolizasRabbit.git
cd WebApiPolizasRabbit
Crear la carpeta para llaves de protección

bash
Copy
Edit
mkdir dataProtectionKeys
Compilar y construir la imagen Docker


docker build -t webapi-polizas-rabbit .
Levantar contenedor de RabbitMQ (si no lo tienes ya corriendo)


docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
La consola de administración estará disponible en: http://localhost:15672

Usuario: guest

Contraseña: guest

Levantar contenedor de la Web API


docker run -d -p 8083:8080 -v $(pwd)/dataProtectionKeys:/root/.aspnet/DataProtection-Keys --name webapi-polizas-rabbit webapi-polizas-rabbit
🔥 Importante: El parámetro -v ./dataProtectionKeys:/root/.aspnet/DataProtection-Keys asegura persistencia de claves de protección de datos.

📄 Endpoints disponibles
Consultar pólizas
bash
Copy
Edit
GET http://localhost:8083/api/Poliza/Lista
Buscar pólizas por filtros
bash
Copy
Edit
GET http://localhost:8083/api/Poliza/Buscar?numeroPoliza=ABSH001
Insertar nueva póliza
bash
Copy
Edit
POST http://localhost:8083/api/Poliza/Nuevo
Content-Type: application/json
Body ejemplo:

json
Copy
Edit
{
  "numeroPoliza": "ABSH001",
  "tipoPolizaId": 1,
  "cedulaAsegurado": "01-8526-4930",
  "montoAsegurado": 250000,
  "fechaVencimiento": "2025-12-31T00:00:00",
  "fechaEmision": "2024-01-01T00:00:00",
  "coberturaId": 1,
  "estadoPolizaId": 1,
  "prima": 1500,
  "periodo": "2025-12-31T00:00:00",
  "fechaInclusion": "2024-01-01T00:00:00",
  "aseguradoraId": 1
}
Enviar póliza a RabbitMQ
bash
Copy
Edit
POST http://localhost:8083/api/Poliza/send
Content-Type: application/json
Body igual al anterior.
Este endpoint enviará el JSON a la cola polizaQueue en RabbitMQ.

📋 Notas importantes
El API utiliza una conexión definida en appsettings.json para conectarse a Azure SQL:

json
Copy
Edit
"ConnectionStrings": {
  "ConnectSQL": "Server=tcp:polizasserverhj2025.database.windows.net,1433;Initial Catalog=DBPolizas;Persist Security Info=False;User ID=admin;UserID=Tupasswrod!Polizas;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
RabbitMQ debe estar accesible usando el hostname rabbitmq (por ser otro contenedor).

Se configuró CORS para permitir cualquier origen durante pruebas locales (AllowAnyOrigin()).

La WebAPI usa Swagger, accede a la documentación en:


http://localhost:8083/swagger
🛠 Comandos útiles
Ver contenedores activos:


docker ps
Ver logs del contenedor de la API:


docker logs webapi-polizas-rabbit
Detener y eliminar contenedor:


docker rm -f webapi-polizas-rabbit
Recompilar la imagen si hay cambios:


docker build -t webapi-polizas-rabbit .
🙌 Autor
Desarrollado por Henderson J. Castañeda
LinkedIn | GitHub








