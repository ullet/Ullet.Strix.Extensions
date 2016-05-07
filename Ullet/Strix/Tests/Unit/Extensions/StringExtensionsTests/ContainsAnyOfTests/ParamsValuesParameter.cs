/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.StringExtensionsTests.ContainsAnyOfTests
{
  public class ParamsValuesParameter
  {
    [TestFixture]
    public class WhenNoValues
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("Any old string".ContainsAnyOf(), Is.False);
      }
    }

    [TestFixture]
    public class WhenNullValues
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("Any old string".ContainsAnyOf((string[])null), Is.False);
      }
    }

    [TestFixture]
    public class WhenEmptyString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("".ContainsAnyOf("A", "The", "It"), Is.False);
      }
    }

    [TestFixture]
    public class WhenNullString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(((string)null).ContainsAnyOf("A", "The", "It"), Is.False);
      }
    }

    [TestFixture]
    public class WhenSingleValue
    {
      [Test]
      public void TrueIfStartsWithValue()
      {
        const string value = "The";
        const string str = value + " String";

        Assert.That(str.ContainsAnyOf(value), Is.True);
      }

      [Test]
      public void TrueIfEndsWithValue()
      {
        const string value = "String";
        const string str = "The " + value;

        Assert.That(str.ContainsAnyOf(value), Is.True);
      }

      [Test]
      public void TrueIfValueInMiddle()
      {
        const string value = "other";
        const string str = "The " + value + " string";

        Assert.That(str.ContainsAnyOf(value), Is.True);
      }

      [Test]
      public void FalseIfDoesNotContainValue()
      {
        const string value = "cabbage";
        const string str = "String not containing the value";

        Assert.That(str.ContainsAnyOf(value), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string value = "other";
        const string str = "The Other String";

        Assert.That(str.ContainsAnyOf(value), Is.False);
      }
    }

    [TestFixture]
    public class WhenTwoValues
    {
      [Test]
      public void TrueIfContainsFirstValue()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The " + value + " String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.True);
      }

      [Test]
      public void TrueIfContainsSecondValue()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The" + otherValue + " String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.True);
      }

      [Test]
      public void TrueIfContainsBothValuesInOrder()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The" + value + " or " + otherValue + " String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.True);
      }

      [Test]
      public void TrueIfContainsBothValuesInReverseOrder()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The" + otherValue + " or " + value + " String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.True);
      }

      [Test]
      public void FalseIfDoesNotContainEitherValue()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The third String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string value = "first";
        const string otherValue = "second";
        const string str = "The First or Second String";

        Assert.That(str.ContainsAnyOf(value, otherValue), Is.False);
      }
    }

    [TestFixture]
    public class WhenMultipleValues
    {
      [TestCase("first")]
      [TestCase("second")]
      [TestCase("third")]
      [TestCase("fourth")]
      public void TrueIfContainsAnyOfTheValues(string actualValue)
      {
        var str = "The " + actualValue + " string";

        Assert.That(
          str.ContainsAnyOf("first", "second", "third", "fourth"), Is.True);
      }

      [TestCase("firstsecondthirdfourth")]
      [TestCase("secondthirdfirstfourth")]
      [TestCase("secondfirstfourththird")]
      [TestCase("fourthsecondthirdfirst")]
      public void TrueIfContainsAllOfTheValuesInAnyOrder(string allValues)
      {
        var str = "The " + allValues + " string";

        Assert.That(
          str.ContainsAnyOf("first", "second", "third", "fourth"), Is.True);
      }

      [TestCase("firstsecond")]
      [TestCase("secondthird")]
      [TestCase("secondfirstfourth")]
      [TestCase("fourthsecondthird")]
      public void TrueIfContainsMultipleValuesInAnyOrder(string multiValues)
      {
        var str = "The " + multiValues + " string";

        Assert.That(
          str.ContainsAnyOf("first", "second", "third", "fourth"), Is.True);
      }

      [Test]
      public void FalseIfContainsNoneOfTheValues()
      {
        const string str = "Other String";

        Assert.That(str.ContainsAnyOf("The", "A", "Some", "That"), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string str = "The FirstSecondThirdFourth String";

        Assert.That(
          str.ContainsAnyOf("first", "second", "third", "fourth"), Is.False);
      }
    }
  }
}
