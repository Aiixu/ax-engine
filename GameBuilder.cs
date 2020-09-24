using System.Threading;
using System;

using Ax.Engine.Core;

using static Ax.Engine.Core.Native;
using static Ax.Engine.Utils.DefaultValue;

namespace Ax.Engine
{
    public sealed class GameBuilder
    {
        public string WindowName;
        public int WindowWidth;
        public int WindowHeight;
        public int WindowLeft;
        public int WindowTop;

        public string DebugFolderPath;

        public string FontName;
        public int FontWidth;
        public int FontHeight;

        public bool CursorVisible { get; set; } = false;

        public GameBuilder SetTitle(string windowName)
        {
            WindowName = windowName;

            return this;
        }

        public GameBuilder SetFont(string fontName, int fontWidth = -1, int fontHeight = -1)
        {
            FontName = fontName;
            FontWidth = fontWidth;
            FontHeight = fontHeight;

            return this;
        }

        public GameBuilder SetSize(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;

            return this;
        }

        public GameBuilder SetPosition(int windowLeft, int windowTop)
        {
            WindowLeft = windowLeft;
            WindowTop = windowTop;

            return this;
        }

        public GameBuilder SetWindowRect(int windowLeft, int windowTop, int windowWidth, int windowHeight)
        {
            return SetPosition(windowLeft, windowTop).SetSize(windowWidth, windowHeight);
        }

        public GameBuilder SetCursorVisible(bool cursorVisible)
        {
            CursorVisible = cursorVisible;

            return this;
        }

        public GameBuilder Debug(string debugFolderPath)
        {
            DebugFolderPath = debugFolderPath;

            return this;
        }

        public Game Build(bool disableNewLineAutoReturn = false, uint flags = 0)
        {
            Default(ref WindowName, StringNotNullOrEmpty, Console.Title);
            Default(ref WindowWidth,IntegerPositive, Console.WindowWidth);
            Default(ref WindowHeight,IntegerPositive, Console.WindowHeight);
            Default(ref WindowLeft, IntegerPositiveOrZero, Console.WindowLeft);
            Default(ref WindowTop, IntegerPositiveOrZero, Console.WindowTop);
            Default(ref DebugFolderPath, StringNotNullOrEmpty, "logs");

            bool isRunning = true;

            IntPtr hWnd = GetConsoleWindow();
            IntPtr hMenu = GetSystemMenu(hWnd, false);

            isRunning &= DeleteMenu(hMenu, (int)SC.MINIMIZE, (int)MF.BYCOMMAND);
            isRunning &= DeleteMenu(hMenu, (int)SC.MAXIMIZE, (int)MF.BYCOMMAND);
            isRunning &= DeleteMenu(hMenu, (int)SC.SIZE, (int)MF.BYCOMMAND);

            OutputHandler outputHandler = new OutputHandler();
            InputHandler inputHandler = new InputHandler();

            isRunning &= SetWindowPos(hWnd, new IntPtr(0), WindowLeft, WindowTop, 0, 0, flags | (uint)SWP.SHOWWINDOW | (uint)SWP.NOSIZE);
            isRunning &= SetConsoleTitle(WindowName);

            isRunning &= inputHandler.Enable();
            isRunning &= outputHandler.Enable(FontName, FontWidth, FontHeight, CursorVisible, disableNewLineAutoReturn);

            Logger.DebugFolderPath = DebugFolderPath;

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);

            Thread.Sleep(100);

            return new Game(hWnd, hMenu, outputHandler, inputHandler, isRunning);
        }
    }
}
