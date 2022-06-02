FROM mcr.microsoft.com/dotnet/runtime:6.0 as runtime
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 as sdk
WORKDIR /src
COPY "AppProducer.csproj" .
RUN dotnet restore 
COPY . . 
RUN dotnet build -c Release

FROM sdk as publish
RUN dotnet publish -c Release -o /app/publish

FROM runtime as final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AppProducer.dll"]