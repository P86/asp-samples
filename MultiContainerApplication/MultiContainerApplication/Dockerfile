#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MultiContainerApplication/MultiContainerApplication.csproj", "MultiContainerApplication/"]
RUN dotnet restore "MultiContainerApplication/MultiContainerApplication.csproj"
COPY . .
WORKDIR "/src/MultiContainerApplication"
RUN dotnet build "MultiContainerApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MultiContainerApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MultiContainerApplication.dll"]
