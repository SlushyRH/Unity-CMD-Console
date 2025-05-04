<h1 align="center">Unity Runtime CMD Console</h1>

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/showcase.gif" align="center">

[ConsoleWindow.cs](https://github.com/SlushyRH/Unity-CMD-Console/blob/main/ConsoleWindow.cs) is a simple MonoBehaviour which will initialize a CMD window that shows all logs from Unity's Debug class. This is useful for people trying to debug their code in a build, and especially useful for people who have more than 1 monitor as the CMD console is an external window meaning it can be dragged across monitors.

> [!WARNING]
> The console will only open if the game is a Windows OS build. If it is not, then the console simply won't show, but your game will run as normal.

# How To Use
Simply create a new root GameObject called `Console Window` and drag the `ConsoleWindow.cs` script onto it! You can then adjust the settings as you like, and then simply go on with your game development journey. You can access the settings through the instance by calling `ConsoleWindow.Instance.`

The `Console Window` GameObject will be marked as DontDestroyOnLoad so it's best to to place the GameObject in the first scene of the game so it persists across all scenes. If the GameObject is in multiple scenes then only one instance of it will stay alive so no need worry about duplicates.

The console will automatically show logs and will only appear when you open a Windows build that matches the [Target Build](https://github.com/SlushyRH/Unity-CMD-Console/tree/main?tab=readme-ov-file#Settings).

> [!NOTE]
> Since the game window and the console are linked, if you close one, then it will close the other.

<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/console.png" align="center">

## Settings
<img src="https://github.com/SlushyRH/Unity-CMD-Console/blob/main/readme/inspector.png" align="center">

- **Console Window Name** controls the name of the console window. If empty, it will display the path to the game's exe
- **Target Build** controls when the console is allowed based on the type of build:
    - **None** means it will never show
    - **Development** means it will only show on development builds (This is the default)
    - **Standard** means it will only show on standard builds
    - **Everything** means it will show in both development and standard builds
- **Show Exception Stack Trace** will add the stack trace from an exception log to the console.
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
<p>If both <b>Include Log Type</b> and <b>Include Timestamp</b> are on. Then the LogType will come first and will be displayed in the console like this: [LogType] [Timestamp] Log Messager Here</p>