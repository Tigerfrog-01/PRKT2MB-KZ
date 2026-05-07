using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.Generic;

namespace Projekt2_TARpe24_Kristopher;

public class RetseptiKategooria : List<Retsept>
{
    public string Nimetus { get; set; }

    public RetseptiKategooria(string nimetus, List<Retsept> retseptid) : base(retseptid)
    {
        Nimetus = nimetus;
    }
}