FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENV PORT=80
ENV ASPNETCORE_URLS=http://+:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /KebabMenuApplication
COPY . .
RUN dotnet restore "KebabOrderApplication.csproj"
RUN dotnet publish -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KebabOrderApplication.dll"]
