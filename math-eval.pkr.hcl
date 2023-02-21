packer {
  required_plugins {
    docker = {
      version = ">= 0.0.7"
      source  = "github.com/hashicorp/docker"
    }
  }
}

source "docker" "math-eval" {
  image  = "bitnami/dotnet-sdk:7.0.100"
  commit = true
  changes = [
    "WORKDIR /app",
    "CMD [\"dotnet\", \"run\", \"math-eval.csproj\", \"-p:StartupObject=ProgramTest\"]",
    "ENTRYPOINT [\"\"]"
  ]
}

build {
  sources = ["source.docker.math-eval"]

  provisioner "file" {
    source      = "."
    destination = "/app"
  }

  provisioner "shell" {
    inline = [
      "rm -f .dotnet"
    ]
  }

  post-processor "docker-tag" {
    repository = "github.com/laiyewhon/math-eval"
    tags       = ["0.0.1-PACKER-SNAPSHOT"]
  }
}