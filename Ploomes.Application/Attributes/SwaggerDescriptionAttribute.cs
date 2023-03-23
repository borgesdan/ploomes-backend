namespace Ploomes.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SwaggerDescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public string? Example { get; private set; }

        public SwaggerDescriptionAttribute(string description, string? example = null)
        {
            Description = description;
            Example = example;
        }
    }
}
