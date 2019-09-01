FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["../InsightDerm.Host.Backend/InsightDerm.Host.Backend.csproj", "../InsightDerm.Host.Backend/"]
RUN dotnet restore "../InsightDerm.Host.Backend/InsightDerm.Host.Backend.csproj"
COPY . .
WORKDIR "/src/../InsightDerm.Host.Backend"
RUN dotnet build "InsightDerm.Host.Backend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "InsightDerm.Host.Backend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "InsightDerm.Host.Backend.dll"]