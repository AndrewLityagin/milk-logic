FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY WebApi/*.csproj WebApi/
COPY Application/*.csproj Application/
COPY Infrastructure/*.csproj Infrastructure/
COPY Domain/*.csproj Domain/


RUN dotnet restore WebApi/WebApi.csproj

COPY . .

RUN dotnet publish WebApi/WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5034
EXPOSE 5034
ENTRYPOINT ["dotnet", "WebApi.dll"]