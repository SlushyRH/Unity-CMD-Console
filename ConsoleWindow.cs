using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SRH.Utility
{
    [DisallowMultipleComponent]
    [AddComponentMenu("SRH/Utility/Console Window")]
    public class ConsoleWindow : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private TargetConsoleBuild targetBuild = TargetConsoleBuild.Development;
        [SerializeField] private bool showExceptionStackTrace = true;
        [SerializeField] private bool includeLogType = false;
        [SerializeField] private bool includeTimestamp = true;

        [Header("Log Colours")]
        [SerializeField] private Color32 infoTxtColour = Color.white;
        [SerializeField] private Color32 warningTxtColour = Color.yellow;
        [SerializeField] private Color32 errorTxtColour = Color.red;
        [SerializeField] private Color32 exceptionTxtColour = Color.red;
        [SerializeField] private Color32 assertTxtColour = Color.white;
        [Space]
        [SerializeField] private Color32 stackTraceTxtColour = Color.red;

        private ConsoleColor ccInfoTxtColour;
        private ConsoleColor ccWarningTxtColour;
        private ConsoleColor ccErrorTxtColour;
        private ConsoleColor ccExceptionTxtColour;
        private ConsoleColor ccAssertTxtColour;
        private ConsoleColor ccStackTraceTxtColour;

        private StreamWriter writer;
        private bool consoleAllocated;

        private static ConsoleWindow _instance;

        public static ConsoleWindow Instance => _instance;

        public Color32 InfoTxtColour
        {
            get => infoTxtColour;
            set
            {
                infoTxtColour = value;
                ccInfoTxtColour = GetClosestConsoleColour(infoTxtColour);
            }
        }

        public Color32 WarningTxtColour
        {
            get => warningTxtColour;
            set
            {
                warningTxtColour = value;
                ccWarningTxtColour = GetClosestConsoleColour(warningTxtColour);
            }
        }

        public Color32 ErrorTxtColour
        {
            get => errorTxtColour;
            set
            {
                errorTxtColour = value;
                ccErrorTxtColour = GetClosestConsoleColour(errorTxtColour);
            }
        }

        public Color32 ExceptionTxtColour
        {
            get => exceptionTxtColour;
            set
            {
                exceptionTxtColour = value;
                ccExceptionTxtColour = GetClosestConsoleColour(exceptionTxtColour);
            }
        }

        public Color32 AssertTxtColour
        {
            get => assertTxtColour;
            set
            {
                assertTxtColour = value;
                ccAssertTxtColour = GetClosestConsoleColour(assertTxtColour);
            }
        }

        public Color32 StackTraceTxtColour
        {
            get => stackTraceTxtColour;
            set
            {
                stackTraceTxtColour = value;
                ccStackTraceTxtColour = GetClosestConsoleColour(stackTraceTxtColour);
            }
        }

        public bool IncludeLogType { get => includeLogType; set => includeLogType = value; }

        public bool IncludeTimestamp { get => includeTimestamp; set => includeTimestamp = value; }

        public bool Pause { get; set; } = false;

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
#endif

        public void Awake()
        {
            // init singleton to ensure only one console instance exists
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
                return;
            }

#if UNITY_EDITOR
            return;
#endif
            // check if console is allowed and this is enabled
            if (!IsBuildAllowed() || !enabled)
                return;

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            // allocate new window console
            AllocConsole();
            consoleAllocated = true;
#endif

            // create writer and direct output to console
            writer = new StreamWriter(Console.OpenStandardOutput())
            {
                AutoFlush = true
            };
            Console.SetOut(writer);

            // convert colours to the cc varients
            ccInfoTxtColour = GetClosestConsoleColour(infoTxtColour);
            ccWarningTxtColour = GetClosestConsoleColour(warningTxtColour);
            ccErrorTxtColour = GetClosestConsoleColour(errorTxtColour);
            ccExceptionTxtColour = GetClosestConsoleColour(exceptionTxtColour);
            ccAssertTxtColour = GetClosestConsoleColour(assertTxtColour);
            ccStackTraceTxtColour = GetClosestConsoleColour(stackTraceTxtColour);

            // listen to log messages
            Application.logMessageReceived += HandleLog;

            // cleanup console on app exit in case of no monobehaviour exit method called
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                CleanupConsole();
            };
        }

        private void OnApplicationQuit()
        {
            CleanupConsole();
        }

        public void OnDestroy()
        {
            CleanupConsole();
        }

        private void CleanupConsole()
        {
            if (writer != null)
            {
                writer.Flush();
                writer.Close();
                writer = null;
            }

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            if (consoleAllocated)
            {
                try
                {
                    // try to dealloate console
                    FreeConsole();
                    consoleAllocated = false;
                }
                catch (Exception e)
                {
                    Debug.LogError($"!FATAL! Failed to deallocate the console: {e.Message}");
                }
            }
#endif
        }

        private void HandleLog(string msg, string stackTrace, LogType type)
        {
            // dont log to console if paused
            if (Pause)
                return;

            // get original colour
            ConsoleColor ogColor = Console.ForegroundColor;

            // apply colour based on log type
            if (type == LogType.Log)
                Console.ForegroundColor = ccInfoTxtColour;
            else if (type == LogType.Warning)
                Console.ForegroundColor = ccWarningTxtColour;
            else if (type == LogType.Error)
                Console.ForegroundColor = ccErrorTxtColour;
            else if (type == LogType.Exception)
                Console.ForegroundColor = ccExceptionTxtColour;
            else if (type == LogType.Assert)
                Console.ForegroundColor = ccAssertTxtColour;
            else
                Console.ForegroundColor = ogColor;

            // create log type and timestamp if needed
            string logType = includeLogType ? $"[{type.ToString()}] " : "";
            string timestamp = includeTimestamp ? $"[{DateTime.Now.ToString("HH:mm:ss")}] " : "";

            // write to the streamwriter and reset colour
            writer.WriteLine($"{logType}{timestamp}{msg}");

            if (showExceptionStackTrace && !string.IsNullOrEmpty(stackTrace))
            {
                Console.ForegroundColor = ccStackTraceTxtColour;
                writer.WriteLine(stackTrace);
            }

            Console.ForegroundColor = ogColor;
        }

        private bool IsBuildAllowed()
        {
#if DEVELOPMENT_BUILD
            return targetBuild.HasFlag(TargetConsoleBuild.Development);
#else
            return targetBuild.HasFlag(TargetConsoleBuild.Standard);
#endif
        }

        // from https://stackoverflow.com/questions/1988833/converting-color-to-consolecolor
        private ConsoleColor GetClosestConsoleColour(Color32 colour)
        {
            // init
            ConsoleColor consoleColour = 0;
            double delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                // get console colour name and actual colour
                string ccName = Enum.GetName(typeof(ConsoleColor), cc);
                var ccColour = System.Drawing.Color.FromName(ccName);

                double tDelta = Math.Pow(ccColour.R - colour.r, 2.0) + Math.Pow(ccColour.G - colour.g, 2.0) + Math.Pow(ccColour.B - colour.b, 2.0);

                if (tDelta == 0)
                    return consoleColour;

                if (tDelta < delta)
                {
                    delta = tDelta;
                    consoleColour = cc;
                }
            }

            // return closest console colour
            return consoleColour;
        }
    }

    [Flags]
    public enum TargetConsoleBuild
    {
        None = 0,
        Development = 1 << 0,
        Standard = 1 << 1,
    }
}