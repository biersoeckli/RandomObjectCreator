# RandomObjectCreator

[![BUILD ON PUSH](https://github.com/biersoeckli/RandomObjectCreator/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/biersoeckli/RandomObjectCreator/actions/workflows/dotnet-build.yml)

C# Helper util to mock objects with random data.

## Examples

### Populate an object with random data
    
    // class declaration
    class TestEntity
    {
        public double doubleProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public TestEntity objectProp { get; set; }
        public List<TestEntity> listProp { get; set; }
        public ICollection<TestEntity> iCollectionProp { get; set; }
        public IEnumerable<TestEntity> iEnumerableProp { get; set; }
    }

    // creating an instance of TestEntity and populate it
    var createdObject = RandomObjectCreator.Create<TestEntity>();

### Populate an object with random data but skip certain types
    
    // class declaration
    class TestEntity
    {
        public double doubleProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public TestEntity2 objectProp { get; set; }
        public List<TestEntity> listProp { get; set; }
        public ICollection<TestEntity> iCollectionProp { get; set; }
        public IEnumerable<TestEntity> iEnumerableProp { get; set; }
    }

    // creating an instance of TestEntity and populate it
    var createdObject = RandomObjectCreator.Create<TestEntity>(null, new List<Type> { typeof(string) });
    // createdObject.stringProp will be null

### Populate an object with random data but skip certain properties with specific name
    
    // class declaration
    class TestEntity
    {
        public double doubleProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public TestEntity2 objectProp { get; set; }
        public List<TestEntity> listProp { get; set; }
        public ICollection<TestEntity> iCollectionProp { get; set; }
        public IEnumerable<TestEntity> iEnumerableProp { get; set; }
    }

    // creating an instance of TestEntity and populate it
    var createdObject = RandomObjectCreator.Create<TestEntity>(new List<string> { "dateTimeProp" });
    // createdObject.dateTimeProp will be null