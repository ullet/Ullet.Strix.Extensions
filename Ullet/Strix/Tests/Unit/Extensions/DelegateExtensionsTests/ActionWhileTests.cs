/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensionsTests
{
  [TestFixture]
  public class ActionWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyFalse()
    {
      var executed = false;

      ((Action)(() => executed = true)).While(() => false);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Action)(() => executionCount++)).While(() => executionCount < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
