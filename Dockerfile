#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.test

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KhalifaCommunityAPI/KhalifaCommunityAPI.csproj", "KhalifaCommunityAPI/"]
RUN dotnet restore "KhalifaCommunityAPI/KhalifaCommunityAPI.csproj"
COPY . .
WORKDIR "/src/KhalifaCommunityAPI"
RUN dotnet build "KhalifaCommunityAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KhalifaCommunityAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KhalifaCommunityAPI.dll"]
