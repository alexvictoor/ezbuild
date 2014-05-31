using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VisualStudioFileParser
{
    public class Solution
    {

        public FileInfo SlnFile { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }

}
