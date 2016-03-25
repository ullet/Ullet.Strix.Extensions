using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensionsTests
{
  [TestFixture]
  public class DoWhileTests
  {
    [Test]
    [Timeout(100)]
    public void ExecutesAtLeastOnce()
    {
      var executed = false;

      ((Func<bool>)(() => executed = true)).DoWhile(x => false);

      Assert.That(executed, Is.True);
    }

    [Test]
    [Timeout(100)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Func<int>)(() => executionCount++)).DoWhile(x => executionCount < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }

    [Test]
    [Timeout(100)]
    public void ReturnsLastResultOfExpression()
    {
      var executionCount = 0;

      var result = ((Func<int>) (() =>
      {
        executionCount++;
        return executionCount*2;
      })).DoWhile(x => executionCount < 10);

      Assert.That(result, Is.EqualTo(20));
    }

    [Test]
    [Timeout(100)]
    public void PassesResultOfExpressionToPredicate()
    {
      var executionCount = 0;

      ((Func<int>)(() => ++executionCount)).DoWhile(x => x < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
