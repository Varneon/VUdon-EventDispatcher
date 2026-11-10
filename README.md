<div>

# [VUdon](https://github.com/Varneon/VUdon) - Event Dispatcher [![GitHub Repo stars](https://img.shields.io/github/stars/Varneon/VUdon-EventDispatcher?style=flat&label=Stars)](https://github.com/Varneon/VUdon-EventDispatcher/stargazers) [![GitHub all releases](https://img.shields.io/github/downloads/Varneon/VUdon-EventDispatcher/total?color=blue&label=Downloads&style=flat)](https://github.com/Varneon/VUdon-EventDispatcher/releases) [![GitHub tag (latest SemVer)](https://img.shields.io/github/v/tag/Varneon/VUdon-EventDispatcher?color=blue&label=Release&sort=semver&style=flat)](https://github.com/Varneon/VUdon-EventDispatcher/releases/latest)

</div>

Dispatcher for delegating update events to UdonBehaviours.

![image](https://github.com/Varneon/VUdon-EventDispatcher/assets/26690821/b7e7b630-753f-4d7a-8888-97e82b5b7b4a)

### *What does this help with? Why would I need this?*
If you have UdonSharpBehaviours with update methods like `FixedUpdate()`, `Update()`, `LateUpdate()` and `PostLateUpdate()` in your scene that only have to be fired occasionally depending on a state, declaring them has a static cost associated with it and a guard clause (`if (!shouldRunUpdate) { return; }`) at the top of the method also has it's own cost of additional instructions and doesn't completely get rid of the overhead.

By using an event dispatcher you can dynamically subscribe to update events from U#, allowing you to easily have hundreds if not thousands of behaviours that have update events that might only have to be fired temporarily, e.g. a pickup when being held, a car when being driven or sat in or a UI when being interacted with.

When you dynamically subscribe to update events during runtime, you don't have 1000 update methods firing up in the background only to be immediately returned and moved to the next behaviour, you only pay the performance cost for the update events that you need at that time, and shaving microseconds is what matters in large, complex frameworks.

# Installation

### Dependencies - `3`

* [VUdon - Array Extensions](https://github.com/Varneon/VUdon-ArrayExtensions) - Allows clean addition and removal of event handlers in the background
* [VUdon - Common](https://github.com/Varneon/VUdon-Common) - Common C# enums shared by the VUdon ecosystem
* [VUdon - Editors](https://github.com/Varneon/VUdon-Editors) - Inspector with consistent style across the VUdon ecosystem

### A) Import with [VRChat Creator Companion](https://vcc.docs.vrchat.com/vpm/)
* https://vpm.varneon.com/ *(Dependencies will be included in the repository lists)*

### B) Import from [Unitypackage](https://docs.unity3d.com/2022.3/Documentation/Manual/AssetPackagesImport.html)
1. Download and import [dependencies](https://github.com/Varneon/VUdon-EventDispatcher/README.md#dependencies---3) from the respective repositories with their specified installation instructions
2. Download latest `com.varneon.vudon.event-dispatcher.unitypackage` from [here](https://github.com/Varneon/VUdon-EventDispatcher/releases/latest)
3. Import the downloaded .unitypackage into your Unity project

<div align="center">

## Developed by Varneon with :hearts:

[![Twitter Follow](https://img.shields.io/static/v1?style=for-the-badge&label=@Varneon&message=8.4K&color=1b9df0&logo=x)](https://x.com/Varneon)
[![YouTube Channel Subscribers](https://img.shields.io/static/v1?style=for-the-badge&label=@Varneon&message=1.4K&color=%23FF0000&logo=YouTube)](https://www.youtube.com/Varneon)
[![GitHub followers](https://img.shields.io/github/followers/Varneon?color=%23303030&label=Varneon&logo=GitHub&style=for-the-badge)](https://github.com/Varneon)

</div>
