using System;

namespace Ullet.Strix.Extensions
{
  public static partial class DelegateExtensions
  {
    public static Func<bool> Not(this Func<bool> f)
    {
      return () => !f();
    }

    public static Func<T, bool> Not<T>(this Func<T, bool> f)
    {
      return t => !f(t);
    }
  }
}
