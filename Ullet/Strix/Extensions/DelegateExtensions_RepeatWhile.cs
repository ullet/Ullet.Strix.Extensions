/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;

namespace Ullet.Strix.Extensions
{
  public static partial class DelegateExtensions
  {
    /// <summary>
    /// Repeat <paramref name="expression"/> while <paramref name="predicate"/>
    /// is <c>true</c>.
    /// </summary>
    /// <returns>
    /// Enumeration of values returned by <paramref name="expression"/>.
    /// </returns>
    /// <remarks>
    /// Since neither <paramref name="expression"/> nor
    /// <paramref name="predicate"/> accept any parameters, the loop is entirely
    /// dependent on an external mutating environment for input.
    /// </remarks>
    public static IEnumerable<T> RepeatWhile<T>(
      this Func<T> expression, Func<bool> predicate)
    {
      // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
      while (predicate())
        yield return expression();
    }

    /// <summary>
    /// Repeat <paramref name="expression"/> while <paramref name="predicate"/>
    /// is <c>true</c>. Initial input for expression is the default value of
    /// <typeparamref name="T"/>. Output of expression is passed as input to
    /// predicate and next evaluation of expression.
    /// </summary>
    /// <returns>
    /// Enumeration of values returned by <paramref name="expression"/>.
    /// </returns>
    public static IEnumerable<T> RepeatWhile<T>(
      this Func<T, T> expression, Func<T, bool> predicate)
    {
      return expression.RepeatWhile(default(T), predicate);
    }

    /// <summary>
    /// Repeat <paramref name="expression"/> while <paramref name="predicate"/>
    /// is <c>true</c>. Value of <paramref name="initial"/> is the initial input
    /// for expression. Output of expression is passed as input to predicate and
    /// next evaluation of expression.
    /// </summary>
    /// <returns>
    /// Enumeration of values returned by <paramref name="expression"/>.
    /// </returns>
    public static IEnumerable<T> RepeatWhile<T>(
      this Func<T, T> expression, T initial, Func<T, bool> predicate)
    {
      var result = initial;
      while (predicate(result))
        yield return result = expression(result);
    }

    /// <summary>
    /// Repeat <paramref name="action"/> while <paramref name="predicate"/> is
    /// true.
    /// </summary>
    /// <param name="action">
    /// <see cref="Action"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true.
    /// </param>
    public static void RepeatWhile(this Action action, Func<bool> predicate)
    {
      action.While(predicate);
    }
  }
}
