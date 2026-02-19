FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShelterHelper.csproj", "./"]
RUN dotnet restore "ShelterHelper.csproj"
COPY . .
RUN dotnet publish "ShelterHelper.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "ShelterHelper.dll"]