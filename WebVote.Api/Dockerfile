#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["WebVote.Api/WebVote.Api.csproj", "WebVote.Api/"]
COPY ["WebVote.Business/WebVote.Business.csproj", "WebVote.Business/"]
COPY ["WebVote.Data/WebVote.Data.csproj", "WebVote.Data/"]
COPY ["WebVote.Constants/WebVote.Constants.csproj", "WebVote.Constants/"]
RUN dotnet restore "WebVote.Api/WebVote.Api.csproj"
COPY . .
WORKDIR "/src/WebVote.Api"
RUN dotnet build "WebVote.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebVote.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebVote.Api.dll"]