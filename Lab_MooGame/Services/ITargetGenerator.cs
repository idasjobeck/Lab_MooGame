using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_MooGame.Models;

namespace Lab_MooGame.Services;

public interface ITargetGenerator
{
    public int TargetLength { get; }
    public int MaxRange { get; }
    public bool AllowRepeats { get; }

    public string GenerateTarget();
}