#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Transfer.Notification/Transfer.Notification.csproj", "Transfer.Notification/"]
RUN dotnet restore "Transfer.Notification/Transfer.Notification.csproj"
COPY . .
WORKDIR "/src/Transfer.Notification"
RUN dotnet build "Transfer.Notification.csproj" -c Release -o /app/build
RUN dotnet dotnet dev-certs https --trust

FROM build AS publish
RUN dotnet publish "Transfer.Notification.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transfer.Notification.dll"]