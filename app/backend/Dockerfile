FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /opt/build/
COPY ./ExRates.Domain/*.csproj ./ExRates.Domain/
COPY ./ExRates.Application/*.csproj ./ExRates.Application/
COPY ./ExRates.Infrastructure/*.csproj ./ExRates.Infrastructure/
COPY ./ExRates.Api/*.csproj ./ExRates.Api/

WORKDIR /opt/build/ExRates.Api/
RUN dotnet restore --use-current-runtime

WORKDIR /opt/build/
COPY ./ExRates.Domain/. ./ExRates.Domain/
COPY ./ExRates.Application/. ./ExRates.Application/
COPY ./ExRates.Infrastructure/. ./ExRates.Infrastructure/
COPY ./ExRates.Api/. ./ExRates.Api/

WORKDIR /opt/build/ExRates.Api/
RUN dotnet publish --use-current-runtime --self-contained false --no-restore -o /opt/app/

FROM mcr.microsoft.com/dotnet/aspnet:6.0

# for healthy self-checks
RUN apt-get update; apt-get -y install curl

WORKDIR /opt/app/
COPY --from=build /opt/app/ ./

ENTRYPOINT ["dotnet", "ExRates.Api.dll"]

EXPOSE 80
