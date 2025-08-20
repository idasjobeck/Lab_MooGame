using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame.Services;

public interface ITargetGenerator
{
    public int TargetLength { get; }

    public string GenerateTarget();
}