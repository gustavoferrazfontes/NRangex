using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Rangex;

namespace RankRegex.Tests
{
    public class RengexTests
    {
        private readonly IEnumerable<ClassifyRegex> patterns;
        public RengexTests()
        {
            patterns = new List<ClassifyRegex>
            {
                new ClassifyRegex(@"RVAcao\|PETR\|.*\|.*\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|USD\|BRL\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|OIS\|BRL\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|BRL\|OIS\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|.*\|.*\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|.*\|BRL\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|.*\|USD\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|.*\|OIS\|"),
                new ClassifyRegex(@"RVAcao\|PETR4\|EUR\|.*\|"),
                new ClassifyRegex(@"RVAcao\|.*\|.*\|.*\|"),
                new ClassifyRegex(@"RVAcao\|ENBR3\|.*\|.*\|"),
                new ClassifyRegex(@"OpcaoAcao\|PETR4\|USD\|BRL\|"),
            };
        }

        [Fact]
        public void Must_have_find_all_models_for_a_specific_key()
        {
            var sut = new Rangex.RankRegex(patterns);

            var actual = sut.GetPatternsTo(@"RVAcao|PETR4|BRL|USD|");

            actual.Count().Should().Be(3);
        }

        [Fact]
        public void Must_have_find_most_specific_model_for_a_key()
        {
            
            var sut = new Rangex.RankRegex(patterns);

            var resultado = sut.GetLessStable(@"RVAcao|PETR4|BRL|USD|");

            resultado.ToString().Should().Be(@"RVAcao\|PETR4\|.*\|USD\|");

        }

        [Fact]
        public void Must_have_find_less_specific_model_for_a_key()
        {
            
            var sut = new Rangex.RankRegex(patterns);

            var resultado = sut.GetMostStable(@"RVAcao|PETR4|BRL|USD|");

            resultado.ToString().Should().Be(@"RVAcao\|.*\|.*\|.*\|");

        }
    }
}
