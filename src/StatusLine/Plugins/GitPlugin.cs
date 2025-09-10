using StatusLine.ClaudeCode;
using StatusLine.Models;

namespace StatusLine.Plugins;

/// <summary>
/// Plugin that displays Git information in the status line
/// Configurable via GitPluginConfig for DI and AOT scenarios
/// </summary>
public class GitPlugin : IPlugin
{
    /// <summary>
    /// Current input context (set by DI container or manually)
    /// </summary>
    public StatuslineInput? Input { get; set; }

    /// <summary>
    /// Configuration for Git plugin display
    /// </summary>
    public GitPluginConfig Config { get; set; } = new();

    /// <summary>
    /// Rendered string output for the status line
    /// </summary>
    public TextModel[] TextArray
    {
        get
        {
            if (!Config.IsEnabled) return [];

            var status = CommandHelper.GetGitStatus();
            if (status.BranchName == null && !status.IsDetachedHead)
                return [];

            return BuildGitStatusModels(status);
        }
    }

    private TextModel[] BuildGitStatusModels(GitStatusInfo status)
    {
        var result = new List<TextModel>();

        // Branch information
        if (status.IsDetachedHead)
        {
            result.Add(new TextModel(Icons.Commit)
            {
                Foreground = Config.IconColor,
                Opacity = Config.Opacity
            });
            result.Add(new TextModel($"{Config.Placeholder}HEAD")
            {
                Opacity = Config.Opacity
            });
        }
        else
        {
            result.Add(new TextModel(Icons.Branch)
            {
                Foreground = Config.IconColor,
                Opacity = Config.Opacity
            });
            result.Add(new TextModel($"{Config.Placeholder}{status.BranchName}")
            {
                Opacity = Config.Opacity
            });
        }

        return result.ToArray();
    }
}

/// <summary>
/// Configuration for GitPlugin behavior
/// </summary>
public class GitPluginConfig
{
    /// <summary>
    /// Whether the plugin is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Plugin name for identification
    /// </summary>
    public string Name { get; set; } = "Git";

    /// <summary>
    /// String to insert between icon and result
    /// </summary>
    public string Placeholder { get; set; } = " ";

    /// <summary>
    /// Whether to show repository state (rebase, merge)
    /// </summary>
    public bool ShowRepositoryState { get; set; } = true;

    /// <summary>
    /// Whether to show file status (staged, unstaged, conflicts)
    /// </summary>
    public bool ShowFileStatus { get; set; } = true;

    /// <summary>
    /// Whether to show clean status when no changes
    /// </summary>
    public bool ShowCleanStatus { get; set; } = true;

    /// <summary>
    /// Opacity for all colors (0.0 to 1.0)
    /// </summary>
    public double Opacity { get; set; } = 0.7;

    /// <summary>
    /// Icon color for all Git icons
    /// </summary>
    public System.Drawing.Color IconColor { get; set; } = System.Drawing.Color.Yellow;
}