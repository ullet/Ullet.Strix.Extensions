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
  public class RepeatUntilTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatUntilPredicateTrue()
    {
      var counter = 0;

      var output =
        ((Func<int>) (() => ++counter)).RepeatUntil(() => counter >= 3);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1, 2, 3}));
    }

    [Test]
    [Timeout(1000)]
    public void ReturnsEmptyIfPredicateNeverFalse()
    {
      var counter = 0;

      var output = ((Func<int>) (() => ++counter)).RepeatUntil(() => true);

      Assert.That(output, Is.Empty);
    }
  }
}
