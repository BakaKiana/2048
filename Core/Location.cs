using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Console2048
{
    struct Location
    {
        public int Rindex{get;set;}
        public int Cindex { get; set; }
        public Location(int rindex,int cindex):this()
        {
            this.Rindex = rindex;
            this.Cindex = cindex;
        }
    }
}
