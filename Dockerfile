FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY backoffice_z/*.csproj ./backoffice_z/
RUN dotnet restore

# copy everything else and build app
COPY backoffice_z/. ./backoffice_z/
WORKDIR /app/backoffice_z
RUN dotnet publish -c Release -o out
EXPOSE 1433

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/backoffice_z/out ./
ENTRYPOINT ["dotnet", "backoffice_z.dll"]
