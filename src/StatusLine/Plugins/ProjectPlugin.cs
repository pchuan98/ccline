using StatusLine.ClaudeCode;
using StatusLine.Models;

namespace StatusLine.Plugins;

/// <summary>
/// Plugin that displays C# project information in the status line
/// Uses CommandHelper to detect .NET projects
/// </summary>
public class ProjectPlugin : IPlugin
{
    /// <summary>
    /// Current input context (set by DI container or manually)
    /// </summary>
    public StatuslineInput? Input { get; set; }

    /// <summary>
    /// Configuration for project plugin display
    /// </summary>
    public ProjectPluginConfig Config { get; set; } = new();

    /// <summary>
    /// Rendered string output for the status line
    /// </summary>
    public TextModel[] TextArray
    {
        get
        {
            if (!Config.IsEnabled || Input?.Workspace == null) return [];

            var result = new List<TextModel>();

            // Try to detect C# project first
            var csharpInfo = CommandHelper.GetDotNetProjectInfo();
            if (csharpInfo != null)
            {
                // C# project detected: show C# icon + project name or count
                if (Config.ShowIcon)
                {
                    result.Add(new TextModel(Icons.CSharp)
                    {
                        Foreground = Config.IconColor,
                        Opacity = Config.Opacity
                    });
                }

                string displayText;
                if (csharpInfo.ProjectCount == 1 && !string.IsNullOrEmpty(csharpInfo.ProjectName))
                {
                    // Single project: show project name
                    displayText = csharpInfo.ProjectName;
                }
                else
                {
                    // Multiple projects: show count
                    displayText = $"{csharpInfo.ProjectCount} projects";
                }

                result.Add(new TextModel($"{Config.Separator}{displayText}")
                {
                    Opacity = Config.Opacity
                });
            }
            else
            {
                // No C# project: show folder icon + folder name
                var projectDir = Input.Workspace.ProjectDir;
                if (string.IsNullOrEmpty(projectDir)) return [];

                var folderName = Path.GetFileName(projectDir);
                if (string.IsNullOrEmpty(folderName)) return [];

                if (Config.ShowIcon)
                {
                    result.Add(new TextModel(Icons.Folder)
                    {
                        Foreground = Config.IconColor,
                        Opacity = Config.Opacity
                    });
                }

                result.Add(new TextModel($"{Config.Separator}{folderName}")
                {
                    Opacity = Config.Opacity
                });
            }

            return result.ToArray();
        }
    }
}

/// <summary>
/// Configuration for ProjectPlugin behavior
/// </summary>
public class ProjectPluginConfig
{
    /// <summary>
    /// Whether the plugin is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Plugin name for identification
    /// </summary>
    public string Name { get; set; } = "Project";

    /// <summary>
    /// Whether to show icons
    /// </summary>
    public bool ShowIcon { get; set; } = true;

    /// <summary>
    /// Icon color for both C# and folder icons
    /// </summary>
    public System.Drawing.Color IconColor { get; set; } = System.Drawing.Color.Purple;

    /// <summary>
    /// Separator between icon and text
    /// </summary>
    public string Separator { get; set; } = " ";

    /// <summary>
    /// Opacity for colors (0.0 to 1.0)
    /// </summary>
    public double Opacity { get; set; } = 0.7;
}