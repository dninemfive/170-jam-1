using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// An abstraction of 2D arrays representing the game grid, of fixed size corresponding to that set in <see cref="GameManager"/>.
/// </summary>
/// <remarks>
/// When the documentation for <c>Board</c> refers to "a tile," it means "an object of type T at a coordinate on the board".
/// </remarks>
/// <typeparam name="T">The type which the board is holding.</typeparam>
public class Board<T>
{
    /// <summary>
    /// The internal 2D array.
    /// </summary>
    private readonly T[,] _board;
    public IEnumerable<T> AllItems
    {
        get
        {
            foreach (T t in _board) yield return t;
        }
    }
    /// <summary>
    /// Generates a new board of the specified type, using the generator which is passed into it. This ensures there's at least an attempt to define each cell.
    /// </summary>
    /// // the "seealso" tag does not appear to work in VS lol
    /// <remarks>
    /// See also:<br/> 
    /// - <see href="https://learn.microsoft.com/en-us/dotnet/api/system.func-3?view=net-7.0"><c>Func~3</c></see><br/>
    /// - <see href="https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions"><c>Lambda expressions</c></see>
    /// </remarks>
    /// <param name="generator">A function (lambda or otherwise) which takes a pair of coords as an input parameter and outputs the corresponding tile.</param>
    public Board(Func<int, int, T> generator)
    {
        _board = new T[GameManager.NUM_TILES_X, GameManager.NUM_TILES_Z];
        foreach((int x, int z) in GameManager.EnumerateAllCoordinates)
        {
            _board[x, z] = generator(x, z);
        }
    }
    /// <summary>
    /// Gets a tile from the board.
    /// </summary>
    /// <param name="x">The x coordinate of the tile to retrieve.</param>
    /// <param name="z">The z coordinate of the tile to retrieve.</param>
    /// <returns>The tile at (x, z).</returns>
    public T this[int x, int z]
    {
        get
        {
            if (x is < 0 or >= GameManager.NUM_TILES_X) throw new ArgumentOutOfRangeException(nameof(x));
            if (z is < 0 or >= GameManager.NUM_TILES_Z) throw new ArgumentOutOfRangeException(nameof(z));
            return _board[x, z];
        }
    }
}
