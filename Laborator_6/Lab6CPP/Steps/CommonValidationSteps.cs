using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Lab6CPP.Steps
{
    [Binding]
    public class CommonValidationSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CommonValidationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        // Verifică dacă lista de produse este sortată alfabetic crescător
        [Then(@"lista de produse este sortată alfabetic crescător")]
        public void ThenListaDeProduseEsteSortataAlfabetic()
        {
            var productNames = _scenarioContext.Get<List<string>>("productNames");
            var sortedNames = productNames.OrderBy(name => name).ToList();

            Assert.AreEqual(sortedNames, productNames,
                "Lista de produse NU este afișată în ordine alfabetică crescătoare.");
        }

        // Verifică dacă lista de produse nu conține duplicate
        [Then(@"lista de produse NU trebuie să conțină duplicate")]
        public void ThenNuExistaDuplicate()
        {
            var productNames = _scenarioContext.Get<List<string>>("productNames");

            var duplicates = productNames
                .GroupBy(name => name)
                .Where(group => group.Count() > 1)
                .Select(group => group.Key)
                .ToList();

            Assert.IsTrue(duplicates.Count == 0,
                "S-au detectat duplicate în lista de produse: " + string.Join(", ", duplicates));
        }
    }
}
