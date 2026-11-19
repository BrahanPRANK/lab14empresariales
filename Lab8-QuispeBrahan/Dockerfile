FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Lab8-QuispeBrahan/Lab8-QuispeBrahan.csproj", "Lab8-QuispeBrahan/"]
RUN dotnet restore "Lab8-QuispeBrahan/Lab8-QuispeBrahan.csproj"
COPY . .
WORKDIR "/src/Lab8-QuispeBrahan"
RUN dotnet publish "Lab8-QuispeBrahan.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} exec dotnet Lab8-QuispeBrahan.dll"]
