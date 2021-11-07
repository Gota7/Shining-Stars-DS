using System;

namespace Ndst {

    class Program {

        static void Main(string[] args) {
            ROM r = new ROM("Shining.nds");
            r.Extract("ROM");
        }

    }

}
