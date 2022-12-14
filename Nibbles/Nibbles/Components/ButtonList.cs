namespace Nibbles.Components
{
  public class ButtonListItem
  {
    public Action Action { get; init; } = default!;
    public string Text { get; init; } = default!;
  }
  
  public class ButtonList : ICanRender
  {
    private readonly Position _position;
    private readonly ButtonListItem[] _buttonListItems;
    public int SelectedIndex { get; set; }

    public ButtonList(Position position, params ButtonListItem[] buttonListItems)
    {
      _position = position;
      _buttonListItems = buttonListItems;
    }

    public void SelectUp()
    {
      if (SelectedIndex > 0)
      {
        SelectedIndex--;
      }
    }

    public void SelectDown()
    {
      if (SelectedIndex + 1 < _buttonListItems.Length)
      {
        SelectedIndex++;
      }
    }

    public void Select()
    {
      _buttonListItems[SelectedIndex].Action();
    }

    public void Render()
    {
      for (var index = 0; index < _buttonListItems.Length; index++)
      {
        var text = _buttonListItems[index];
        Console.SetCursorPosition(_position.X, _position.Y + index);
        Console.Write($"{(index == SelectedIndex ? ">" : " ")} {text.Text}");
      }
    }
  }
}
