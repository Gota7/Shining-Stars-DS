﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Ndst {

    class Program {

        static void Main(string[] args) {
            ROM r = new ROM("Shining.nds");
            r.Save("Test.nds");
            r.Extract("ROM");
        }

    }

}
