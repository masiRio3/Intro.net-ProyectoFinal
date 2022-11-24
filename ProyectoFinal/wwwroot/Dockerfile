FROM mcr.microsoft.com/dotnet/core/sdk AS build-env
WORKDIR /app

# Copy csproj and restore
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "DockerTest.dll"]