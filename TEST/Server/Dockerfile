#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TEST/Server/TEST.Server.csproj", "TEST/Server/"]
COPY ["TEST/Client/TEST.Client.csproj", "TEST/Client/"]
COPY ["TEST/Shared/TEST.Shared.csproj", "TEST/Shared/"]
RUN dotnet restore "TEST/Server/TEST.Server.csproj"
COPY . .
WORKDIR "/src/TEST/Server"
RUN dotnet build "TEST.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TEST.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TEST.Server.dll"]