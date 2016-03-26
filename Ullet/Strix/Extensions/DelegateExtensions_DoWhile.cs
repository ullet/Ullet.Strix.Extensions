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
    /// Evaluate <paramref name="expression"/> at least once while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate. Returns the final return
    /// value from expression.
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
    /// The value returned from expression in the final iteration of the loop.
    /// </returns>
    /// <remarks>
    /// Since <paramref name="expression"/> does not have any input parameters,
    /// the function must be dependent on some external input in order for its
    /// result to vary with each iteration of the loop.  This external input
    /// could be, for example, a captured variable in a closure, or read from a
    /// file system.
    /// </remarks>
    /// <example>
    /// Generate a file name that doesn't already exist on the file system.
    /// <code>
    /// <![CDATA[
    /// Func<string> fileNameGenerator = Path.GetRandomFileName;
    /// var fileName = fileNameGenerator.DoWhile(File.Exists);
    /// ]]>
    /// </code>
    /// Or written as a single statement (cast to Func required):
    /// <code>
    /// <![CDATA[
    /// var fileName =
    ///   ((Func<string>)Path.GetRandomFileName).DoWhile(File.Exists);
    /// ]]>
    /// </code>
    /// This could instead be written as a do..while() loop:
    /// <code>
    /// <![CDATA[
    /// string fileName;
    /// do {
    ///   fileName = Path.GetRandomFileName();
    /// } while (File.Exists(fileName));
    /// ]]>
    /// </code>
    /// </example>
    public static T DoWhile<T>(this Func<T> expression, Func<T, bool> predicate)
    {
      return ((Func<T, T>) (t => expression())).DoWhile(predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> at least once while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate and the next evalutation
    /// of the expression. Default of <typeparamref name="T"/> is passed to the
    /// first evaluation of expression. Returns the final return value from
    /// expression.
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
    /// The value returned from expression in the final iteration of the loop.
    /// </returns>
    public static T DoWhile<T>(
      this Func<T, T> expression, Func<T, bool> predicate)
    {
      return expression.DoWhile(default(T), predicate);
    }

    /// <summary>
    /// Evaluate <paramref name="expression"/> at least once while
    /// <paramref name="predicate"/> is true. Result of each evaluation of
    /// epression is passed as input to the predicate and the next evalutation
    /// of the expression. The specified initial value is passed to the first
    /// evaluation of expression. Returns the final return value from the
    /// expression.
    /// </summary>
    /// <typeparam name="T">
    /// Type of input and return value of <paramref name="expression"/>.
    /// </typeparam>
    /// <param name="expression">
    /// <see cref="Func{T,TResult}"/> to execute in each iteration of the loop.
    /// </param>
    /// <param name="initial">
    /// Initial value passed to the first call of <paramref name="expression"/>.
    /// </param>
    /// <param name="predicate">
    /// <see cref="Func{T,TResult}"/> to test for termination of loop. Loop
    /// exists when predicate function evaluates to true. Takes as input the
    /// return value of <paramref name="expression"/> for each iteration.
    /// </param>
    /// <returns>
    /// The value returned from expression in the final iteration of the loop.
    /// </returns>
    public static T DoWhile<T>(
      this Func<T, T> expression, T initial, Func<T, bool> predicate)
    {
      return expression.While(expression(initial), predicate);
    }

    /// <summary>
    /// Execute <paramref name="action"/> at least once while
    /// <paramref name="predicate"/> is true.
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
    public static void DoWhile(this Action action, Func<bool> predicate)
    {
      do
      {
        action();
        // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
      } while (predicate());
    }
  }
}
