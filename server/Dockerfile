FROM mcr.microsoft.com/dotnet/sdk:5.0
ADD . /src
WORKDIR /src
RUN dotnet build /p:Configuration=Release
CMD [ "dotnet", "bin/Release/net5.0/server.dll" ]
