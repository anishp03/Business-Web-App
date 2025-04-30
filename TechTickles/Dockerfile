# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechTickles/TechTickles.csproj", "TechTickles/"]
RUN dotnet restore "TechTickles/TechTickles.csproj"
COPY . .
RUN ls -al /app  # Check files in /app directory
RUN dotnet publish "TechTickles.csproj" -c Release -o /app/out --verbosity detailed

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TechTickles.dll"]
