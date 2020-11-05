using System;
using System.Threading;

using Ax.Engine.Core;
using Ax.Engine.Core.Rendering;

using static Ax.Engine.Core.Native.WinApi;
using static Ax.Engine.Core.Native.WinUser;

namespace Ax.Engine
{
    public sealed class GameBuilder
    {
        public string WindowName      { get; private set; } = Console.Title;
        public int WindowWidth        { get; private set; } = Console.WindowWidth;
        public int WindowHeight       { get; private set; } = Console.WindowHeight;
        public int WindowLeft         { get; private set; } = Console.WindowLeft;
        public int WindowTop          { get; private set; } = Console.WindowTop;

        public string DebugFolderPath { get; private set; } = "logs";

        public int MaximumFpsCount    { get; private set; } = 30;

        // TODO -> set default
        public string FontName { get; private set; }
        public int FontWidth { get; private set; }
        public int FontHeight { get; private set; }

        public bool CursorVisible { get; private set; } = false;

        public Type RendererType { get; private set; }
        public Type OutputHandlerType { get; private set; }

        public GameBuilder SetRenderer<TRenderer, TOutputHandler>()
            where TRenderer : SurfaceRenderer
            where TOutputHandler : OutputHandler
        {
            RendererType = typeof(TRenderer);
            OutputHandlerType = typeof(TOutputHandler);

            return this;
        }

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

        public GameBuilder LimitFPS(int fps)
        {
            MaximumFpsCount = fps;

            return this;
        }

        public Game Build(uint flags = 0)
        {
            Logger.DebugFolderPath = DebugFolderPath;

            OutputHandler outputHandler = (OutputHandler)Activator.CreateInstance(OutputHandlerType);
            SurfaceRenderer surfaceRenderer = new QueuedSurfaceRenderer(outputHandler, WindowWidth, WindowHeight);

            InputHandler inputHandler = new InputHandler();

            bool isRunning = true;

            IntPtr hWnd = GetConsoleWindow();
            IntPtr hMenu = GetSystemMenu(hWnd, false);

            isRunning &= DeleteMenu(hMenu, (int)SC.MINIMIZE, (int)MF.BYCOMMAND);
            isRunning &= DeleteMenu(hMenu, (int)SC.MAXIMIZE, (int)MF.BYCOMMAND);
            isRunning &= DeleteMenu(hMenu, (int)SC.SIZE, (int)MF.BYCOMMAND);

            isRunning &= SetWindowPos(hWnd, new IntPtr(0), WindowLeft, WindowTop, 0, 0, flags | (uint)SWP.SHOWWINDOW | (uint)SWP.NOSIZE);
            isRunning &= SetConsoleTitle(WindowName);

            isRunning &= inputHandler.Enable();

            //outputHandler.Enable(ref logger, RenderingMode, FontName, FontWidth, FontHeight, CursorVisible, disableNewLineAutoReturn, 1000 / MaximumFpsCount)}");

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);

            Thread.Sleep(100);

            return new Game(surfaceRenderer, inputHandler, isRunning);
        }
    }
}
