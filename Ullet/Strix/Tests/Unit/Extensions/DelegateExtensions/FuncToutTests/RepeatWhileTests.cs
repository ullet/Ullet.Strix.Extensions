/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Linq;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.FuncToutTests
{
  [TestFixture]
  public class RepeatWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatWhilePredicateTrue()
    {
      var counter = 0;

      var output =
        ((Func<int>) (() => ++counter)).RepeatWhile(() => counter < 3);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1, 2, 3}));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsEmptyIfPredicateNeverTrue()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).RepeatWhile(() => false);

      Assert.That(output, Is.Empty);
    }
  }
}
