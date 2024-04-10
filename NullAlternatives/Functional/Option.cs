namespace NullAlternatives.Functional;

public class Option<T>
{
   private readonly T? _value;
   private readonly bool _hasValue;

   private Option(T? value)
   {
      if (value is not null)
      {
         _hasValue = true;
         _value = value;
      }

      _hasValue = false;
   }

   public static Option<T> FromSome(T value) => new(value);
   public static Option<T> FromNone() => new(default);

   public Option<T> Some(Action<T> action)
   {
      if (_hasValue && _value is not null)
      {
         action(_value);
      }

      return this;
   }

   public Option<T> None(Action action)
   {
      if (!_hasValue)
      {
         action();
      }

      return this;
   }

   public bool IsSome() => _hasValue;

   public bool IsSomeAnd(Func<T, bool> action)
   {
      return _hasValue && action(_value!);
   }

   public bool IsNone() => !_hasValue;
}