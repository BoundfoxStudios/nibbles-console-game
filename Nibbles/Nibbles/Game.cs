using Microsoft.Extensions.DependencyInjection;
using Nibbles.Scenes;

namespace Nibbles
{
  public class Game
  {
    private readonly TimeSpan _tickRate = TimeSpan.FromMilliseconds(100);
    private readonly InputReader _inputReader;
    private readonly CancellationTokenSource _gameCancellationTokenSource = new();

    private readonly View _view;
    private Scene? _activeScene;
    private readonly ServiceProvider _services;

    public Game()
    {
      var serviceCollection = new ServiceCollection();
      serviceCollection
        .AddSingleton<View>()
        .AddSingleton<InputReader>()
        .AddSingleton(this)
        .AddTransient<MainMenuScene>()
        .AddTransient<Level1>()
        .AddTransient<Level2>();

      _services = serviceCollection.BuildServiceProvider();

      _view = _services.GetRequiredService<View>();
      _inputReader = _services.GetRequiredService<InputReader>();
    }

    public void Exit()
    {
      _gameCancellationTokenSource.Cancel();
    }

    public async Task StartAsync()
    {
      _inputReader.StartKeypressMonitor(_gameCancellationTokenSource.Token);

      ChangeScene<MainMenuScene>();

      do
      {
        _activeScene!.Tick();
        _view.Render(_activeScene!);
        await Task.Delay(_tickRate);
      } while (!_gameCancellationTokenSource.IsCancellationRequested);

      Console.Clear();
      Console.WriteLine("Bye, danke für's Spielen!");
    }
    
    public void ChangeScene<T>()
      where T : Scene
    {
      _activeScene?.Leave();
      _activeScene = null;

      _activeScene = _services.GetRequiredService<T>();

      _activeScene.Enter();
    }
  }
}
