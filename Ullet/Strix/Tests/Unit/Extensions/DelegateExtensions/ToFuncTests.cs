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
  public class ToFuncTests
  {
    [Test]
    public void CanConvertFuncToPredicate()
    {
      Predicate<string> predicate = s => s.Length > 0;

      Func<string, bool> func = predicate.ToFunc();

      Assert.That(func("a string"), Is.True);
    }
  }
}
