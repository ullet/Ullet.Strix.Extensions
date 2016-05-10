/*
 * Written by Trevor Barnett, <mr.ullet@gmail.com>, 2016
 * Released to the Public Domain.  See http://unlicense.org/ or the
 * UNLICENSE file accompanying this source code.
 */

using System;
using System.Linq;
using FsCheck;
using NUnit.Framework;

namespace
  Ullet.Strix.Extensions.Tests.Unit.StringExtensionsTests.StartsWithAnyTests
{
  public class ParamsPrefixesParameter
  {
    [TestFixture]
    public class WhenNoPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop.ForAll<string>(s => !s.StartsWithAny())
          .QuickCheckThrowOnFailure();
      }
    }

    [TestFixture]
    public class WhenNullPrefixList
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop.ForAll<string>(s => !s.StartsWithAny((string[])null))
          .QuickCheckThrowOnFailure();
      }
    }

    [TestFixture]
    public class WhenOnlyNullOrEmptyStringPrefixes
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop.ForAll<string>(
          s => Prop.ForAll(
            Arb.From(Gen.ArrayOf(Gen.Elements(null, ""))),
            prefixes => !s.StartsWithAny(prefixes)))
          .QuickCheckThrowOnFailure();
      }
    }

    [TestFixture]
    public class WhenEmptyString
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop.ForAll<string[]>(prefixes => !"".StartsWithAny(prefixes))
          .QuickCheckThrowOnFailure();
      }
    }

    [TestFixture]
    public class WhenNullString
    {
      [Test]
      public void AlwaysFalse()
      {
        Prop.ForAll<string[]>(
          prefixes => !((string) null).StartsWithAny(prefixes))
          .QuickCheckThrowOnFailure();
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

        Assert.That(str.StartsWithAny(prefix), Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithPrefix()
      {
        const string prefix = "The";
        const string str = "Not the String";

        Assert.That(str.StartsWithAny(prefix), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string str = "the String";

        Assert.That(str.StartsWithAny(prefix), Is.False);
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
          str.StartsWithAny(prefix, otherPrefix), Is.True);
      }

      [Test]
      public void TrueIfStartsWithSecondPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = otherPrefix + " String";

        Assert.That(
          str.StartsWithAny(prefix, otherPrefix), Is.True);
      }

      [Test]
      public void FalseIfDoesNotStartsWithEitherPrefix()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "Not the String";

        Assert.That(str.StartsWithAny(prefix, otherPrefix), Is.False);
      }

      [Test]
      public void IsCaseSensitive()
      {
        const string prefix = "The";
        const string otherPrefix = "A";
        const string str = "the String";
        const string otherStr = "a String";

        Assert.That(str.StartsWithAny(prefix, otherPrefix), Is.False);
        Assert.That(otherStr.StartsWithAny(prefix, otherPrefix), Is.False);
      }
    }

    [TestFixture]
    public class WhenMultipleNonEmptyPrefixes
    {
      [Test]
      public void TrueIfStartsWithAnyOfThePrefixes()
      {
        Func<string, bool> notEmpty = s => !string.IsNullOrEmpty(s);
        Func<string[], string[]> removeEmptyStrings =
          pfs => pfs.Where(notEmpty).ToArray();
        Func<string[], bool> keepNonEmptyLists = a => a.Any();
        var arbNonEmptyArrayOfNonEmptyStrings =
          Arb.From<string[]>().MapFilter(removeEmptyStrings, keepNonEmptyLists);
        Func<string[], Arbitrary<string>> arbPickOneFromArray =
          a => Arb.From(Gen.Choose(0, a.Length - 1).Select(i => a[i]));

        Prop.ForAll(
          arbNonEmptyArrayOfNonEmptyStrings,
          prefixes =>
            Prop.ForAll(
              arbPickOneFromArray(prefixes),
              prefix => Prop.ForAll<string>(
                rest => (prefix + rest).StartsWithAny(prefixes))))
          .QuickCheckThrowOnFailure();
      }

      [Test]
      public void FalseIfStartsWithNoneOfThePrefixes()
      {
        const string str = "Other String";

        Assert.That(str.StartsWithAny("The", "A", "Some", "That"), Is.False);
      }

      [TestCase("the")]
      [TestCase("a")]
      [TestCase("some")]
      [TestCase("that")]
      public void IsCaseSensitive(string actualPrefix)
      {
        string str = actualPrefix + " String";

        Assert.That(str.StartsWithAny("The", "A", "Some", "That"), Is.False);
      }
    }
  }
}
