#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./api/api.csproj", "./api/"]
RUN dotnet restore "./api/api.csproj"
COPY . ./api/
COPY ../DataAccessLayer/ ./DataAccessLayer/
RUN dotnet build "./api/api.csproj" -c Release -o /app/build

# Make sure that we update the database to use the latest migrations.
FROM mcr.microsoft.com/dotnet/core/sdk:6.0 AS setup
WORKDIR /src/DataAccessLayer
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef --version 6.0.7
RUN dotnet-ef migrations add CreateDatabase
RUN dotnet-ef migrations add LimitStrings
RUN dotnet-ef database update

FROM build AS publish
RUN dotnet publish "./api/api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "./AssembledApi.dll"]