# Use the official .NET 6.0 SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Publish the application to the out folder
RUN dotnet publish -c Release -o out --no-restore

# Use the official .NET 6.0 runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/out .

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "TechTickles.dll"]
