/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2015, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.Strix.Extensions
{
  /// <summary>
  /// General extension methods for any type.
  /// </summary>
  public static class GeneralExtensions
  {
    /// <summary>
    /// Execute an action against an instance of an object and return the
    /// original instance.
    /// </summary>
    /// <remarks>
    /// <para>Alias for Mutate.</para>
    /// /// <para>
    /// One advantage of using Execute rather than just calling the action
    /// directly is that it returns the original instance so enables chaining
    /// calls.
    /// </para>
    /// </remarks>
    public static T Execute<T>(this T t, Action<T> action)
    {
      return t.Mutate(action);
    }

    /// <summary>
    /// Execute an action against an instance of an object and return the
    /// original instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Since an action has no return value, something must be mutated, or at
    /// least must be a side-effect, such as output to a device, in order for
    /// this method to do anything useful.  The method name "Mutate" is
    /// intended to make it clear that this is definately not a pure function.
    /// </para>
    /// <para>
    /// One advantage of using Mutate rather than just calling the action
    /// directly is that it returns the original instance so enables chaining
    /// calls.
    /// </para>
    /// </remarks>
    public static T Mutate<T>(this T t, Action<T> action)
    {
      action(t);
      return t;
    }

    /// <summary>
    /// Execute an action against an instance of an object with an additional
    /// object and then return the original instance.
    /// </summary>
    public static TInst ExecuteWithObject<TInst, TObj>(
      this TInst t, TObj o, Action<TInst, TObj> action)
    {
      return t.MutateWithObject(o, action);
    }

    /// <summary>
    /// Execute an action against an instance of an object with an additional
    /// object and then return the original instance.
    /// </summary>
    public static TInst MutateWithObject<TInst, TObj>(
      this TInst t, TObj o, Action<TInst, TObj> action)
    {
      action(t, o);
      return t;
    }

    /// <summary>
    /// Test if object matches any of the specified predicates.
    /// </summary>
    /// <param name="t">Object to match.</param>
    /// <param name="predicates">
    /// <![CDATA[Func<string, bool>]]> predicate functions that perform the
    /// match tests.
    /// </param>
    /// <typeparam name="T">Type of object being matched.</typeparam>
    /// <returns>
    /// <c>true</c> if object matches using at least one of the predicates;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny<T>(
      this T t, IEnumerable<Func<T, bool>> predicates)
    {
      return (predicates ?? Enumerable.Empty<Func<T, bool>>())
        .Any(p => (p ?? (_ => false))(t));
    }

    /// <summary>
    /// Test if object matches any of the specified predicates.
    /// </summary>
    /// <param name="t">Object to match.</param>
    /// <param name="predicates">
    /// <![CDATA[Func<string, bool>]]> predicate functions that perform the
    /// match tests.
    /// </param>
    /// <typeparam name="T">Type of object being matched.</typeparam>
    /// <returns>
    /// <c>true</c> if object matches using at least one of the predicates;
    /// otherwise <c>false</c>.
    /// </returns>
    public static bool MatchesAny<T>(
      this T t, params Func<T, bool>[] predicates)
    {
      return t.MatchesAny((IEnumerable<Func<T, bool>>)predicates);
    }
  }
}
