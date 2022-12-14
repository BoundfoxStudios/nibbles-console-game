namespace Nibbles.Components
{
  public enum Direction
  {
    Up,
    Down,
    Left,
    Right,
  }

  public class Snake : ICanRender
  {
    private readonly View _view;
    private Direction _direction;
    private readonly List<Position> _body = new();
    private int _remainingGrow = 0;
    public Position Head => _body.First();
    public IEnumerable<Position> Body => _body.Skip(1);
    public event Action Dead = delegate {  }; 

    public Snake(View view, Position position, Direction startDirection = Direction.Up, int initialSize = 1)
    {
      _view = view;
      _direction = startDirection;
      _remainingGrow = initialSize - 1;
      _body.Add(position);
    }

    // TODO: It would be better to have an SnakeInputReader class for that
    public void ChangePosition(ConsoleKey key)
    {
      switch (key)
      {
        case ConsoleKey.W:
          _direction = Direction.Up;
          break;

        case ConsoleKey.S:
          _direction = Direction.Down;
          break;

        case ConsoleKey.A:
          _direction = Direction.Left;
          break;

        case ConsoleKey.D:
          _direction = Direction.Right;
          break;
      }
    }

    public void Crawl()
    {
      Position newHead;

      switch (_direction)
      {
        case Direction.Up:
          newHead = Head.Up;
          break;
        case Direction.Down:
          newHead = Head.Down;
          break;
        case Direction.Left:
          newHead = Head.Left;
          break;
        case Direction.Right:
          newHead = Head.Right;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }

      newHead = ContainInPlayableArea(newHead);

      if (_body.Contains(newHead))
      {
        Dead.Invoke();
        return;
      }

      _body.Insert(0, newHead);

      if (_remainingGrow > 0)
      {
        _remainingGrow--;
      }
      else
      {
        _body.RemoveAt(_body.Count - 1);
      }
    }

    private Position ContainInPlayableArea(Position position)
    {
      if (position.X < 0)
      {
        position = (_view.WindowWidth, position.Y);
      }
      else if (position.X > _view.WindowWidth - 1)
      {
        position = (0, position.Y);
      }

      if (position.Y < 0)
      {
        position = (position.X, _view.WindowHeight - 1);
      }
      else if (position.Y > _view.WindowHeight)
      {
        position = (position.X, 0);
      }

      return position;
    }

    public void Render()
    {
      Console.SetCursorPosition(Head.X, Head.Y);
      Console.Write("🐍");

      foreach (var position in Body)
      {
        Console.SetCursorPosition(position.X, position.Y);
        Console.Write("🔹");
      }
    }

    public void Grow()
    {
      _remainingGrow++;
    }
  }
}
