using CSharpFunctionalExtensions;

namespace NullAlternatives.Bonus;

public static class ResultVsException
{
   public static int ExceptionMethod1()
   {
      return Workers.DoWork1();
   } 
   public static int ExceptionMethod2()
   {
      return Workers.DoWork2();
   } 
   public static int ExceptionMethod3()
   {
      return Workers.DoWork3();
   } 
   public static int ResultMethod1()
   {
      return Workers.DoWork4();
   } 
   public static int ResultMethod2()
   {
      var answer = 0;
      if (Workers.DoWork5().TryGetValue(out var returnedInt))
      {
         answer = returnedInt;
      }

      return answer;
   } 
   public static int ResultMethod3()
   {
      return Workers.DoWork6();
   } 
}

public static class Workers
{
   public static int DoWork1()
   {
      throw new Exception();
   }

   public static int DoWork2()
   {
      return 200;
   }

   public static int DoWork3()
   {
      return 300;
   }

   public static int DoWork4()
   {
      return 400;
   }

   public static Result<int> DoWork5()
   {
      return Result.Success(500);
   }

   public static int DoWork6()
   {
      return 600;
   }
}