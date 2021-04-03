FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY  ["Kaizen.Api/uploaddownloadfiles.csproj", "Kaizen.Api"]
COPY  ["Kaizen.DataAccess/Kaizen.DataAccess.csproj", "Kaizen.DataAccess"]
COPY   ["Kaizen.Models/Kaizen.Models.csproj", "Kaizen.Models"] 
COPY   ["Taste.Utilities/Taste.Utilities.csproj", "Taste.Utilities"] 

RUN dotnet restore "Kaizen.Api/uploaddownloadfiles.csproj"
COPY . .


FROM build AS publish
RUN dotnet publish . -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD [ "dotnet","uploaddownloadfiles.dll" ]


COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]