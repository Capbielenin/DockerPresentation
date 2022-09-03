FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV PORT=80
ENV ASPNETCORE_URLS=http://+:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /KebabMenuApplication
COPY . .
RUN dotnet restore "KebabMenuApplication.csproj"
RUN dotnet publish -c Debug -o /app/publish

FROM base AS final
RUN apt-get update && apt-get install -y \
curl
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KebabMenuApplication.dll"]
