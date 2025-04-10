# README - Despliegue de WebAPI .NET con Docker y Kubernetes + Azure SQL

Este README documenta el proceso completo para desplegar una Web API .NET Core en Docker, exponerla usando Kubernetes y conectarla a una base de datos Azure SQL.

---

## ✨ Tecnologías Usadas

- .NET 8 (Web API)
- Docker
- Kubernetes (local con Docker Desktop)
- Azure SQL Database
- MacOS (host con Parallels para Windows)

---

## 📚 Estructura del Proyecto

```
CRUD_MANT_POLIZAS/
├── WebApiPolizas/            # Proyecto WebAPI
│   ├── Controllers/
│   ├── Models/
│   ├── appsettings.json
│   ├── WebApiPolizas.csproj
│   └── Program.cs
├── Dockerfile                # Archivo para construir la imagen
├── k8s/
│   ├── deployment.yaml       # Despliegue K8s
│   └── service.yaml          # Servicio K8s
```

---

## ⚙️ Paso 1 - Crear el archivo Dockerfile

Ubicado en la raíz del proyecto:

```dockerfile
# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "WebApiPolizas.dll"]
```

---

## 🚀 Paso 2 - Construir y ejecutar con Docker

```bash
docker build -t webapi-polizas .

docker run -d -p 8080:8080 \
  -v "$(pwd)/appsettings.json:/app/appsettings.json" \
  webapi-polizas
```

**Nota:** Asegurate de que el puerto 8080 no esté ocupado.

Verificá:

```bash
curl http://localhost:8080/swagger/index.html
```

---

## 📁 Paso 3 - Configuración de appsettings.json para Azure SQL

```json
{
  "ConnectionStrings": {
    "ConnectSQL": "Server=tcp:polizasserverhj2025.database.windows.net,1433;Initial Catalog=DBPolizas;Persist Security Info=False;User ID=admin;Password=TuPassword123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Verificá conectividad desde Mac:

```bash
ping polizasserverhj2025.database.windows.net
```

Y usando `sqlcmd`:

```bash
sqlcmd -S tcp:polizasserverhj2025.database.windows.net,1433 \
  -U admihj -P 'TuPassword123!' -d DBPolizas -N -C
```

---

## 🚧 Paso 4 - Subir imagen a Docker Hub

```bash
docker tag webapi-polizas hjclabsdocker/webapi-polizas:latest
docker push hjclabsdocker/webapi-polizas:latest
```

---

## ⚖️ Paso 5 - Desplegar en Kubernetes

**deployment.yaml**
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: webapi-polizas
spec:
  replicas: 1
  selector:
    matchLabels:
      app: webapi-polizas
  template:
    metadata:
      labels:
        app: webapi-polizas
    spec:
      containers:
      - name: webapi-polizas
        image: hjclabsdocker/webapi-polizas:latest
        ports:
        - containerPort: 8080
```

**service.yaml**
```yaml
apiVersion: v1
kind: Service
metadata:
  name: webapi-polizas-service
spec:
  type: NodePort
  selector:
    app: webapi-polizas
  ports:
    - protocol: TCP
      port: 80
      targetPort: 8080
      nodePort: 30080
```

Aplicar los manifiestos:

```bash
kubectl apply -f k8s/deployment.yaml
kubectl apply -f k8s/service.yaml
```

Ver:

```bash
kubectl get pods
kubectl get svc
```

Accedé a:

```
http://localhost:30080/swagger/index.html
```

---

## 🪤 Tips y errores comunes

- ⚠️ *Port already allocated*: Asegurate de detener contenedores anteriores con `docker stop <id>`.
- ⚠️ *ConnectionString not initialized*: Verificá que el `appsettings.json` esté correctamente montado.
- Para editar dentro del contenedor:

```bash
docker exec -it <container_id> sh
```

---

## 📖 Recursos Adicionales

- [Documentación oficial Docker](https://docs.docker.com/)
- [Documentación Kubernetes](https://kubernetes.io/docs/)
- [Conexión .NET con Azure SQL](https://learn.microsoft.com/sql/connect/)

---

## 🙌 Hecho por: **Henderson J.**
Con fines educativos y de despliegue profesional.

