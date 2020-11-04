using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;

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

        public int FPS;

        public string FontName;
        public int FontWidth;
        public int FontHeight;

        public bool CursorVisible = false;

        public OutputHandler.RenderingMode RenderingMode;
        public int RenderingThreadCount;

        public GameBuilder SetRenderingMode(OutputHandler.RenderingMode renderingMode)
        {
            RenderingMode = renderingMode;

            return this;
        }

        public GameBuilder SetRenderingThreadCount(int renderingThreadCount)
        {
            if(!RenderingMode.HasFlag(OutputHandler.RenderingMode.MultiThreaded))
            {
                throw new Exception("Please make sure to enable multi threaded rendering in order to set rendering thread count");
            }

            if(renderingThreadCount == 0 || renderingThreadCount % 2 != 0)
            {
                throw new Exception("Rendering thread count should be greater than 1 (since 1 = 1 thread) and a power of two");
            }

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
            FPS = fps;

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

            Logger.DebugFolderPath = DebugFolderPath;
            StringBuilder logger = new StringBuilder();

            bool isRunning = true;

            IntPtr hWnd = GetConsoleWindow();
            IntPtr hMenu = GetSystemMenu(hWnd, false);

            logger.AppendLine($"HWND           {hWnd}");
            logger.AppendLine($"HMENU          {hMenu}");
            
            logger.AppendLine($"DELMENU_MIN    {isRunning &= DeleteMenu(hMenu, (int)SC.MINIMIZE, (int)MF.BYCOMMAND)}");
            logger.AppendLine($"DELMENU_MAX    {isRunning &= DeleteMenu(hMenu, (int)SC.MAXIMIZE, (int)MF.BYCOMMAND)}");
            logger.AppendLine($"DELMENU_SIZ    {isRunning &= DeleteMenu(hMenu, (int)SC.SIZE, (int)MF.BYCOMMAND)}");

            OutputHandler outputHandler = new OutputHandler();
            InputHandler inputHandler = new InputHandler();

            logger.AppendLine($"SETWINPOS      {isRunning &= SetWindowPos(hWnd, new IntPtr(0), WindowLeft, WindowTop, 0, 0, flags | (uint)SWP.SHOWWINDOW | (uint)SWP.NOSIZE)}");
            logger.AppendLine($"SETWINTXT      {isRunning &= SetConsoleTitle(WindowName)}");

            logger.AppendLine($"INHANDLER      {isRunning &= inputHandler.Enable(ref logger)}");
            logger.AppendLine($"OUTHANDLER     {outputHandler.Enable(ref logger, RenderingMode, FontName, FontWidth, FontHeight, CursorVisible, disableNewLineAutoReturn, 1000 / FPS)}");

            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);

            Thread.Sleep(100);

            Game.WindowWidth = WindowWidth;
            Game.WindowHeight = WindowHeight;

            Game.WindowWidthInPixels = WindowWidth * FontWidth;
            Game.WindowHeightInPixels = WindowHeight * FontHeight;

            Game.FontWidth = FontWidth;
            Game.FontHeight = FontHeight;

            Logger.Write(logger.ToString());

            return new Game(hWnd, hMenu, outputHandler, inputHandler, isRunning);
        }
    }
}
