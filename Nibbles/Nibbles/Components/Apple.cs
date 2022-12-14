namespace Nibbles.Components
{
  public class Apple : ICanRender
  {
    public Position Position { get; set;  }

    public Apple(Position position)
    {
      Position = position;
    }
    
    public void Render()
    {
      Console.SetCursorPosition(Position.X, Position.Y);
      Console.Write("🍏");
    }
  }
}
