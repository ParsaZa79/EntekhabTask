FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EntekhabSalary.WebApi/EntekhabSalary.WebApi.csproj", "EntekhabSalary.WebApi/"]
RUN dotnet restore "EntekhabSalary.WebApi/EntekhabSalary.WebApi.csproj"
COPY . .
WORKDIR "/src/EntekhabSalary.WebApi"
RUN dotnet build "EntekhabSalary.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EntekhabSalary.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EntekhabSalary.WebApi.dll"]
