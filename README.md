# Doppler ASP.NET Core Web App

Sample application showing how to use the Doppler CLI to inject secrets as environment variables or JSON settings files.

If deploying to Azure, also check out our [Azure Key Vault integration](https://docs.doppler.com/docs/azure-key-vault).

## Setup

Import the sample project to Doppler:

```sh
doppler import
```

Select the config to use:

```sh
doppler setup --project dotnet-core-webapp
```

Then confirm the Doppler CLI can fetch secrets for that config:

```sh
doppler secrets
```

Then build the application by running:

```sh
dotnet publish -c Release -o ./app --use-current-runtime --self-contained false
```

We'll now explore how to inject secrets into ASP.NET Core applications using the Doppler CLI.

> NOTE: Kubernetes deployment guides coming soon!

## 1. Doppler CLI Environment Variable Injection

The Doppler CLI acts as the application runner by injecting secrets as environment variables into the process in the required `PascalCase` format using the `dotnet-env` name transformer:

```sh
doppler run --name-transformer dotnet-env -- dotnet app/DopplerWebApp.dll
```

The same technique also works for local development:

```sh
doppler run --name-transformer dotnet-env -- dotnet run
```

## 2. Doppler CLI Mounted JSON Settings File

The Doppler CLI acts as the application runner by mounting an ephemeral `doppler.appsettings.json` secrets file loaded by the application during the [creation of the application Host](./Program.cs#L10).

```sh
doppler run --mount doppler.appsettings.json -- dotnet ./app/DopplerWebApp.dll
```

The ephemeral nature of the `doppler.appsettings.json` file is due the Doppler CLI using a Linux named pipe that is read like a file but is automatically cleaned up when the Doppler process exits, making it the only secure method for supplying secrets via the file system.

The only drawback for local development is that Linux pipes don't work with the `dotnet run` command because the files in the current directory are copied into the `Debug` directory as part of the build process and you can't create a copy of a named pipe.

We recommend using the environment variable injection method shown previously for local development. But if you're set on using the `doppler.appsettings.json` file , you can use a bash script similar to [doppler-json-settings-dev.sh](./bin/doppler-json-settings-dev.sh) which is a crude implementation of an ephemeral `doppler.appsettings.json` file:

```sh
./bin/doppler-json-settings-dev.sh dotnet run
```

## Summary

This is our initial exploration into integrating Doppler with ASP.NET Core and we are currently building out our own Doppler configuration provider which will make it possible to hydrate classes without the Doppler CLI.