using Nibbles.Scenes;

namespace Nibbles
{
  public class View
  {
    public int WindowHeight { get; }
    public int WindowWidth { get; }

    public View()
    {
      WindowHeight = Console.WindowHeight;
      WindowWidth = Console.WindowWidth;

      // if (WindowWidth > WindowHeight * 3)
      // {
      //   WindowWidth = WindowHeight * 3;
      // }

      Console.CursorVisible = false;
    }


    public void Render(Scene scene)
    {
      Console.Clear();
      scene.Render();
    }
  }
}
