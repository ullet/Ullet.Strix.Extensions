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
  public class RepeatTests
  {
    [Test]
    [Timeout(1000)]
    public void CanRepeatOnce()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(1);

      Assert.That(output.ToArray(), Is.EqualTo(new[] {1}));
    }

    [Test]
    [Timeout(1000)]
    public void CanRepeatMultipleTimes()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(4);

      Assert.That(output.ToArray(), Is.EqualTo(new[] { 1, 2, 3, 4 }));
    }

    [Test]
    [Timeout(1000)]
    public void CanRepeatZeroTimes()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(0);

      Assert.That(output.ToArray(), Is.Empty);
    }

    [Test]
    [Timeout(1000)]
    public void NegativeRepeatsTreatedAsZeroRepeats()
    {
      var counter = 0;

      var output = ((Func<int>)(() => ++counter)).Repeat(-12);

      Assert.That(output.ToArray(), Is.Empty);
    }
  }
}
