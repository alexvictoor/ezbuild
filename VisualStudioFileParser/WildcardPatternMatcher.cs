using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualStudioFileParser
{
    /// <summary>
    /// Copy/paste from http://www.codeproject.com/Tips/57304/Use-wildcard-characters-and-to-compare-strings
    /// 
    /// </summary>
    public static class WildcardPatternMatcher
    {

        public static Boolean MatchWildcardExpression(this String input, String pattern)
        {
            return MatchWildcardString(pattern, input);
        }
        private static Boolean MatchWildcardString(String pattern, String input)
        {
            if (String.Compare(pattern, input) == 0)
            {
                return true;
            }
            else if (String.IsNullOrEmpty(input))
            {
                return String.IsNullOrEmpty(pattern.Trim(new Char[1] { '*' }));
            }
            else if (pattern.Length == 0)
            {
                return false;
            }
            else if (pattern[0] == '?')
            {
                return MatchWildcardString(pattern.Substring(1), input.Substring(1));
            }
            else if (pattern[pattern.Length - 1] == '?')
            {
                return MatchWildcardString(pattern.Substring(0, pattern.Length - 1), input.Substring(0, input.Length - 1));
            }
            else if (pattern[0] == '*')
            {
                return MatchWildcardString(pattern.Substring(1), input) || MatchWildcardString(pattern, input.Substring(1));
            }
            else if (pattern[pattern.Length - 1] == '*')
            {
                return MatchWildcardString(pattern.Substring(0, pattern.Length - 1), input) || MatchWildcardString(pattern, input.Substring(0, input.Length - 1));
            }
            else if (pattern[0] == input[0])
            {
                return MatchWildcardString(pattern.Substring(1), input.Substring(1));
            }
            return false;
        }
    }
}
