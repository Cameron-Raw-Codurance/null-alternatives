using CSharpFunctionalExtensions;

namespace NullAlternatives.Bonus.ExplicitReturnsVsHiddenExceptions;

public static class ResultsThatShow
{
   public static int ResultMethod1()
   {
      return ResultWorkers.DoWork1();
   } 
   
   // In contrast to methods that throw Exceptions, methods that return Results force the dev to handle
   // the unhappy path
   public static int ResultMethod2()
   {
      var answer = 0;
      if (ResultWorkers.DoWork2().TryGetValue(out var returnedInt))
      {
         answer = returnedInt;
      }

      return answer;
   } 
   public static int ResultMethod3()
   {
      return ResultWorkers.DoWork3();
   } 
}

public static class ResultWorkers
{
   public static int DoWork1()
   {
      return 400;
   }

   // Unlike methods that might throw Exceptions, methods that handle Results make it clear that there's an
   // error case by having a Result return type
   public static Result<int> DoWork2()
   {
      return Result.Success(500);
   }

   public static int DoWork3()
   {
      return 600;
   }
}