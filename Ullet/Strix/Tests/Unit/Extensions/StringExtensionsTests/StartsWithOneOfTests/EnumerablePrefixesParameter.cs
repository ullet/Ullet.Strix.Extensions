/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.StringExtensionsTests.StartsWithOneOfTests
{
  public class EnumerablePrefixesParameter
  {
    [TestFixture]
    public class WhenNoPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".StartsWithOneOf(Enumerable.Empty<string>()),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenNullPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "Any old string".StartsWithOneOf((IEnumerable<string>) null),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenEmptyString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          "".StartsWithOneOf((IEnumerable<string>)new [] {"A", "The", "It"}),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenNullString
    {
      [Test]
      public void AlwaysFalse()
      {
        Assert.That(
          ((string) null).StartsWithOneOf(
            (IEnumerable<string>)new[] { "A", "The", "It" }),
          Is.False);
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

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) new[] {prefix}), Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithPrefix()
      {
        const string prefix = "The";
        const string str = "Not the String";

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) new[] { prefix }),
          Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string str = "the String";

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) new[] { prefix }),
          Is.False);
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
          str.StartsWithOneOf(
            (IEnumerable<string>) new[] {prefix, otherPrefix}),
          Is.True);
      }

      [Test]
      public void TrueIfStartsWithSecondPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = otherPrefix + " String";

        Assert.That(
          str.StartsWithOneOf(
            (IEnumerable<string>) new[] {prefix, otherPrefix}),
          Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithEitherPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "Not the String";

        Assert.That(
          str.StartsWithOneOf(
            (IEnumerable<string>) new[] {prefix, otherPrefix}),
          Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "the String";
        const string otherStr = "a String";

        Assert.That(
          str.StartsWithOneOf(
            (IEnumerable<string>) new[] {prefix, otherPrefix}),
          Is.False);
        Assert.That(
          otherStr.StartsWithOneOf(
            (IEnumerable<string>) new[] {prefix, otherPrefix}),
          Is.False);
      }
    }

    [TestFixture]
    public class WhenMultiplePrefixes
    {
      [TestCase(0)]
      [TestCase(1)]
      [TestCase(2)]
      [TestCase(3)]
      public void TrueIfStartsWithOneOfThePrefixes(int prefixIndex)
      {
        var prefixes = new[]{"The", "A", "Some", "That"};
        var prefix = prefixes[prefixIndex];
        string str = prefix + " String";

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) prefixes), Is.True);
      }

      [Test]
      public void FalseIfStartsWithNoneOfThePrefixes()
      {
        var prefixes = new[] { "The", "A", "Some", "That" };
        const string str = "Other String";

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) prefixes), Is.False);
      }

      [TestCase(0)]
      [TestCase(1)]
      [TestCase(2)]
      [TestCase(3)]
      public void IsCaseSensitive(int prefixIndex)
      {
        var prefixes = new[] { "The", "A", "Some", "That" };
        var prefix = prefixes[prefixIndex].ToLower();
        string str = prefix + " String";

        Assert.That(
          str.StartsWithOneOf((IEnumerable<string>) prefixes), Is.False);
      }
    }
  }
}
