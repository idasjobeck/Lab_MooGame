using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

/// <summary>
/// Defines the contract for generating a target string based on specified parameters.
/// </summary>
/// <remarks>Implementations of this interface are expected to generate target strings that adhere to the
/// constraints defined by the properties <see cref="TargetLength"/>, <see cref="MaxRange"/>, and <see
/// cref="AllowRepeats"/>.</remarks>

public interface ITargetGenerator
{
    public int TargetLength { get; }
    public int MaxRange { get; }
    public bool AllowRepeats { get; }

    public string GenerateTarget();
}