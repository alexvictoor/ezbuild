using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace VisualStudioFileParser.Test
{
    internal class ProjectTest
    {
        private Project project;

        [SetUp]
        public void setUp()
        {
            project = new Project()
            {
                SlnPath = Constant.TEST_RESOURCES_DIR + "EZBuild.sln",
                CsprojRelativePath = @"VisualStudioFileParser\VisualStudioFileParser.csproj",
                AssemblyName = "VisualStudioFileParser",
                AssemblyType = "dll"
            };
        }

        [Test]
        public void should_generate_a_valid_absolute_path()
        {
            // given

            // when
            string absolutePath = project.CsprojPath;

            // then
            Check.That(File.Exists(absolutePath)).IsTrue();
            Check.That(Path.GetFileName(absolutePath)).IsEqualTo(Path.GetFileName(project.CsprojRelativePath));
        }

        [Test]
        public void should_generate_a_canonical_absolute_path()
        {
            // given

            // when
            string absolutePath = project.CsprojPath;

            // then
            Check.That(absolutePath).DoesNotContain("..");
        }
        
        [Test]
        public void should_generate_a_canonical_sln_path()
        {
            // given

            // when
            string slnPath = project.SlnPath;

            // then
            Check.That(slnPath).DoesNotContain("..");
        }

        [Test]
        public void should_generate_a_valid_assembly_path()
        {
            // given

            // when
            string assemblyPath = project.AssemblyPath;

            // then
            Check.That(File.Exists(assemblyPath)).IsTrue();
            Check.That(Path.GetFileName(assemblyPath)).IsEqualTo("VisualStudioFileParser.dll");
        }

    }
}