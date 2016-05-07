/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.StringExtensionsTests.StartsWithAnyOfTests
{
  public class ParamsPrefixesParameter
  {
    [TestFixture]
    public class WhenNoPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("Any old string".StartsWithAnyOf(), Is.False);
      }
    }

    [TestFixture]
    public class WhenNullPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("Any old string".StartsWithAnyOf((string[])null), Is.False);
      }
    }

    [TestFixture]
    public class WhenEmptyString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That("".StartsWithAnyOf("A", "The", "It"), Is.False);
      }
    }

    [TestFixture]
    public class WhenNullString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(((string)null).StartsWithAnyOf("A", "The", "It"), Is.False);
      }
    }

    [TestFixture]
    public class WhenSinglePrefix
    {
      [Test]
      public void TrueIfStartsWithPrefix()
      {
        const string prefix = "The";
        const string str = prefix + " String";

        Assert.That(str.StartsWithAnyOf(prefix), Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithPrefix()
      {
        const string prefix = "The";
        const string str = "Not the String";

        Assert.That(str.StartsWithAnyOf(prefix), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string str = "the String";

        Assert.That(str.StartsWithAnyOf(prefix), Is.False);
      }
    }

    [TestFixture]
    public class WhenTwoPrefixes
    {
      [Test]
      public void TrueIfStartsWithFirstPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = prefix + " String";

        Assert.That(
          str.StartsWithAnyOf(prefix, otherPrefix), Is.True);
      }

      [Test]
      public void TrueIfStartsWithSecondPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = otherPrefix + " String";

        Assert.That(
          str.StartsWithAnyOf(prefix, otherPrefix), Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithEitherPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "Not the String";

        Assert.That(str.StartsWithAnyOf(prefix, otherPrefix), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "the String";
        const string otherStr = "a String";

        Assert.That(str.StartsWithAnyOf(prefix, otherPrefix), Is.False);
        Assert.That(otherStr.StartsWithAnyOf(prefix, otherPrefix), Is.False);
      }
    }

    [TestFixture]
    public class WhenMultiplePrefixes
    {
      [TestCase("The")]
      [TestCase("A")]
      [TestCase("Some")]
      [TestCase("That")]
      public void TrueIfStartsWithAnyOfThePrefixes(string actualPrefix)
      {
        var str = actualPrefix + " String";

        Assert.That(str.StartsWithAnyOf("The", "A", "Some", "That"), Is.True);
      }

      [Test]
      public void FalseIfStartsWithNoneOfThePrefixes()
      {
        const string str = "Other String";

        Assert.That(str.StartsWithAnyOf("The", "A", "Some", "That"), Is.False);
      }

      [TestCase("the")]
      [TestCase("a")]
      [TestCase("some")]
      [TestCase("that")]
      public void IsCaseSensitive(string actualPrefix)
      {
        string str = actualPrefix + " String";

        Assert.That(str.StartsWithAnyOf("The", "A", "Some", "That"), Is.False);
      }
    }
  }
}
