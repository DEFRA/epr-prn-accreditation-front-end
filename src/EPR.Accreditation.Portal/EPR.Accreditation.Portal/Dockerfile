#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EPR.Accreditation.Portal/EPR.Accreditation.Portal.csproj", "EPR.Accreditation.Portal/"]
RUN dotnet restore "./EPR.Accreditation.Portal/./EPR.Accreditation.Portal.csproj"
COPY . .
WORKDIR "/src/EPR.Accreditation.Portal"
RUN dotnet build "./EPR.Accreditation.Portal.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EPR.Accreditation.Portal.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EPR.Accreditation.Portal.dll"]