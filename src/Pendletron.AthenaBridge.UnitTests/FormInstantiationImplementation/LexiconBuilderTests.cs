using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormInstantiationImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Forms;

namespace Pendletron.AthenaBridge.UnitTests.FormInstantiationImplementation
{
    [TestClass]
    public class LexiconBuilderTests
    {
        protected LexiconBuilder LB()
        {
            return new LexiconBuilder();
        }

        protected Mock<IMainFormControlMap> FormMapMock()
        {
            return new Mock<IMainFormControlMap>();
        }
        [TestMethod]
        public void Build_Returns_NotNullLexicon()
        {
            var formMap = FormMapMock();
            var built = LB().Build(formMap.Object);
            Assert.IsNotNull(built);
        }
        [TestMethod]
        public void BuildButton_ReturnsNull_ForNullButton()
        {
            var formMap = FormMapMock();
            var builder = LB();
            Button button = null;
            var results = builder.BuildButton(button);
            Assert.IsNull(button);
        }

        [TestMethod]
        public void BuildButton_HasButtonNameForKey_ForKnownButton()
        {
            var formMap = FormMapMock();
            var builder = LB();
            string expectedName = "btnKnownName";
            Button button = new Button();
            button.Name = expectedName;
            var results = builder.BuildButton(button);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.HasValue);
            Assert.AreEqual(expectedName, results.Value.Key);
        }

        [TestMethod]
        public void BuildButton_HasButtonTextForValue_ForKnownButton()
        {
            var formMap = FormMapMock();
            var builder = LB();
            string expectedValue = "Expected Value";
            Button button = new Button();
            button.Name = "justAName";
            button.Text = expectedValue;
            var results = builder.BuildButton(button);
            Assert.IsNotNull(results);
            Assert.IsTrue(results.HasValue);
            Assert.AreEqual(expectedValue, results.Value.Value);
        }

        [TestMethod]
        public void SetPairInLexicon_DoesNotAddNullPair()
        {
            var formMap = FormMapMock();
            var builder = LB();
            var lexicon = new Lexicon();
            KeyValuePair<string, object>? pair = null;
            int countBeforeAdd = lexicon.Count;
            builder.SetPairInLexicon(lexicon, pair);
            int countAfterAdd = lexicon.Count;

            Assert.AreEqual(countBeforeAdd, countAfterAdd);
        }

        [TestMethod]
        public void SetPairInLexicon_DoesAdd_NotNullPair()
        {
            var formMap = FormMapMock();
            var builder = LB();
            var lexicon = new Lexicon();
            string expectedKey = "ExpectedKey";
            string expectedValue = "ExpectedValue";
            var pair = new KeyValuePair<string, object>(expectedKey, expectedValue);
            
            int countBeforeAdd = lexicon.Count;
            builder.SetPairInLexicon(lexicon, pair);
            int countAfterAdd = lexicon.Count;

            Assert.AreEqual(countBeforeAdd+1, countAfterAdd);
            Assert.IsTrue(lexicon.ContainsKey(expectedKey));

            var valueByKey = lexicon[expectedKey];
            Assert.AreEqual(expectedValue, valueByKey);
        }

        [TestMethod]
        public void SetPairInLexicon_OverwritesExisting_NotNullPair()
        {
            var formMap = FormMapMock();
            var builder = LB();
            var lexicon = new Lexicon();
            string expectedKey = "ExpectedValue";
            string expectedValue = "ExpectedValue";
            var pair = new KeyValuePair<string, object>(expectedKey, expectedValue);

            int countBeforeAdd = lexicon.Count;
            builder.SetPairInLexicon(lexicon, pair);
            int countAfterAdd = lexicon.Count;

            Assert.AreEqual(countBeforeAdd + 1, countAfterAdd);
            Assert.IsTrue(lexicon.ContainsKey(expectedKey));

            var valueByKey = lexicon[expectedKey];
            Assert.AreEqual(expectedKey, valueByKey);

            string expectedValue2 = "ExpectedValue2";
            pair = new KeyValuePair<string, object>(expectedKey, expectedValue2);

            builder.SetPairInLexicon(lexicon, pair);
            countAfterAdd = lexicon.Count;
            Assert.AreEqual(countBeforeAdd + 1, countAfterAdd);
            Assert.IsTrue(lexicon.ContainsKey(expectedKey));

            valueByKey = lexicon[expectedKey];
            Assert.AreEqual(expectedValue2, valueByKey);

        }

        [TestMethod]
        public void Build_Visits_AllFormProperties()
        {
            // I don't really know how to test this
            var formMap = FormMapMock();
            var builder = LB();

            formMap.SetupAllProperties();

            var lexicon = builder.Build();

            bool allPropertiesVisited = false;
            Assert.IsTrue(allPropertiesVisited);
        }
    }
}
