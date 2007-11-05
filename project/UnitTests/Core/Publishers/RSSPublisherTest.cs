using NUnit.Framework;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Publishers;
using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.UnitTests.Core.Publishers
{
	[TestFixture]
	public class RSSPublisherTest : IntegrationFixture
	{
		IIntegrationResult result;
		RssPublisher publisher;

		[SetUp]
		public void Setup()
		{
            result = Integration("myProject", @"c:\temp", @"c:\temp");
			result.ProjectUrl = "http://somewhere/someplace.html";
			publisher = new RssPublisher();
		}

		[Test]
		public void ChannelContainsCorrectDetails()
		{
			/// Execute
			Document document = publisher.GenerateDocument(result);

			/// Verify
			Assert.AreEqual("CruiseControl.NET - myProject", document.Channel.Title);
			Assert.AreEqual("http://somewhere/someplace.html", document.Channel.Link);
			Assert.AreEqual("The latest build results for myProject", document.Channel.Description);
		}

		[Test]
		public void FirstItemContainsDetailsFromCurrentResult()
		{
			result.Status = IntegrationStatus.Success;
            result.Label = "1.0";

			/// Execute
			Document document = publisher.GenerateDocument(result);

			Item firstItem = (Item) document.Channel.Items[0];
            Assert.AreEqual("Build 1.0 : Success", firstItem.Title);
            Assert.AreEqual("No Modifications found in Build", firstItem.Description);
            
		}
	}
}
