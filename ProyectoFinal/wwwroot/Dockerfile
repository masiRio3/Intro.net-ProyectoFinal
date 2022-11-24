FROM microsoft/dotnet:2.0-sdk AS build

COPY ProyectoFinal/*.csproj ./app/ProyectoFinal/

WORKDIR /app/ProyectoFinal
RUN dotnet restore

COPY ProyectoFinal/. ./ProyectoFinal/
RUN dotnet publish -o out /p:PublishWithAspNetCoreTargetManifest="false"

FROM microsoft/dotnet:2.0-runtime AS runtime
ENV ASPNETCORE_URLS http://+:80
WORKDIR /app
COPY --from=build /app/ProyectoFinal/out ./
ENTRYPOINT ["dotnet", "ProyectoFinal.dll"]

