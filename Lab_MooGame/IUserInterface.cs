using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_MooGame;

public interface IUserInterface
{
    void WriteLine(string message);
    string? ReadLine();
}