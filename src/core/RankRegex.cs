using System.Collections.Generic;
using System.Linq;

namespace Rangex 
{
    public sealed class RankRegex
    {
        private readonly IEnumerable<ClassifyRegex> _regexPatterns;
        
        public RankRegex(IEnumerable<ClassifyRegex> regexPatterns)
        {
            this._regexPatterns = regexPatterns;
        }

        public ClassifyRegex GetLessStable(string aText)
        {
            var hankedPatterns = GetRankedRegexTo(aText);
            return hankedPatterns.OrderByDescending(_ => _).First();
        }

        public ClassifyRegex GetMostStable(string aText)
        {
            var hankedPatterns = GetRankedRegexTo(aText);

            return hankedPatterns.OrderBy(_ => _).First();
        }

        public IEnumerable<ClassifyRegex> GetPatternsTo(string aText)
        {
            return _regexPatterns.Where(chaveInstrumento => chaveInstrumento.IsMatch(aText));
        }

        private  IEnumerable<ClassifyRegex> GetRankedRegexTo(string aText)
        {
            var patterns = GetPatternsTo(aText);
            foreach (var regexPattern in patterns)
            {
                regexPattern.GenerateScore(aText);
            }
            return patterns;
        }


    }
}
