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
    /// Evaluate <paramref name="expression"/> while
    /// <paramref name="predicate"/> is true. May execute the expression zero
    /// times if predicate is immediately true. Result of each evaluation of
    /// epression is passed as input to the predicate. First input for predicate
    /// is default of <typeparamref name="T"/>. Returns the final return
    /// value from expression or default if never executed.
    /// </summary>
    /// <typeparam name="T">
    /// Type returned by <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop or
    /// default of <typeparamref name="T"/> if expression never evaluated.
    /// </returns>
    /// <remarks>
    /// Since <paramref name="expression"/> does not have any input parameters,
    /// the function must be dependent on some external input in order for its
    /// result to vary with each iteration of the loop.  This external input
    /// could be, for example, a captured variable in a closure, or read from a
    /// file system.
    /// </remarks>
    public static T While<T>(this Func<T> expression, Func<T, bool> predicate)
    {
      return expression.While(default(T), predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> while
    /// <paramref name="predicate"/> is true. May execute the expression zero
    /// times if predicate is immediately true. Result of each evaluation of
    /// epression is passed as input to the predicate. First input for predicate
    /// is the specified <paramref name="defaultResult"/>. Returns the final
    /// return value from expression or <paramref name="defaultResult"/> if
    /// never executed.
    /// </summary>
    /// <typeparam name="T">
    /// Type returned by <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="defaultResult">
    /// Default value for initial predicate call and return value if
    /// <paramref name="expression"/> is never evaluated.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop or
    /// <paramref name="defaultResult"/> if expression never evaluated.
    /// </returns>
    /// <remarks>
    /// Since <paramref name="expression"/> does not have any input parameters,
    /// the function must be dependent on some external input in order for its
    /// result to vary with each iteration of the loop.  This external input
    /// could be, for example, a captured variable in a closure, or read from a
    /// file system.
    /// </remarks>
    public static T While<T>(
      this Func<T> expression, T defaultResult, Func<T, bool> predicate)
    {
      return ((Func<T, T>) (t => expression())).While(defaultResult, predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate and the next evalutation
    /// of the expression. The default of <typeparamref name="T"/> is passed to
    /// the preficate and expression as the first input value. Returns the final
    /// return value from the expression or the default value if expression is
    /// never evaluated.
    /// </summary>
    /// <typeparam name="T">
    /// Type of input and return value of <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{T,TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop or
    /// default of <typeparamref name="T"/> if expression never evaluated.
    /// </returns>
    public static T While<T>(
      this Func<T, T> expression, Func<T, bool> predicate)
    {
      return expression.While(default(T), predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate and the next evalutation
    /// of the expression. <paramref name="initial"/> is passed to the predicate
    /// and expression as the first input value. Returns the final return value
    /// from the expression or <paramref name="initial"/> if expression is never
    /// evaluated.
    /// </summary>
    /// <typeparam name="T">
    /// Type of input and return value of <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{T,TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="initial">
    /// Initial input for <paramref name="predicate"/> and
    /// <paramref name="expression"/> and default return value.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop or
    /// <paramref name="initial"/> if expression never evaluated.
    /// </returns>
    public static T While<T>(
      this Func<T, T> expression, T initial, Func<T, bool> predicate)
    {
      return expression.While(initial, initial, predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate and the next evalutation
    /// of the expression. <paramref name="initial"/> is passed to the predicate
    /// and expression as the first input value. Returns the final return value
    /// from the expression or <paramref name="defaultResult"/> if expression is
    /// never evaluated.
    /// </summary>
    /// <typeparam name="T">
    /// Type of input and return value of <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{T,TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="initial">
    /// Initial input for <paramref name="predicate"/> and
    /// <paramref name="expression"/>.
    /// </param>
    /// <param name="defaultResult">Default return value.</param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop or
    /// <paramref name="initial"/> if expression never evaluated.
    /// </returns>
    public static T While<T>(
      this Func<T, T> expression,
      T initial,
      T defaultResult,
      Func<T, bool> predicate)
    {
      // Could rewrite this in terms of one of the other While extension methods
      // but it would be less readable!
      var executed = false;
      var result = initial;
      while (predicate(result))
      {
        executed = true;
        result = expression(result);
      }
      return executed ? result : defaultResult;
    }

    /// <summary>
    /// Execute <paramref name="action"/> while <paramref name="predicate"/> is
    /// true.
    /// </summary>
    /// <param name="action">
    /// <see cref="Action"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true.
    /// </param>
    /// <remarks>
    /// Since <paramref name="action"/> has neither input nor output, and
    /// <paramref name="predicate"/> also takes no input, the loop is entirely
    /// dependent on an external environment, both for input and to
    /// be mutated to output some value, i.e. this is a very non-functional
    /// extension method where side-effects are central to it doing anything
    /// useful.
    /// </remarks>
    /// <example>
    /// One use is to define a readable "Repeat N times" function:
    /// <code>
    /// <![CDATA[
    /// action.While(() => numberOfTimes-- > 0);
    /// ]]>
    /// </code>
    /// But then instead could easily written as:
    /// <code>
    /// <![CDATA[
    /// while (numberOfTimes-- > 0)
    ///   action;
    /// ]]>
    /// </code>
    /// (Can even put it all on one line if you like that sort of thing.)
    /// </example>
    public static void While(this Action action, Func<bool> predicate)
    {
      // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
      while (predicate())
        action();
    }
  }
}
