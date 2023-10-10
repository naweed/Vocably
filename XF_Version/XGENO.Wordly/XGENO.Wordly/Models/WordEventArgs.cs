using System;
using System.Collections.Generic;

namespace XGENO.Wordly.Models
{
    public class WordEventArgs : EventArgs
    {
        public string Word { get; set; }
        public string Meaning { get; set; }
        public List<Level_Progress> Progress { get; set; } = new List<Level_Progress>();
    }
}
