/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;

namespace Ullet.Strix.Extensions
{
  public static partial class DelegateExtensions
  {
    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<bool> Not(this Func<bool> f)
    {
      return () => !f();
    }

    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<T, bool> Not<T>(this Func<T, bool> f)
    {
      return t => !f(t);
    }

    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<T1, T2, bool> Not<T1, T2>(this Func<T1, T2, bool> f)
    {
      return (t1, t2) => !f(t1, t2);
    }

    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<T1, T2, T3, bool> Not<T1, T2, T3>(
      this Func<T1, T2, T3, bool> f)
    {
      return (t1, t2, t3) => !f(t1, t2, t3);
    }

    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<T1, T2, T3, T4, bool> Not<T1, T2, T3, T4>(
      this Func<T1, T2, T3, T4, bool> f)
    {
      return (t1, t2, t3, t4) => !f(t1, t2, t3, t4);
    }

    /// <summary>
    /// Return negative predicate.
    /// </summary>
    public static Func<T1, T2, T3, T4, T5, bool> Not<T1, T2, T3, T4, T5>(
      this Func<T1, T2, T3, T4, T5, bool> f)
    {
      return (t1, t2, t3, t4, t5) => !f(t1, t2, t3, t4, t5);
    }
  }
}
