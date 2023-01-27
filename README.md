# math-eval project

## SDK and IDE Installation

1. <https://code.visualstudio.com/docs/languages/dotnet>
2. <https://dotnet.microsoft.com/en-us/download>

## Project Bootstrap

```zsh
dotnet new console
```

## Running the app

```zsh
# running the tests
dotnet run ProgramTests.cs
```

## Dump dotnet sdk version

```zsh
dotnet --version > .dotnet
```

## Containerized builds

```zsh
# build the image
docker build -t github.com/laiyewhon/math-eval:0.0.1-SNAPSHOT .

# run the image in interactive mode to display console output
docker run -ti github.com/laiyewhon/math-eval:0.0.1-SNAPSHOT
```

## References

1. [.gitignore](https://github.com/dotnet/core/blob/main/.gitignore)
