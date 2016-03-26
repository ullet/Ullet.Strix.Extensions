/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions
{
  [TestFixture]
  public class ToPredicateTests
  {
    [Test]
    public void CanConvertFuncToPredicate()
    {
      Func<string, bool> func = s => s.Length > 0;

      Predicate<string> predicate = func.ToPredicate();

      Assert.That(predicate("a string"), Is.True);
    }
  }
}
