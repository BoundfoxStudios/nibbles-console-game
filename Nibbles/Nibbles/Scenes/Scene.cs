namespace Nibbles.Scenes
{
  public abstract class Scene : ICanRender
  {
    protected readonly List<ICanRender> Renderables = new();
    
    public virtual void Enter()
    {
      // Empty.
    }

    public virtual void Tick()
    {
      // Empty.
    }

    public virtual void Leave()
    {
      // Empty.
    }

    public virtual void Render()
    {
      foreach (var renderable in Renderables)
      {
        renderable.Render();
      }
    }
  }
}
