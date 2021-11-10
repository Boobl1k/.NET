FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HM7/WebAppMVC/WebAppMVC.csproj", "WebAppMVC/"]
RUN dotnet restore "HM7/WebAppMVC/WebAppMVC.csproj"
COPY . .
WORKDIR "/src/WebAppMVC"
RUN dotnet build "WebAppMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAppMVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppMVC.dll"]
