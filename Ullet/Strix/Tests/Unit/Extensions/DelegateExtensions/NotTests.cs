/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using NUnit.Framework;
using Fn = Ullet.Strix.Extensions.DelegateExtensions;

namespace Ullet.Strix.Extensions.Tests.Unit.DelegateExtensions
{
  [TestFixture]
  public class NotTests
  {
    [Test]
    public void CanNegateZeroParameterReturnBoolFunction()
    {
      Assert.That(Fn.Not(() => true)(), Is.False);
      Assert.That(Fn.Not(() => false)(), Is.True);
    }

    [Test]
    public void CanNegateOneParameterPredicate()
    {
      Assert.That(Fn.Not(() => true)(), Is.False);
      Assert.That(Fn.Not(() => false)(), Is.True);
    }

    [Test]
    public void CanNegateTwoParameterPredicate()
    {
      Assert.That(Fn.Not((int a, long b) => a > b)(2, 1), Is.False);
      Assert.That(Fn.Not((int a, long b) => a <= b)(2, 1), Is.True);
    }

    [Test]
    public void CanNegateThreeParameterPredicate()
    {
      Assert.That(
        Fn.Not((double a, int b, long c) => a / b > c)(3, 2, 1), Is.False);
      Assert.That(
        Fn.Not((double a, int b, long c) => a / b <= c)(3, 2, 1), Is.True);
    }

    [Test]
    public void CanNegateFourParameterPredicate()
    {
      Assert.That(
        Fn.Not((double a, int b, long c, byte d) => a / b > c + d)(7, 2, 1, 2),
        Is.False);
      Assert.That(
        Fn.Not((double a, int b, long c, byte d) => a / b <= c + d)(7, 2, 1, 2),
        Is.True);
    }

    [Test]
    public void CanNegateFiveParameterPredicate()
    {
      Assert.That(
        Fn.Not((double a, int b, long c, byte d, float e) =>
          a / b > (c + d) / e)(7.0, 2, 5, 1, 3.0f),
        Is.False);
      Assert.That(
        Fn.Not((double a, int b, long c, byte d, float e) =>
          a / b <= (c + d) / e)(7.0, 2, 5, 1, 3.0f),
        Is.True);
    }
  }
}
