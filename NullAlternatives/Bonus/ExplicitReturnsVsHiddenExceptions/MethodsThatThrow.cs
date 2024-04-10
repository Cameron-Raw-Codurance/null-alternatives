namespace NullAlternatives.Bonus.ExplicitReturnsVsHiddenExceptions;

public static class MethodsThatThrow
{
   // Which one of these workers throws an Exception? Impossible to tell
   public static int ExceptionMethod1()
   {
      return ExceptionWorkers.DoWork1();
   } 
   public static int ExceptionMethod2()
   {
      return ExceptionWorkers.DoWork2();
   } 
   public static int ExceptionMethod3()
   {
      return ExceptionWorkers.DoWork3();
   } 
}

public static class ExceptionWorkers
{
   // There is no indication in the method signature that it throws Exceptions. You have to investigate, or 
   // trust that the package author has made it easy to see from within the IDE tooling
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
}