ARG VER_DOTNET_SDK=7.0.100
ARG REPO_DOTNET_SDK=bitnami/dotnet-sdk:$VER_DOTNET_SDK

FROM $REPO_DOTNET_SDK

WORKDIR /app

COPY . /app

CMD [ "dotnet", "run", "ProgramTests.cs" ]