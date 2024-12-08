FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /main
COPY ["User.Login.Api/User.Login.Api.csproj", "User.Login.Api/"]
RUN dotnet restore "User.Login.Api/User.Login.Api.csproj"
COPY . .

RUN dotnet build "User.Login.Api/User.Login.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Login.Api/User.Login.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Login.Api.dll"]
