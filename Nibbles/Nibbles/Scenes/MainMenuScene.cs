using Nibbles.Components;

namespace Nibbles.Scenes
{
  public class MainMenuScene : Scene
  {
    private readonly InputReader _inputReader;
    private readonly ButtonList _buttonList;

    public MainMenuScene(InputReader inputReader, Game game)
    {
      _inputReader = inputReader;

      _buttonList = new(
        (0, 0),
        new ButtonListItem() { Text = "Level 1", Action = game.ChangeScene<Level1> },
        new ButtonListItem() { Text = "Level 2", Action = game.ChangeScene<Level2> },
        new ButtonListItem() { Text = "Exit", Action = game.Exit }
      );
      Renderables.Add(_buttonList);
    }

    public override void Enter()
    {
      base.Enter();

      _inputReader.Key += ReadKey;
    }

    public override void Leave()
    {
      _inputReader.Key -= ReadKey;

      base.Leave();
    }

    private void ReadKey(ConsoleKey key)
    {
      switch (key)
      {
        case ConsoleKey.W:
          _buttonList.SelectUp();
          break;

        case ConsoleKey.S:
          _buttonList.SelectDown();
          break;
        
        case ConsoleKey.Q:
          _buttonList.SelectedIndex = 2;
          _buttonList.Select();
          break;

        case ConsoleKey.Spacebar:
          _buttonList.Select();
          break;
      }
    }
  }
}
