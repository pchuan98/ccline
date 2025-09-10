using System.Drawing;

namespace StatusLine.Models;

/// <summary>
/// 表示带有样式和格式化选项的文本模型
/// </summary>
/// <remarks>
/// 用于创建和控制台状态栏文本，支持颜色、背景色、对齐方式和宽度限制
/// </remarks>
public class TextModel(string content = "")
{
    /// <summary>
    /// 获取或设置背景色
    /// </summary>
    /// <value>Color.Empty 表示无背景色</value>
    public Color Backgroud { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double Opacity { get; set; } = 1;

    /// <summary>
    /// 获取或设置前景色（文字颜色）
    /// </summary>
    /// <value>Color.Empty 表示使用默认颜色</value>
    public Color Foreground { get; set; }

    /// <summary>
    /// 获取或设置要显示的文本内容
    /// </summary>
    /// <value>默认为空字符串</value>
    /// <example>
    /// model.Content = "Hello World";
    /// </example>
    public string Content { get; set; } = content;

    /// <summary>
    /// 获取或设置文本宽度限制
    /// </summary>
    /// <value>小于等于 0 表示无限制，大于 0 时会截断或填充文本</value>
    /// <example>
    /// model.Width = 20; // 限制宽度为 20 个字符
    /// </example>
    public int Width { get; set; } = 0;

    /// <summary>
    /// 获取或设置文本对齐方式
    /// </summary>
    /// <value>true 表示左对齐，false 表示右对齐</value>
    /// <remarks>仅在 Width 属性大于 0 时生效</remarks>
    /// <example>
    /// model.IsLeftAlign = false; // 右对齐
    /// </example>
    public bool IsLeftAlign { get; set; } = true;

    /// <summary>
    /// 获取格式化后的文本，包含颜色、背景色和对齐方式
    /// </summary>
    /// <value>返回带有 ANSI 颜色代码的格式化文本</value>
    /// <remarks>
    /// 此属性会根据 Width、IsLeftAlign、Foreground 和 Backgroud 属性
    /// 自动应用文本格式化和样式
    /// </remarks>
    /// <example>
    /// var model = new TextModel 
    /// {
    ///     Content = "Hello",
    ///     Width = 10,
    ///     Foreground = Color.Red
    /// };
    /// Console.WriteLine(model.Render); // 输出带颜色的格式化文本
    /// </example>
    public string Render
    {
        get
        {
            var formattedContent = Content;

            if (Width > 0)
            {
                formattedContent = Content.Length < Width
                    ? Content.Substring(0, Width)
                    : IsLeftAlign ? Content.PadRight(Width) : Content.PadLeft(Width);

            }

            return Helper.Format(
                formattedContent, Foreground, Backgroud, Opacity
            );
        }
    }

    /// <summary>
    /// 将格式化后的文本输出到控制台
    /// </summary>
    /// <remarks>
    /// 此方法会输出带有颜色和格式的文本到控制台，不会在末尾添加换行符
    /// 如果需要换行，请使用 Console.WriteLine() 或在 Content 中包含换行符
    /// </remarks>
    public void Write()
    {
        Helper.WriteReset();
        Console.Write(Render);
    }
}