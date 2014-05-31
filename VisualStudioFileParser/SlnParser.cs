using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace VisualStudioFileParser
{
    public class SlnParser
    {
        public string TestPattern { get; set; }

        public SlnParser()
        {
            TestPattern = "*Test";
        }

        public Solution Parse(string path)
        {
            var slnFile = new FileInfo(path);
            if (!slnFile.Exists)
            {
                throw new ArgumentException("Incorrect sln path: " + path);
            }

            string[] slnContent = File.ReadAllLines(path);
            var projectLines = from line in slnContent where line.StartsWith("Project") select line;
            const string projectExpression = "^Project\\(\"(?<PROJECTTYPEGUID>.*)\"\\)\\s*=\\s*\"(?<PROJECTNAME>.*)\"\\s*,\\s*\"(?<RELATIVEPATH>.*)\"\\s*,\\s*\"(?<PROJECTGUID>.*)\"$";
            Regex projecRegex = new Regex(projectExpression);
            IList<Project> projects = new List<Project>();
            foreach (string line in projectLines)
            {
                var match = projecRegex.Match(line);
                string name = match.Groups["PROJECTNAME"].Value;
                string projectPath = match.Groups["RELATIVEPATH"].Value;
                if (projectPath.EndsWith("proj"))
                {
                    var project = new Project() { Name = name, CsprojRelativePath = projectPath, SlnPath = path};
                    AssessProjectTestNature(project);
                    ParseProjectProperties(project);
                    projects.Add(project);
                }
            }
            return new Solution()
            {
                SlnFile = slnFile,
                Projects = projects
            
            };
        }

        private void AssessProjectTestNature(Project project)
        {
            project.Test = project.Name.MatchWildcardExpression(TestPattern);
        }

        private void ParseProjectProperties(Project project)
        {
            XDocument xDocument = XDocument.Load(project.CsprojPath);
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());
            XNamespace ns = xDocument.Root.GetDefaultNamespace();
            namespaces.AddNamespace("vst", ns.NamespaceName);
            project.AssemblyName = xDocument.XPathSelectElement("/vst:Project/vst:PropertyGroup/vst:AssemblyName", namespaces).Value;
            string outputType = xDocument.XPathSelectElement("/vst:Project/vst:PropertyGroup/vst:OutputType", namespaces).Value;
            project.AssemblyType = "Library" == outputType ? "dll" : "exe";
        }
    }
}
