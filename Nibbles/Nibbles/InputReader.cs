namespace Nibbles
{
  public class InputReader
  {
    public event Action<ConsoleKey> Key = delegate {  };
    
    public void StartKeypressMonitor(CancellationToken cancellationToken)
    {
      Task.Run(async () =>
      {
        while (!cancellationToken.IsCancellationRequested)
        {
          if (Console.KeyAvailable)
          {
            var key = Console.ReadKey(true).Key;
            Key.Invoke(key);
          }

          await Task.Delay(10, cancellationToken);
        }
      }, cancellationToken);
    }
  }
}
