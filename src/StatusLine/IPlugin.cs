using StatusLine.ClaudeCode;
using StatusLine.Models;

namespace StatusLine;

/// <summary>
/// Simple interface for statusline plugins designed for AOT-friendly DI container registration
/// </summary>
public interface IPlugin
{
    /// <summary>
    /// Current input context (set by DI container)
    /// </summary>
    StatuslineInput? Input { get; set; }

    /// <summary>
    /// Rendered string output for the status line
    /// </summary>
    TextModel[] TextArray { get; }
}