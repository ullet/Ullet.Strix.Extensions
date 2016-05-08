/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ullet.Strix.Extensions.Tests.Unit.Support
{
  public static class Predicates<TValue>
  {
    public static Func<TValue, bool> True
    {
      get { return _ => true; }
    }

    public static Func<TValue, bool> False
    {
      get { return _ => false; }
    }

    public static Func<TValue, bool> Null
    {
      get { return null; }
    }

    public static IEnumerable<Func<TValue, bool>> AllTrue(int count)
    {
      return Enumerable.Repeat(True, count);
    }

    public static IEnumerable<Func<TValue, bool>> AllFalse(int count)
    {
      return Enumerable.Repeat(False, count);
    }

    public static IEnumerable<Func<TValue, bool>> AllNull(int count)
    {
      return Enumerable.Repeat(Null, count);
    }
  }
}
