using StatusLine.Models;

namespace StatusLine.Components;

public interface IComponent
{
    TextModel[] TextArray { get; }
}