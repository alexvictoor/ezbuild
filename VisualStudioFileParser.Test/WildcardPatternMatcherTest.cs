using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFluent;
using NUnit.Framework;

namespace VisualStudioFileParser.Test
{
    class WildcardPatternMatcherTest
    {
        [Test]
        public void Should_Evaluate_Patterns()
        {
            // Positive Tests
            Check.That("".MatchWildcardExpression("*")).IsTrue();
            Check.That(" ".MatchWildcardExpression("?")).IsTrue();
            Check.That("a".MatchWildcardExpression("*")).IsTrue();
            Check.That("ab".MatchWildcardExpression("*")).IsTrue();
            Check.That("a".MatchWildcardExpression("?")).IsTrue();
            Check.That("abc".MatchWildcardExpression("*?")).IsTrue();
            Check.That("abc".MatchWildcardExpression("?*")).IsTrue();
            Check.That("abc".MatchWildcardExpression("*abc")).IsTrue();
            Check.That("abc".MatchWildcardExpression("*abc*")).IsTrue();
            Check.That("aXXXbc".MatchWildcardExpression("*a*bc")).IsTrue();

            // Negative Tests
            Check.That("".MatchWildcardExpression("*a")).IsFalse();
            Check.That("".MatchWildcardExpression("a*")).IsFalse();
            Check.That("".MatchWildcardExpression("?")).IsFalse();
            Check.That("a".MatchWildcardExpression("*b*")).IsFalse();
            Check.That("ab".MatchWildcardExpression("b*a")).IsFalse();
            Check.That("a".MatchWildcardExpression("??")).IsFalse();
            Check.That("".MatchWildcardExpression("*?")).IsFalse();
            Check.That("a".MatchWildcardExpression("??*")).IsFalse();
            Check.That("abX".MatchWildcardExpression("*abc")).IsFalse();
            Check.That("Xbc".MatchWildcardExpression("*abc*")).IsFalse();
            Check.That("ac".MatchWildcardExpression("*a*bc*")).IsFalse();

        }
    }
}
