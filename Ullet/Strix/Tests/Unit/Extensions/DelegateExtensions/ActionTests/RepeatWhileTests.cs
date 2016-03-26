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
  public class RepeatWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void DoesNotExecuteOnceIfPredicateInitiallyFalse()
    {
      var executed = false;

      ((Action)(() => executed = true)).RepeatWhile(() => false);

      Assert.That(executed, Is.False);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesWhileConditionTrue()
    {
      var executionCount = 0;

      ((Action)(() => executionCount++)).RepeatWhile(() => executionCount < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
