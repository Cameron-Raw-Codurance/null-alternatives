using CSharpFunctionalExtensions;
using FluentAssertions;
using NullAlternatives.Bonus;

namespace Tests.Bonus;

public class ResultTests
{
    [Test]
    public void CreateResultsOnError()
    {
        var ourResult = Result.Try(() => ResultExamples.ReturnsNumberOrThrowsSpecialException(true));

        #region more functionality displayed

        Result.Try(() => ResultExamples.ReturnsNumberOrThrowsSpecialException(true))
            .Check(x => Result.Combine(FirstCheck(x), SecondCheck(x)))
            .TapError(err => Logger.Info($"Check failed the first two validations: {err}"))
            .MapError(err => $"Process was unsuccessful: {err}")
            .Map(x => $"Here is the success: {x}")
            .Match(
                Logger.LogSuccess,
                Logger.LogFailure);

        #endregion

        ourResult.Should().Fail("Threw exception");
    }

    [Test]
    public void CreateResultOnSuccess()
    {
        var ourResult = Result.Try(() => ResultExamples.ReturnsNumberOrThrowsSpecialException(false));
        ourResult.Should().SucceedWith(100);
    }

    private Result FirstCheck(int arg)
    {
        return arg > 500 ? Result.Success() : Result.Failure("Number too small");
    }

    private Result SecondCheck(int arg)
    {
        return arg > 500 ? Result.Success() : Result.Failure("Number too small");
    }
}

static class Logger
{
    public static void LogSuccess(string message)
    {
    }

    public static void LogFailure(string message)
    {
    }

    public static void Info(string message)
    {
        
    }
}