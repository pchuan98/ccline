using System.Drawing;
using System.Text;
using StatusLine.Models;

namespace StatusLine;

// ReSharper disable All

/// <summary>
/// Provides ANSI color output functionality for status lines
/// </summary>
public static class Helper
{
    private const string Reset = "\e[0m";

    public static void WriteReset() => Console.Write(Reset);

    /// <summary>
    /// Mixes foreground color with background color using alpha blending
    /// </summary>
    /// <param name="fg">Foreground color</param>
    /// <param name="bg">Background color</param>
    /// <param name="alpha">Alpha value (0.0 = fully background, 1.0 = fully foreground)</param>
    /// <returns>Blended color</returns>
    private static Color BlendColors(Color fg, Color bg, double alpha)
    {
        alpha = Math.Max(0, Math.Min(1, alpha));

        var r = (int)(bg.R * (1 - alpha) + fg.R * alpha);
        var g = (int)(bg.G * (1 - alpha) + fg.G * alpha);
        var b = (int)(bg.B * (1 - alpha) + fg.B * alpha);

        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Formats text with ANSI color codes for terminal display
    /// </summary>
    /// <param name="text">Text content to format</param>
    /// <param name="fg">Foreground color, defaults to White if Color.Empty</param>
    /// <param name="bg">Background color, defaults to Black if Color.Empty</param>
    /// <param name="alpha">Transparency level (0.0-1.0), blends fg with bg when less than 1.0</param>
    /// <returns>ANSI-formatted string ready for terminal output</returns>
    /// <example>
    /// var coloredText = Helper.Format("Hello", Color.Red, Color.Blue, 0.8);
    /// Console.Write(coloredText);
    /// </example>
    public static string Format(string text, Color fg, Color bg, double alpha = 1)
    {
        var result = new StringBuilder();

        fg = fg == Color.Empty ? Color.White : fg;
        bg = bg == Color.Empty ? Color.Black : bg;

        // Apply alpha blending to simulate transparency
        if (alpha < 1)
            fg = BlendColors(fg, bg, alpha);

        result.Append($"\e[38;2;{fg.R};{fg.G};{fg.B}m");
        result.Append($"\e[48;2;{bg.R};{bg.G};{bg.B}m");

        result.Append(text);
        result.Append(Reset);

        return result.ToString();
    }

    /// <summary>
    /// Initializes console for UTF-8 encoding and reads all stdin input
    /// </summary>
    /// <returns>Complete stdin content as string</returns>
    /// <remarks>Call this once at program start to configure console encoding and capture Claude Code session data</remarks>
    /// <example>
    /// var sessionData = Helper.Initialize();
    /// var context = JsonSerializer.Deserialize&lt;SessionContext&gt;(sessionData);
    /// </example>
    public static string Initialize()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        return Console.In.ReadToEnd();
    }

    public static void Write(this TextModel[] models)
    {
        foreach (var model in models)
            model.Write();
    }

    public static void Write(this Exception e, string header = "", string msg = "")
    {
        if (e == null) return;

        WriteReset();

        // Use custom header or fallback to exception type
        var displayHeader = !string.IsNullOrEmpty(header) ? header : $"[{e.GetType().Name}]";
        var displayMessage = !string.IsNullOrEmpty(msg) ? msg : e.Message;

        var errorHeader = Format($"{displayHeader} ", Color.Red, Color.Empty, 1.0);
        var errorMessage = Format(displayMessage, Color.Red, Color.Empty, 0.9);

        Console.Write(errorHeader);
        Console.WriteLine(errorMessage);

        // Stack trace in dimmed white for readability
        if (!string.IsNullOrEmpty(e.StackTrace))
        {
            var stackTrace = Format(e.StackTrace, Color.White, Color.Empty, 0.6);
            Console.WriteLine(stackTrace);
        }

        // Inner exception with recursion and indentation
        if (e.InnerException != null)
        {
            var innerLabel = Format("  Caused by: ", Color.Yellow, Color.Empty, 0.8);
            Console.Write(innerLabel);

            // Recursively write inner exception
            e.InnerException.Write();
        }

        WriteReset();
    }

    public static void SplitWrite(string split = " | ") => new TextModel(split)
    {
        Opacity = 0.8
    }.Write();


}