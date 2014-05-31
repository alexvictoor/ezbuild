using System;
using System.IO;

namespace VisualStudioFileParser
{
    public class Project
    {
        private string _slnPath;

        public string Name { get; set; }
        public string CsprojRelativePath { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyType { get; set; }
        public string AssemblyLocation { get; set; }
        public bool Test { get; set; }

        public Project()
        {
            AssemblyLocation = @"bin\Debug";
        }

        public string SlnPath
        {
            get { return _slnPath; } 
            set { _slnPath = new FileInfo(value).FullName; }
        }

        public string CsprojPath
        {
            get
            {
                string solutionFullPath = new FileInfo(SlnPath).Directory.FullName;
                return Path.Combine(solutionFullPath, CsprojRelativePath);
            }
        }

        public string AssemblyPath
        {
            get
            {
                string projectRootPath = new FileInfo(CsprojPath).Directory.FullName;
                string assemblyRelativePath = AssemblyLocation + @"\" + AssemblyName + "." + AssemblyType;
                return Path.Combine(projectRootPath, assemblyRelativePath);
            }
        }
    }

}