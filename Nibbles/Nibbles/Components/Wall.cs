namespace Nibbles.Components
{
  public class Wall : ICanRender
  {
    public Position Position { get; }

    public Wall(Position position)
    {
      Position = position;
    }
    
    public void Render()
    {
      Console.SetCursorPosition(Position.X, Position.Y);
      Console.Write("⬜️");
    }
  }
}
