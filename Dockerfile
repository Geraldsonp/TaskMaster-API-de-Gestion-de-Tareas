#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ticket.Manager.Api/Ticket.Manager.Api.csproj", "Ticket.Manager.Api/"]
COPY ["Ticket.Manager.Application/Ticket.Manager.Application.csproj", "Ticket.Manager.Application/"]
COPY ["Ticket.Manager.Domain/Ticket.Manager.Domain.csproj", "Ticket.Manager.Domain/"]
COPY ["Ticket.Manager.Infrastructure/Ticket.Manager.Infrastructure.csproj", "Ticket.Manager.Infrastructure/"]
RUN dotnet restore "Ticket.Manager.Api/Ticket.Manager.Api.csproj"
COPY . .
WORKDIR "/src/Ticket.Manager.Api"
RUN dotnet build "Ticket.Manager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket.Manager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticket.Manager.Api.dll"]