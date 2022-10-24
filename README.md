# RandomObjectCreator

[![BUILD ON PUSH](https://github.com/biersoeckli/RandomObjectCreator/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/biersoeckli/RandomObjectCreator/actions/workflows/dotnet-build.yml)

C# helper util to mock objects with random data.

## Examples

    // class declaration for the following examples
    class TestEntity
    {
        public double doubleProp { get; set; }
        public string stringProp { get; set; }
        public DateTime dateTimeProp { get; set; }
        public TestEntity2 objectProp { get; set; }
        public List<TestEntity2> listProp { get; set; }
        public ICollection<TestEntity2> iCollectionProp { get; set; }
        public IEnumerable<TestEntity2> iEnumerableProp { get; set; }
    }

    class TestEntity2
    {
        public double doubleProp2 { get; set; }
        public string stringProp2 { get; set; }
        public DateTime dateTimeProp2 { get; set; }
    }

### Populate an object with random data
    
    var createdObject = RandomObjectCreator.Create<TestEntity>();

### Populate an object with random data but skip certain types

    var createdObject = RandomObjectCreator.Create<TestEntity>(null, new List<Type> { typeof(string) });
    
    // createdObject.stringProp will be null

### Populate an object with random data but skip certain properties with specific name
    
    var createdObject = RandomObjectCreator.Create<TestEntity>(new List<string> { "dateTimeProp" });
    
    // createdObject.dateTimeProp will be null