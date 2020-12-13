FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

LABEL version="1.0" maintainer="MarcelinoBorges"

WORKDIR /app

COPY ./bin/Release/publish .

ENTRYPOINT ["dotnet", "API.dll"]