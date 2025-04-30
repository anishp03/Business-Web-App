# Use the .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechTickles/TechTickles.csproj", "TechTickles/"]
RUN dotnet restore "TechTickles/TechTickles.csproj"

# Copy the rest of the files
COPY . .
WORKDIR "/src/TechTickles"
RUN dotnet publish "TechTickles.csproj" -c Release -o /app/out --verbosity detailed

# Copy the build output to the base image and define the entrypoint
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "TechTickles.dll"]
