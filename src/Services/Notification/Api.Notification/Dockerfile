#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app-source
RUN dotnet dev-certs https

# Copy the main source project files
#COPY ./../*.sln ./
COPY ./src/Services/Api.Notification/Api.Notification/*.csproj ./src/Services/Api.Notification/Api.Notification/
COPY ./src/Common/AspNetCore.Common/*.csproj ./src/Common/AspNetCore.Common/

# Restore as distinct layers
RUN dotnet restore ./src/Services/Api.Notification/Api.Notification/Api.Notification.csproj

# Remainder of build process
COPY . .

#Build only Api-Notification
WORKDIR ./src/Services/Api.Notification/Api.Notification
RUN dotnet build -c Release -o /app/build

#Publish artifact
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Api.Notification.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Api.Notification.dll