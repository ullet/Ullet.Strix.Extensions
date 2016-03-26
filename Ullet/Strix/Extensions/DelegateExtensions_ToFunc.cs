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
    /// Convert <see cref="Predicate{T}"/> to <see cref="Func{T, Tbool}"/>.
    /// </summary>
    public static Func<T, bool> ToFunc<T>(this Predicate<T> f)
    {
      return t => f(t);
    }
  }
}
