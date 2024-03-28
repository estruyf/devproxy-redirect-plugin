# Dev Proxy plugin to redirect requests to a API endpoint

This is a [Microsoft's Dev Proxy](https://learn.microsoft.com/en-us/microsoft-cloud/dev/dev-proxy/overview) plugin that redirects requests for a specific path to a different API endpoint.

You can for instance use this plugin when you want to redirect API calls from your production environment to your local development environment.

> You can read more about Dev Proxy plugin development on the following blog post: [Developing custom plugins for the Microsoft's Dev Proxy](https://www.eliostruyf.com/developing-custom-plugins-microsoft-dev-proxy/)

## Usage

- Clone this repository
- Add the `dev-proxy-abstractions.dll` to the root of the project. You can find the `dev-proxy-abstractions.dll` in the latest release of the [Dev Proxy](https://github.com/microsoft/dev-proxy/releases)
- Build the project by running the following command: `dotnet build`
- Once you have built the project, you should see a `bin` folder in the root of the project. In this folder, you will find the `DevProxyCustomPlugin.dll` file which you need for your Dev Proxy configuration.

## Configuration

To configure the plugin, you need to add the following configuration to your `dev-proxy.json` file:

```json
{
  "$schema": "https://raw.githubusercontent.com/microsoft/dev-proxy/main/schemas/v0.15.0/rc.schema.json",
  "plugins": [{
    "name": "RedirectCalls",
    "enabled": true,
    "pluginPath": "./bin/Debug/net8.0/DevProxyCustomPlugin.dll",
    "configSection": "redirectCalls"
  }],
  "redirectCalls": {
    "fromUrl": "", // The URL you want to redirect from
    "toUrl": "" // The URL you want to redirect to
  },
  "urlsToWatch": [
    // Add the URLs you want to watch
  ],
  "labelMode": "text",
  "logLevel": "debug",
  "newVersionNotification": "stable"
}
```
