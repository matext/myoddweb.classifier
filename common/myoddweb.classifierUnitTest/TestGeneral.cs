﻿using NUnit.Framework;
using System;

namespace myoddweb.classifierUnitTest
{
  [TestFixture]
  public class TestGeneral : TestCommon
  {
    public TestGeneral()
    {
      ReleaseEngine(false);
    }

    [TearDown]
    public void CleanupTest()
    {
      ReleaseEngine(false);
    }

    [OneTimeTearDown]
    public void ClassCleanup()
    {
      ReleaseEngine(true);
    }

    [Test]
    public void TestGetVersionNumber()
    {
      var versionNumber = TheEngine.GetEngineVersionNumber();
      Assert.AreEqual(1007002, versionNumber );
    }

    [Test]
    public void TestGetVersion()
    {
      var version = TheEngine.GetEngineVersion();
      Assert.AreEqual( new Version(1, 7, 2, 0), version);
    }
  }
}
