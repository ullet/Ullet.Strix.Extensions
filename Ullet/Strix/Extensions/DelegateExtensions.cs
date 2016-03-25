﻿using System;

namespace Ullet.Strix.Extensions
{
  public static class DelegateExtensions
  {
    /// <summary>
    /// Evaluate <paramref name="expression"/> at least once until
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
      T result;
      do
      {
        result = expression();
      } while (predicate(result));
      return result;
    }
  }
}
