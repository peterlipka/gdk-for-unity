[//]: # (Doc of docs reference 23)
[//]: # (TODO - tech writer pass)

# Troubleshooting

* [Errors](#errors)
* [Exceptions](#exceptions)
* [Problems](#problems)

We also maintain a [known issues]({{urlRoot}}/known-issues) page for any open bugs in the SpatialOS GDK for Unity.


## Errors

#### Error reported
When building workers (Unity Editor menu: **Build For Cloud** > **my_worker_name**):
**_Error building player because build target was unsupported_**</br>

**Issue and workarounds**<br/>
 You don't have the right Unity build settings enabled.<br/>
 In your Unity Editor:

* Check what build support you have enabled (menu: **File** > **Build Settings**) and make sure you have your target build platform enabled. <br/>
  * You need **Mac** build support if you are developing on a Windows PC and want to share your game with Mac users.
  * You need **Windows** build support if you are developing on a Mac and want to share your game with Windows PC users. 
  * Unity gives you build support for your development machine (Windows or Mac) by default.
  * _**In addition**_, make sure you have **Linux** build support enabled.<br/> 
 You need Linux build support because all server-workers in a cloud deployment run in a Linux environment. 
 <br/>Fix this by runing the Unity installer and selecting the appropriate option. (See [Setup and installing]({{urlRoot}}/setup-and-installing#set-up-your-machine)).
 
 **Note:** When you come to build your project, in the `Assets/Fps/Config/BuildConfiguration`, do not change the `UnityGameLogic Cloud Environment` from Linux. This will cause further build errors.

<br/>

#### Error reported 

_Could not discover location for dotnet.exe._

**Issue and workarounds**<br/>
Ensure that you have the [.NET Core SDK (x64)(Microsoft documentation)](https://www.microsoft.com/net/download/dotnet-core/2.1) installed and that the directory containing
the dotnet executable is added to your PATH environment variable. Restart your computer
and Unity after installing the .NET Core SDK.

<br/>

## Exceptions

#### Exception thrown
_`<name of a type>` is an IComponentData, and thus must be blittable
(No managed object is allowed on the struct)._

**Issue and workarounds**<br/>

* The struct you defined implements `IComponentData` and contains [non-blittable](https://docs.microsoft.com/en-us/dotnet/framework/interop/blittable-and-non-blittable-types) fields. Types that implement
`IComponentData` may only contain blittable fields.

* `bool` as well as `System.Boolean` are not blittable. Please use `Improbable.Gdk.Core.BlittableBool`, a blittable alternative
implementation we provide, in that case.

<br/>

### Exception thrown 
When building a worker:<br/> 
_`<path to burst package>\.Runtime\bcl.exe` did not run properly!_

**Issue and workarounds**<br/>
This is a benign exception that is thrown when building a worker while burst compilation is turned on. Your worker was successfully built despite this error. To remove the error message, disable Burst compilation by unchecking `Jobs > Enable Burst Compilation`
in the Unity Editor.

<br/>

## Log warnings

#### Unity Editor logs warning

The Unity Editor logs a bunch of warnings when I open the SpatialOS GDK Unity project of the  for the first time. 

**Issue and workarounds**<br/>
The warnings originate from code written by Unity, e.g. from `com.unity.mathematics` or `com.unity.entities`.
These warnings are caused by code maintained by Unity over which we have no control. The warnings are benign so you can ignore them.

<br/>

#### Unity Editor logs warning
Several error messages are logged after I destroyed a GameObject or ECS entity representing a SpatialOS entity.

**Issue and workarounds**<br/>
Do not proactively destroy GameObjects or ECS entities representing SpatialOS entities. The SpatialOS GDK cleans them up automatically when the respective SpatialOS entity is removed from the worker. Send a [DeleteEntity world command]({{urlRoot}}/content/gameobject/world-commands.md) to delete the entity on the SpatialOS side instead.

<br/>

## Problems

#### A Reader/Writer or CommandSender/Handler in my code is null

**Issue and workarounds**<br/>

  * Ensure that your field is declared in a MonoBehaviour. They are not supported in ECS systems.
  * Ensure that the field is decorated with a [Require] attribute.
  * Ensure that the GameObject containing your MonoBehaviour is associated with a SpatialOS entity. You can verify this by examining whether a `SpatialOSComponent` MonoBehaviour was added to your GameObject at runtime.
  * Ensure that you only access Requirables in a MonoBehaviour while the MonoBehaviour is enabled. Requirables are supposed to be null while the MonoBehaviour is disabled. Note that certain Unity event methods like `OnTriggerEnter` or `OnCollisionEnter` may be invoked even if a MonoBehaviour is disabled.

<br/>

#### A MonoBehaviour containing fields using the [Require] attribute stays disabled when I expect it to be enabled

**Issue and workarounds**<br/>


  * Ensure that `GameObjectRepresentationHelper.AddSystems` is called when initializing your [`Worker`]({{urlRoot}}/content/glossary#worker).
  * Ensure that the GameObject containing your MonoBehaviour is associated with a SpatialOS entity. You can verify this be examining whether a `SpatialOSComponent` MonoBehaviour was added to your GameObject at runtime.
  * Ensure that the [requirements]({{urlRoot}}/content/gameobject/interact-spatialos-monobehaviours) for all your fields in your MonoBehaviour are satisfied. You can verify this by examining the SpatialOS entity associated with your GameObject in the Inspector. In the Inspector, ensure that all relevant components are attached to your SpatialOS entity and that read and write access is delegated correctly for your worker.
