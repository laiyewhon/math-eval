ARG VER_DOTNET_SDK=7.0.100
ARG REPO_DOTNET_SDK=bitnami/dotnet-sdk:$VER_DOTNET_SDK

ARG VER_DOTNET_RUNTIME=7.0.1
ARG REPO_DOTNET_RUNTIME=bitnami/dotnet:$VER_DOTNET_RUNTIME

FROM $REPO_DOTNET_SDK

WORKDIR /app

COPY . /app

RUN dotnet publish math-eval.csproj -p:StartupObject=ProgramTest

FROM $REPO_DOTNET_RUNTIME

WORKDIR /app

COPY --from=0 /app/bin/Debug/net7.0/publish /app/

CMD [ "/app/math-eval" ]
