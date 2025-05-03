<h1 align="center">Unity Runtime CMD Console</h1>

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/showcase.gif" align="center">

The `ConsoleWindow.cs` script is a simple MonoBehaviour which will initialize a CMD window that shows all logs from Unity's Debug class. This is useful for people tring to debug their code in a build, and espically useful for people who have mmore than 1 monitor asthe CMD console is an external window menaing it can be dragged across monitors.

> [!WARNING]
> This script only works on Windows OS and only works in builds, not the editor. However, you can attach the script to any GameObject and it will only show when it's in a Windows build.

# How To Use
Simply create a new GameObject called `Console Window` and drag the `ConsoleWindow.cs` script onto it! You can adjust the settings as you like, and then simply go on with your game development journey. The console will automatically show logs and will only appear when you open a windows build that matches the [build limit](https://github.com/SlushyRH/Unity-CMD-Console/tree/main?tab=readme-ov-file#Settings).

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/console.png" align="center">

## Settings
<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/inspector.png" align="center">

- **Build Limit** controls when the console is allowed based on the type of build:
    - **None** means it will never show
    - **Everything** means it will always show
    - **Development** means it will only show on development builds
    - **Standard** means it will only show on standard builds
- **Include Log Type** will add the log type in front of the message:
    - [Info] Log Message Here
    - [Error] Log Message Here
- **Include Timestamp** will add the time of the log in front of the message:
    - [16:24:14] Log Message Here
- **Timestamp Format** will allow you to swtich the format of the timestamp between 12 and 24 hour formatting:
    - [4:24:14 PM] Log Message Here
    - OR
    - [16:24:14] Log Message Here
- **Log Colours** will allow you to control what colour each log appears as in the console.