using ObjectCreator;

namespace RandomObjectCreatorTest
{
    [TestClass]
    public class RandomEntityCreatorTest
    {
        [TestMethod]
        public async Task TestRunRandomEntityCreator()
        {
            var instance = RandomObjectCreator.Create<TestEntityWithList>();

            //Assert.AreEqual("meow", result.SurveyId);
        }

        [TestMethod]
        public async Task CreateEntityWithIgnoredTypes()
        {
            var instance = RandomObjectCreator.Create<TestEntity>(null, new List<Type> { typeof(string) });
            Assert.IsNull(instance.stringProp);
            Assert.IsNotNull(instance.shortProp);
            Assert.IsNotNull(instance.doubleProp);
            Assert.IsNotNull(instance.boolProp);
            Assert.IsNotNull(instance.decimalProp);
            Assert.IsNotNull(instance.floatProp);
            Assert.IsNotNull(instance.dateOnlyProp);
            Assert.IsNotNull(instance.timeOnlyProp);
            Assert.IsNotNull(instance.dateTimeProp);
        }

        [TestMethod]
        public async Task CreateEntityWithIgnoredPropNames()
        {
            var instance = RandomObjectCreator.Create<TestEntity>(new List<string> { nameof(TestEntity.shortProp), nameof(TestEntity.decimalNullableProp) });
            Assert.IsNotNull(instance.stringProp);
            Assert.AreEqual(0, instance.shortProp);
            Assert.IsNull(instance.decimalNullableProp);
            Assert.IsNotNull(instance.doubleProp);
            Assert.IsNotNull(instance.boolProp);
            Assert.IsNotNull(instance.decimalProp);
            Assert.IsNotNull(instance.floatProp);
            Assert.IsNotNull(instance.dateOnlyProp);
            Assert.IsNotNull(instance.timeOnlyProp);
            Assert.IsNotNull(instance.dateTimeProp);
        }
    }

    class TestEntity
    {
        public byte byteProp { get; set; }
        public short shortProp { get; set; }
        public float floatProp { get; set; }
        public decimal decimalProp { get; set; }
        public decimal? decimalNullableProp { get; set; }
        public double doubleProp { get; set; }
        public bool boolProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public DateOnly dateOnlyProp { get; set; }
        public TimeOnly timeOnlyProp { get; set; }
    }

    class TestEntityWithList : TestEntity
    {
        public TestEntity objectProp { get; set; }
        public List<TestEntity> listProp { get; set; }
        public ICollection<TestEntity> iCollectionProp { get; set; }
        public IEnumerable<TestEntity> iEnumerableProp { get; set; }
    }
}