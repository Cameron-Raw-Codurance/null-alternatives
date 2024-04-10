using CSharpFunctionalExtensions;

namespace NullAlternatives.Functional.UsingMaybe;

public class Product
{
    public string Name { get; init; }
    public double Price { get; init; }
    public Maybe<double> Weight { get; init; }
}