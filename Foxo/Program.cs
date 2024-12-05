using Foxo;
using FoxoLib;
using System.Linq.Expressions;

/* TODO: Redraw content on window size change
var listenerThread = new Thread(DisplayHelper.WindowSizeChangeListener)
{
    IsBackground = true
};
listenerThread.Start();
*/

Console.CursorVisible = true;
Console.BackgroundColor = ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.Black;

MenuHelper.MainMenu();