﻿using NUnit.Framework;

namespace ChessTest
{
	public abstract class TestBase
	{
		[TestFixtureSetUp]
		public abstract void DoSetUp();

		[TestFixtureTearDown]
		public abstract void DoTearDown();
	}
}
