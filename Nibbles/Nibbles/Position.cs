using System.Diagnostics;

namespace Nibbles
{
  [DebuggerDisplay("{X}/{Y}")]
  public readonly struct Position
  {
    private readonly int _x;

    public int X
    {
      get => _x;
      init => _x = value - value % 2;
    }

    public Position Right => (X + 2, Y);
    public Position Left => (X - 2, Y);
    public Position Up => (X, Y - 1);
    public Position Down => (X, Y + 1);

    public int Y { get; init; }

    public static implicit operator Position((int, int) position) => new() { X = position.Item1, Y = position.Item2 };
    public static bool operator ==(Position lhs, Position rhs) => lhs.X == rhs.X && lhs.Y == rhs.Y;
    public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);
    
    public bool Equals(Position other) => X == other.X && Y == other.Y;
    public override bool Equals(object? obj) => obj is Position other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(X, Y);
  }
}
