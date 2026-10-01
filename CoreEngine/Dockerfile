FROM mcr.microsoft.com/dotnet/sdk:10.0 AS builder
WORKDIR /usr/src/app
LABEL maintainer="yrallabandi53@gmail.com"
COPY . .
COPY ["CoreEngine.csproj", "./"]
EXPOSE 8080
RUN dotnet restore "CoreEngine.csproj"
RUN dotnet publish "CoreEngine.csproj" \
-c Release \
-o /app/publish \
--no-restore \
/p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /usr/src/app
LABEL maintainer="yrallabandi53@gmail.com"
EXPOSE 8080
ENTRYPOINT ["dotnet", "CoreEngine.dll"]
COPY --from=builder /app/publish .
