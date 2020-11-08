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
        public short WindowWidth      { get; private set; } = (short)Console.WindowWidth;
        public short WindowHeight     { get; private set; } = (short)Console.WindowHeight;
        public int WindowLeft         { get; private set; } = Console.WindowLeft;
        public int WindowTop          { get; private set; } = Console.WindowTop;

        public string DebugFolderPath { get; private set; } = "logs";

        public int MaximumFpsCount    { get; private set; } = 30;

        // TODO -> set default
        public string FontName { get; private set; }
        public short FontWidth { get; private set; }
        public short FontHeight { get; private set; }

        public bool CursorVisible { get; private set; } = false;

        public Type RendererType { get; private set; }
        public Type OutputHandlerType { get; private set; }

        private CONSOLE_FONT_INFOEX outputFont;

        public GameBuilder SetRenderer<TRenderer, TOutputHandler>()
            where TRenderer : SurfaceRenderer
            where TOutputHandler : OutputHandler
        {
            RendererType = typeof(TRenderer);
            OutputHandlerType = typeof(TOutputHandler);

            outputFont = new CONSOLE_FONT_INFOEX();
            GetCurrentConsoleFontEx(GetStdHandle((uint)HANDLE.STD_OUTPUT_HANDLE), false, ref outputFont);

            FontName = outputFont.FaceName;
            FontWidth = outputFont.dwFontSize.X;
            FontHeight = outputFont.dwFontSize.Y;

            return this;
        }

        public GameBuilder SetTitle(string windowName)
        {
            WindowName = windowName;

            return this;
        }

        public GameBuilder SetFont(string fontName, short fontWidth, short fontHeight)
        {
            SetFontName(fontName);
            SetFontSize(fontWidth, fontHeight);

            return this;
        }

        public GameBuilder SetFontName(string fontName)
        {
            FontName = fontName;

            outputFont.FaceName = fontName;

            return this;
        }

        public GameBuilder SetFontSize(short fontWidth, short fontHeight)
        {
            FontWidth = fontWidth;
            FontHeight = fontHeight;

            outputFont.dwFontSize = new COORD(fontWidth, fontHeight);

            return this;
        }

        public GameBuilder SetWindowSize(short windowWidth, short windowHeight)
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

        public GameBuilder SetWindowRect(int windowLeft, int windowTop, short windowWidth, short windowHeight)
        {
            SetPosition(windowLeft, windowTop);
            SetWindowSize(windowWidth, windowHeight);

            return this;
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
            IntPtr hWnd = GetConsoleWindow();
            IntPtr hMenu = GetSystemMenu(hWnd, false);

            SetWindowPos(hWnd, IntPtr.Zero, WindowLeft, WindowTop, 0, 0, flags | (uint)SWP.SHOWWINDOW | (uint)SWP.NOSIZE);
            Console.SetWindowSize(WindowWidth, WindowHeight);

            Logger.DebugFolderPath = DebugFolderPath;

            OutputHandlerInfo outputHandlerInfo = new OutputHandlerInfo()
            {
                size = new COORD(WindowWidth, WindowHeight),
                font = outputFont,
                frameDelay = 1000 / MaximumFpsCount
            };

            OutputHandler outputHandler = (OutputHandler)Activator.CreateInstance(OutputHandlerType, outputHandlerInfo);
            SurfaceRenderer surfaceRenderer = (SurfaceRenderer)Activator.CreateInstance(RendererType, outputHandler, WindowWidth, WindowHeight, true);

            InputHandler inputHandler = new InputHandler();
            inputHandler.Enable();

            DeleteMenu(hMenu, (int)SC.MINIMIZE, (int)MF.BYCOMMAND);
            DeleteMenu(hMenu, (int)SC.MAXIMIZE, (int)MF.BYCOMMAND);
            DeleteMenu(hMenu, (int)SC.SIZE, (int)MF.BYCOMMAND);

            SetConsoleTitle(WindowName);

            Console.CursorVisible = false;

            Thread.Sleep(100);

            return new Game(surfaceRenderer, inputHandler);
        }
    }
}
