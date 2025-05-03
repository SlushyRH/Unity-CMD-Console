<h1 align="center">Unity Runtime CMD Console</h1>

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/showcase.gif" align="center">

[ConsoleWindow.cs](https://github.com/SlushyRH/Unity-CMD-Console/blob/main/ConsoleWindow.cs) is a simple MonoBehaviour which will initialize a CMD window that shows all logs from Unity's Debug class. This is useful for people trying to debug their code in a build, and especially useful for people who have more than 1 monitor as the CMD console is an external window meaning it can be dragged across monitors.

> [!WARNING]
> The console will only open if the game is a Windows OS build. If it is not, then the console simply won't show, but your game will run as normal.

# How To Use
Simply create a new root GameObject called `Console Window` and drag the `ConsoleWindow.cs` script onto it! But be careful since the `ConsoleWindow.cs` is marked as DontDestroyOnLoad. You can then adjust the settings as you like, and then simply go on with your game development journey. You can access the settings through the instance by calling `ConsoleWindow.Instance.`

The console will automatically show logs and will only appear when you open a Windows build that matches the [Target Build](https://github.com/SlushyRH/Unity-CMD-Console/tree/main?tab=readme-ov-file#Settings).

> [!NOTE]
> If you quit the game then the console will automatically close as well, and if you close the console, the game will close as well since they are linked. Hence why the script is marked as DontDestroyOnLoad so it will persist across scenes and not quit the game. The console will only be created when the scene with the object is loaded so I highly recommend to place the ConsoleWindow in the first scene of the game.

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/console.png" align="center">

## Settings
<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/inspector.png" align="center">

- **Target Build** controls when the console is allowed based on the type of build:
    - **None** means it will never show
    - **Development** means it will only show on development builds
    - **Standard** means it will only show on standard builds
    - **Everything** means it will show in both development and standard builds
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
<br>
<p>* If both <b>Include Log Type</b> and <b>Include Timestamp</b> are on. Then the LogType will come first and will be displayed in the console like this: [LogType] [Timestamp] Log Messager Here</p>