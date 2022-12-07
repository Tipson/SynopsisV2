FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SynopsisV2/SynopsisV2.csproj", "SynopsisV2/"]
RUN dotnet restore "SynopsisV2/SynopsisV2.csproj"
COPY . .
WORKDIR "/src/SynopsisV2"
RUN dotnet build "SynopsisV2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SynopsisV2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SynopsisV2.dll"]
