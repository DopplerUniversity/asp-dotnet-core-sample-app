# ASP.NET Core Web App (SDK 6.0)

This sample app demonstrates three different options for secrets injection.

## 1. Doppler CLI Environment Variable Injection

The Doppler CLI acts as the application runner by injecting secrets as environment variables into the process in the required `PascalCase` format using the `dotnet-env` name transformer:

```sh
doppler run  --name-transformer dotnet-env -- dotnet run
```

This is the preferred method as unencrypted secrets never touch the file system.

The only caveat is that because the Doppler CLI is spawning the application process, you'll need to [manually attach the debugger to the process](https://docs.microsoft.com/en-us/visualstudio/debugger/attach-to-running-processes-with-the-visual-studio-debugger?view=vs-2022#:~:text=You%20can%20attach%20the%20Visual,the%20debugger%20to%20the%20process.) during local development each time.

If you're debugging frequently enough that this is a deal breaker, the following JSON option is available.

## 2. Doppler CLI Injected JSON Configuration File

Secrets are downloaded in the [JSON configuration provider's](https://docs.microsoft.com/en-us/dotnet/core/extensions/configuration-providers#json-configuration-provider) expected format and loaded by the application during the [creation of the application Host](./Program.cs#L12).

During local development, secrets would be downloaded the secrets JSON file in a pre-build step, then deleted the once the debugger exits to ensure secrets aren't persisted beyond the life of the application process.

The JSON can be generated by running:

```sh
doppler secrets download --name-transformer dotnet --no-file > doppler.appSettings.json
```

During local development, secrets would be downloaded the secrets JSON file in a pre-build step, then deleted the once the debugger exits to ensure secrets aren't persisted beyond the life of the application process.

We can approximate this flow using bash script at [bin/dotnet-run-json.sh](./bin/dotnet-run-json.sh) which uses `trap` to capture the exit signal of the process and remove the `doppler.json` file:

```sh
./bin/dotnet-run-json.sh
```

## 3. Secret Manager Tool for Local Development

Another possible solution for local development only is the [Secret Manager Tool](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux#secret-manager).

Simply initialize the secret store for the current application, then feed secrets from the Doppler CLI to the `dotnet user-secrets set` command:

```sh
dotnet user-secrets init
doppler secrets download --name-transformer dotnet --no-file  | dotnet user-secrets set
```

We don't recommend this option as the Secret Manager tool doesn't encrypt the stored secrets, plus this option doesn't dynamically keep your secrets in sync.