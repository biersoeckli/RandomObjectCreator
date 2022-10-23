using ObjectCreator;

namespace RandomObjectCreatorTest
{
    [TestClass]
    public class RandomEntityCreatorTest
    {
        [TestMethod]
        public async Task TestRunRandomEntityCreator()
        {
            var instance = RandomObjectCreator.Create<TestEntity>();

            //Assert.AreEqual("meow", result.SurveyId);
        }
    }

    class TestEntity
    {
        public byte byteProp { get; set; }
        public short shortProp { get; set; }
        public float floatProp { get; set; }
        public decimal decimalProp { get; set; }
        public double doubleProp { get; set; }
        public bool boolProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public DateOnly dateOnlyProp { get; set; }
        public TimeOnly timeOnlyProp { get; set; }
        public TestEntityNested objectProp { get; set; }
        public List<TestEntityNested> listProp { get; set; }
        public ICollection<TestEntityNested> iCollectionProp { get; set; }
        public IEnumerable<TestEntityNested> iEnumerableProp { get; set; }
    }

    class TestEntityNested
    {
        public byte byteProp { get; set; }
        public short shortProp { get; set; }
        public float floatProp { get; set; }
        public decimal decimalProp { get; set; }
        public double doubleProp { get; set; }
        public bool boolProp { get; set; }
        public string stringProp { get; set; }
    }
}