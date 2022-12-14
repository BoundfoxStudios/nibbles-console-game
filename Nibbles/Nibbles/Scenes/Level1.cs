using Nibbles.Components;

namespace Nibbles.Scenes
{
  public class Level1 : Level
  {
    public Level1(InputReader inputReader, Game game, View view) : base(inputReader, game, view)
    {
      Position wallPosition = (0, 0);

      while (wallPosition.X < view.WindowWidth)
      {
        Renderables.Add(new Wall(wallPosition));
        Renderables.Add(new Wall((wallPosition.X, view.WindowHeight)));
        wallPosition = wallPosition.Right;
      }

      wallPosition = (0, 1);
      
      while (wallPosition.Y < view.WindowHeight - 1)
      {
        Renderables.Add(new Wall(wallPosition));
        Renderables.Add(new Wall((view.WindowWidth, wallPosition.Y)));
        wallPosition = wallPosition.Down;
      }
    }
  }
}
