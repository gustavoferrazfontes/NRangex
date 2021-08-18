using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("RankRegex.Tests")]
namespace Rangex 
{
    public sealed class ClassifyRegex: IComparable<ClassifyRegex>
    {
        internal int TotalScore { get; private set; }

        private readonly Regex regex;
        private readonly string pattern;
        public ClassifyRegex(string aText)
        {
            this.pattern = aText;
            this.regex = new Regex(aText);
        }

        public void GenerateScore(string text)
        {
            var scores = new List<bool>(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                scores.Add(regex.IsMatch(text.Substring(0,text.Length -i)));
            }

             TotalScore = scores.Count(result => result == false);
        }

        public bool IsMatch(string text) => regex.IsMatch(text);

        public override string ToString()
        {
            return pattern;
        }


        public int CompareTo(ClassifyRegex other)
        {
            if (other == null) return 0;
            return other.TotalScore < this.TotalScore || other.pattern.Length < this.pattern.Length  ? 1 : -1;
        }
    }
}
