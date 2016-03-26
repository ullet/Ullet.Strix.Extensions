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
    /// Repeat <paramref name="action"/> <paramref name="count"/> times.
    /// </summary>
    public static void Repeat(this Action action, int count)
    {
      action.While(() => count-- > 0);
    }

    /// <summary>
    /// Repeat <paramref name="expression"/> <paramref name="count"/> times.
    /// </summary>
    /// <returns>
    /// Enumeration of all values returned by <paramref name="expression"/>.
    /// </returns>
    /// <remarks>
    /// If <paramref name="expression"/> is a pure function, so constant output,
    /// then this Repeat is equivalent to:
    /// <code>
    /// <![CDATA[
    /// Enumerable.Repeat(expression(), count)]]>
    /// </code>
    /// It is more useful if expression is not a pure function, i.e. mutates the
    /// environment or dependent on mutating environment. For example can be
    /// used to generate a sequence of random integers:
    /// <code>
    /// <![CDATA[
    /// var randomValues = ((Func<int>) new System.Random().Next).Repeat(100);
    /// ]]></code>
    /// </remarks>
    public static IEnumerable<T> Repeat<T>(this Func<T> expression, int count)
    {
      while (count-- > 0)
        yield return expression();
    }
  }
}
