# Doppler ASP.NET Core Web App Examples

Examples using the Doppler CLI to hydrate a [Doppler class](./Models/Doppler.cs) with JSON or environment variables via [Configuration providers](httpshttps://docs.microsoft.com/en-gb/aspnet/core/fundamentals/configuration/?view=aspnetcore-6.0).

If deploying to Azure, using our [Azure Key Vault integration](https://docs.doppler.com/docs/azure-key-vault) is likely to offer a better experience, but the following may still be of interest for local development.

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

You're now ready to start exploring the sample applications

## Sample Apps

- [Blazor 6.0](./blazor-app-6.0)
- [IIS web.config](./IIS/)

## Summary

This is our initial exploration into integrating Doppler with ASP.NET core and we are currently building out our own Doppler configuration provider which will make it possible to hydrate classes without the Doppler CLI.