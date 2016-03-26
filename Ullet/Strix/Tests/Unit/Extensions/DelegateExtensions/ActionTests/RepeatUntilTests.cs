/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions.ActionTests
{
  [TestFixture]
  public class RepeatUntilTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyTrue()
    {
      var executed = false;

      ((Action)(() => executed = true)).RepeatUntil(() => true);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Action)(() => executionCount++))
        .RepeatUntil(() => executionCount >= 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
