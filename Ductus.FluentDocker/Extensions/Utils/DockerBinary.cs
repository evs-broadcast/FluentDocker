﻿using System;

namespace Ductus.FluentDocker.Extensions.Utils
{
  public enum DockerBinaryType
  {
    DockerClient = 1,
    Machine = 2,
    Compose = 3
  }

  public sealed class DockerBinary
  {
    internal DockerBinary(string path, string binary)
    {
      Path = path;
      Binary = binary.ToLower();
      Type = Translate(binary);

      var isToolbox = Environment.GetEnvironmentVariable("DOCKER_TOOLBOX_INSTALL_PATH")?.ToLower().Equals(Path);
      IsToolbox = isToolbox ?? false;
    }

    public static DockerBinaryType Translate(string binary)
    {
      switch (binary.ToLower())
      {
        case "docker":
        case "docker.exe":
          return DockerBinaryType.DockerClient;
        case "docker-machine":
        case "docker-machine.exe":
          return DockerBinaryType.Machine;
        case "docker-compose":
        case "docker-compose.exe":
          return DockerBinaryType.Compose;
        default:
          throw new Exception($"Cannot determine the docker type on binary {binary}");
      }
    }

    public string FqPath => System.IO.Path.Combine(Path, Binary);
    public string Path { get; }
    public string Binary { get; }
    public DockerBinaryType Type { get; }
    public bool IsToolbox { get; }
  }
}