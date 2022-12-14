using Nibbles.Components;

namespace Nibbles.Scenes
{
  public class Level2 : Level1
  {
    public Level2(InputReader inputReader, Game game, View view) : base(inputReader, game, view)
    {
      var yPadding = 4;
      var columns = view.WindowWidth / 15;

      for (var x = 0; x < columns; x++)
      {
        for (var y = yPadding; y < view.WindowHeight - yPadding; y++)
        {
          Renderables.Add(new Wall((x * columns, y)));
        }
      }
    }
  }
}
