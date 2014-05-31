using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace VisualStudioFileParser.Test
{
    class SlnParserTest
    {
        [Test]
        public void Should_parse_projects_from_sln_file()
        {
            // given
            var parser = new SlnParser();
            string slnPath = Constant.TEST_RESOURCES_DIR + "EZBuild.sln";
            // when
            Solution solution = parser.Parse(slnPath);
            // then
            Check.That(solution).IsNotNull();
            Check.That(solution.SlnFile).IsNotNull();
            Check.That(solution.SlnFile.Exists).IsTrue();
            Check.That(solution.Projects).HasSize(3);
            var firstExpectedProject 
                = new Project()
                {
                    Name = "EZBuild", 
                    SlnPath = slnPath, 
                    CsprojRelativePath = @"EZBuild\EZBuild.csproj", 
                    AssemblyName = "EZBuild", 
                    AssemblyType = "dll"
                };
            Check.That(solution.Projects.First()).HasFieldsWithSameValues(firstExpectedProject);
        }

        [Test]
        public void Should_flag_as_test_a_project_with_name_ending_with_test()
        {
            // given
            var parser = new SlnParser();
            string slnPath = Constant.TEST_RESOURCES_DIR + "EZBuild.sln";
            // when
            Solution solution = parser.Parse(slnPath);
            // then
            IEnumerable<Project> testProjects = from prj in solution.Projects where prj.Test select prj;
            Check.That(testProjects).HasSize(1);
            Check.That(testProjects.First().Name).IsEqualTo("VisualStudioFileParser.Test");
        }

        [Test]
        public void Should_not_flag_as_test_any_projectn_when_a_bad_pattern_is_used()
        {
            // given
            var parser = new SlnParser() { TestPattern = "ABCD"};
            string slnPath = Constant.TEST_RESOURCES_DIR + "EZBuild.sln";
            // when
            Solution solution = parser.Parse(slnPath);
            // then
            IEnumerable<Project> testProjects = from prj in solution.Projects where prj.Test select prj;
            Check.That(testProjects).IsEmpty();
        }
    }
}
