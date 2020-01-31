FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build-env
WORKDIR /src
COPY ./src/Emailer/*.csproj ./Emailer/
COPY ./src/Emailer.Api/*.csproj ./Emailer.Api/
COPY ./src/Emailer.UnitTests/*.csproj ./Emailer.UnitTests/
COPY ./src/Emailer.IntegrationTests/*.csproj ./Emailer.IntegrationTests/
COPY ./src/mailhoghandson.sln ./
RUN dotnet restore 
COPY ./src/ ./

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS test-env
WORKDIR /src
COPY --from=build-env /src/ .
RUN dotnet test Emailer.UnitTests

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS publish-env
WORKDIR /src
COPY --from=build-env /src/ .
RUN dotnet publish Emailer.Api -c Release -o output

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 as runtime
WORKDIR /app
COPY --from=publish-env /src/Emailer.Api/output/ .
ENTRYPOINT ["dotnet", "Emailer.Api.dll"]