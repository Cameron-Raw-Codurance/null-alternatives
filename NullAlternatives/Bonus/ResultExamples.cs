using CSharpFunctionalExtensions;

namespace NullAlternatives.Bonus;

public static class ResultExamples
{
    public static int ReturnsNumberOrThrowsSpecialException(bool willThrow)
    {
        if (willThrow)
        {
            throw new JustAnExampleException("Threw exception");
        }

        return 100;
    }

    public static Result<int> ReturnsResultOfInt(bool willTriggerException)
        => Result.Try(() => ReturnsNumberOrThrowsSpecialException(willTriggerException));
}

public class JustAnExampleException : Exception
{
    public JustAnExampleException(string message) : base(message)
    {
        
    }
}

