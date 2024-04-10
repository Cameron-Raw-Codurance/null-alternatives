using CSharpFunctionalExtensions;

namespace NullAlternatives.Functional.UsingOption;

public class Product
{
    public string Name { get; init; }
    public double Price { get; init; }
    public Option<double> Weight { get; init; }
}