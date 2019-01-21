# Get started: 4 - Upload and launch your game

This has 3 steps:

* Set your project name
* Upload worker assemblies
* Launch a cloud deployment


### Set your project name

When you signed up for SpatialOS, your account was automatically associated with an organisation and a project, both of which have the same generated name. To find this name enter the [Console](https://console.improbable.io/projects). It should looks like `beta_randomword_anotherword_randomnumber`:

<img src="{{assetRoot}}assets/project-page.png" style="margin: 0 auto; display: block;" />

Using a text editor of your choice, open `gdk-for-unity-fps-starter-project/spatialos.json` and replace the `unity_gdk` project name with the project name you were assigned in the Console. This will tell SpatialOS which project you intend to upload to. Your `spatialos.json` should look like this:

```json
{
  "name": "beta_yankee_hawaii_621",
  "project_version": "0.0.1",
  "sdk_version": "14.0-b6143-48ac8-WORKER-SNAPSHOT",
  "dependencies": [
      {"name": "standard_library", "version": "14.0-b6143-48ac8-WORKER-SNAPSHOT"}
  ]
}
```

You also need to set your project name in the `Deployments` window of the Unity Editor. Select `SpatialOS > Deployment Launcher` to open then `Deployments` window.

Fill in the "Project Name" field at the top of the window in the Unity Editor.

<br/>
### Upload worker assemblies

An [assembly](https://docs.improbable.io/reference/latest/shared/glossary#assembly) is a bundle of code, art assets and other files necessary to run your game in the cloud.

To run a deployment in the cloud, you must upload the worker assemblies to your SpatialOS project. Do this using the `Deployments` window in the Unity Editor. Select `SpatialOS > Deployment Launcher` to open then `Deployments` window.

To upload an assembly, under the "Assembly" section, fill in the "Assembly Name" field in the window and select Upload Assembly. A valid configuration looks like the following:

<img src="{{assetRoot}}assets/deployment-window.png" style="margin: 0 auto; display:block;" />

> **It’s finished uploading when:** You see an upload report printed in your Unity console, for example:
```
Uploaded assembly my_assembly to project unity_gdk successfully.
```

Based on your network speed, this may take a little while (1-10 minutes) to complete.

<br/>

### Launch a cloud deployment

The next step is to [launch a cloud deployment](https://docs.improbable.io/reference/latest/shared/deploy/deploy-cloud#5-deploy-the-project) using the assembly that you just uploaded. Do this using the `Deployments` window in the Unity Editor. Select `SpatialOS > Deployment Launcher` to open then `Deployments` window.

When launching a cloud deployment you must provide four parameters:

* **the assembly name**, which identifies the worker assemblies to use. The name needs to conform to the following regex: `[a-zA-Z0-9_.-]{5,64}`.
* **a launch configuration**, which declares the world and load balancing configuration.
* **a name for your deployment**, which is used to label the deployment in the SpatialOS web Console. The name needs to conform to the following regex: `[a-z0-9_]{2,32}`.
* **a snapshot**, which declares the state of the world at startup

In the `Deployments` window, under the "Deployment Launcher" section, fill in the `Assembly Name` and `Deployment Name` fields, where `Assembly Name` is the name you gave the assembly in the previous step and `Deployment Name` is a name of your choice (for example, shootyshooty).

Additionally, set `Snapshot Path` to "snapshots/cloud.snapshot" and `Config` to `cloud_launch_small.json`.

Ensure that the `Enable simulated players` is checked and select `Launch deployment`. A valid configuration looks like the following:

<img src="{{assetRoot}}assets/deployment-window-end.png" style="margin: 0 auto; display:block;" />

> **It's finished when:** TODO

## Well done getting set up!
It’s time to play your game.

#### Next: [Get playing!]({{urlRoot}}/content/get-started/get-playing.md)
