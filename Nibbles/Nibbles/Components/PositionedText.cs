namespace Nibbles.Components
{
  public class PositionedText : ICanRender
  {
    private readonly string _text;
    private readonly Position _position;

    public PositionedText(string text, Position position)
    {
      _text = text;
      _position = position;
    }
    
    public void Render()
    {
      Console.SetCursorPosition(_position.X, _position.Y);
      Console.Write(_text);
    }

    public static PositionedText Center(string text, View view) => Center(text, view, (0, 0));
    public static PositionedText Center(string text, View view, Position offset)
    {
      var textLength = text.Length;
      return new(text, (view.WindowWidth / 2 - textLength / 2 + offset.X, view.WindowHeight / 2 - 1 + offset.Y));
    }
  }
}
