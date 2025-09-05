using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Models;

/// <summary>
/// Represents a generator for producing random integers within specified ranges.
/// </summary>
/// <remarks>This interface provides methods to generate random integers, either within a specified range or
/// without any constraints. Implementations of this interface should ensure that the generated numbers are uniformly
/// distributed within the specified range.</remarks>

public interface IRandom
{
    public int Next(int maxValue);
    public int Next(int minValue, int maxValue);
    public int Next();
}