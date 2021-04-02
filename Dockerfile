#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Kaizen.Api/uploaddownloadfiles.csproj", "Kaizen.Api/"]
COPY ["Kaizen.Models/Kaizen.Models.csproj", "Kaizen.Models/"]
COPY ["Taste.Utilities/Taste.Utilities.csproj", "Taste.Utilities/"]
COPY ["Kaizen.DataAccess/Kaizen.DataAccess.csproj", "Kaizen.DataAccess/"]
RUN dotnet restore "Kaizen.Api/uploaddownloadfiles.csproj"
COPY . .
WORKDIR "/src/Kaizen.Api"
RUN dotnet build "uploaddownloadfiles.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "uploaddownloadfiles.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "uploaddownloadfiles.dll"]