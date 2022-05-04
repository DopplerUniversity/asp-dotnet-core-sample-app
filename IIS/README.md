# IIS web.config

You can use the Doppler CLI to populate the `<environmentVariables> ` element in a [web.config.tmpl](./web.config.tmpl) Doppler template:

```sh
doppler secrets substitute web.config.tmpl > web.config
```
