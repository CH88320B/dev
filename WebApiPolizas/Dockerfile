# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory for the build
WORKDIR /app

# Copy the entire source code to the container
COPY . .

# Restore the dependencies (via nuget)
RUN dotnet restore

# Publish the application in Release mode, output to 'out' directory
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory for the runtime container
WORKDIR /app

# Copy the output of the publish step into the runtime container
COPY --from=build /app/out .

# Define the entry point of the application
ENTRYPOINT ["dotnet", "WebApiPolizasRabbit.dll"]