using System.Drawing;
using StatusLine.Models;

namespace StatusLine.Components;

public static class ProgressBarThemes
{
    public static Func<double, Color> Red =>
        d => Color.FromArgb(255, (int)(200 * d) + 50, 10, 10);

    public static Func<double, Color> Green2Red =
        d => Color.FromArgb(255, 160, 255 - (int)(250 * d), 0);
}

/// <summary>
/// Provides ASCII progress bar display functionality for status lines
/// </summary>
/// <remarks>
/// Creates customizable progress bars using Unicode block characters with color support
/// </remarks>
public class ProgressBar(Func<double, Color>? handle = null, double opacity = 1) : IComponent
{
    public const string EmptyBlock = "░";
    public const string EmptyCircle = "○";
    public const string FillBlock = "▓";
    public const string FillFullBlock = "█";
    public const string FillCircle = "●";

    /// <summary>
    /// Character used for filled portions of the progress bar
    /// </summary>
    /// <value>Default is full block character (█)</value>
    public string FilledChar { get; set; } = FillFullBlock;

    /// <summary>
    /// Character used for empty portions of the progress bar
    /// </summary>
    /// <value>Default is light shade block character (░)</value>
    public string EmptyChar { get; set; } = EmptyBlock;

    /// <summary>
    /// Current progress value between 0.0 and 1.0
    /// </summary>
    /// <value>0.0 = empty, 1.0 = full</value>
    public double Value { get; set; } = 0.0;

    /// <summary>
    /// Width of the progress bar in characters
    /// </summary>
    /// <value>Must be greater than 0</value>
    public int Width { get; set; } = 20;

    /// <summary>
    /// Background color of the progress bar
    /// </summary>
    public Color EmptyColor { get; set; } = Color.Gray;

    /// <summary>
    /// Foreground color of the progress bar (overridden by ColorMap if defined)
    /// </summary>
    public Color FillColor { get; set; } = Color.Empty;

    /// <summary>
    /// Gets the rendered progress bar as TextModel array for display
    /// </summary>
    /// <returns>Single-element array containing the formatted progress bar</returns>
    /// <example>
    /// var bar = new ProgressBar { Value = 0.7, Width = 10 };
    /// foreach (var text in bar.TextArray) text.Write();
    /// </example>
    public TextModel[] TextArray =>
        Enumerable.Range(0, Width)
            .Select(i => (double)i / (Width - 1))
            .Select(i => (i, i > Value ? EmptyColor : handle?.Invoke(i) ?? FillColor))
            .Select(t => new TextModel(t.i > Value ? EmptyChar : FilledChar)
            {
                Foreground = t.Item2,
                Opacity = opacity
            }).ToArray();
}