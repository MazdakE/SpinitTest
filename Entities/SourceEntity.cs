namespace SpinitTest.Entities
{
    public class SourceEntity
    {
        public List<string>? Measures { get; set; } = new();

        public AnnotationEntity? Annotations { get; set; }
        public string? Name { get; set; }
    }
}
