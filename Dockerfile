FROM mcr.microsoft.com/dotnet/core/sdk as build-env
WORKDIR /app
COPY *.csproj ./
COPY nuget.config ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime
WORKDIR /app
COPY --from=build-env /app/out ./
ENTRYPOINT ["dotnet", "GreenSharp.dll"]