using Nibbles.Components;

namespace Nibbles.Scenes
{
  public abstract class Level : Scene
  {
    private readonly InputReader _inputReader;
    private readonly Game _game;
    private readonly View _view;
    private readonly Snake _snake;
    private readonly Apple _apple;
    private readonly Random _random = new();
    private bool _isGameOver;
    private IEnumerable<Position> Walls => Renderables.Where(renderable => renderable is Wall).Select(wall => (wall as Wall).Position);

    protected Level(InputReader inputReader, Game game, View view)
    {
      _inputReader = inputReader;
      _game = game;
      _view = view;

      _snake = new(view, (view.WindowWidth / 2, view.WindowHeight / 2), initialSize: 10);

      _apple = new(GetNextApplePosition());

      Renderables.Add(_snake);
      Renderables.Add(_apple);
    }

    private void GameOver()
    {
      _isGameOver = true;

      Renderables.Clear();
      Renderables.Add(PositionedText.Center("GAME OVER", _view));
      Renderables.Add(PositionedText.Center("Press Q for Main Menu", _view, (0, 1)));
    }

    public override void Enter()
    {
      base.Enter();

      _inputReader.Key += ReadKey;
      _snake.Dead += GameOver;
    }

    public override void Tick()
    {
      base.Tick();

      if (_isGameOver)
      {
        return;
      }

      _snake.Crawl();

      DetectCollisions();
    }

    private void DetectCollisions()
    {
      DetectWallCollision();
      DetectAppleCollision();
    }

    private void DetectWallCollision()
    {
      if (Walls.Contains(_snake.Head))
      {
        GameOver();
      }
    }

    private void DetectAppleCollision()
    {
      if (_snake.Head == _apple.Position)
      {
        _snake.Grow();
        _apple.Position = GetNextApplePosition();
      }
    }

    private Position GetNextApplePosition()
    {
      Position result = (0, 0);

      do
      {
        result = (_random.Next(0, _view.WindowWidth), _random.Next(0, _view.WindowHeight));
      } while (Walls.Contains(result));

      return result;
    }

    public override void Leave()
    {
      _snake.Dead -= GameOver;
      _inputReader.Key -= ReadKey;

      base.Leave();
    }

    private void ReadKey(ConsoleKey key)
    {
      switch (key)
      {
        case ConsoleKey.Q:
          _game.ChangeScene<MainMenuScene>();
          break;

        case ConsoleKey.W:
        case ConsoleKey.S:
        case ConsoleKey.A:
        case ConsoleKey.D:
          _snake.ChangePosition(key);
          break;
      }
    }
  }
}
