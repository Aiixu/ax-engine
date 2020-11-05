using System;
using System.Threading;

using Ax.Engine.Core;
using Ax.Engine.Core.Rendering;
using static Ax.Engine.Core.Native;

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

        public int MaximumFpsCount    { get; set; } = 30;

        // TODO -> set default
        public string FontName { get; set; }
        public int FontWidth { get; set; }
        public int FontHeight { get; set; }

        public bool CursorVisible { get; set; } = false;

        public Type Renderer { get; set; } = typeof(QueuedSurfaceRenderer);

        public GameBuilder SetRenderer(Type renderer) 
        {
            if(!typeof(ISurfaceRenderer).IsAssignableFrom(renderer))
            {
                throw new Exception($"{renderer} doesn't inherit from ISurfaceRenderer");
            }

            Renderer = renderer;

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

        public Game Build(bool disableNewLineAutoReturn = false, uint flags = 0)
        {
            Logger.DebugFolderPath = DebugFolderPath;

            OutputHandler outputHandler = new OutputHandler();
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

            Game.WindowWidth = WindowWidth;
            Game.WindowHeight = WindowHeight;

            Game.WindowWidthInPixels = WindowWidth * FontWidth;
            Game.WindowHeightInPixels = WindowHeight * FontHeight;

            Game.FontWidth = FontWidth;
            Game.FontHeight = FontHeight;

            return new Game(hWnd, hMenu, outputHandler, inputHandler, isRunning);
        }
    }
}
