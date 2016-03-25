using System;
using NUnit.Framework;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensionsTests
{
  [TestFixture]
  public class ActionDoWhileTests
  {
    [Test]
    [Timeout(1000)]
    public void ExecutesAtLeastOnce()
    {
      var executed = false;

      ((Action)(() => executed = true)).DoWhile(() => false);

      Assert.That(executed, Is.True);
    }

    [Test]
    [Timeout(1000)]
    public void ExecutesUntilConditionTrue()
    {
      var executionCount = 0;

      ((Action)(() => executionCount++))
        .DoWhile(() => executionCount < 10);

      Assert.That(executionCount, Is.EqualTo(10));
    }
  }
}
