using System.IO;
using NUnit.Framework;

namespace Finaltec.Communication.HartLite.UnitTest._HartCommunicationLite
{
    [TestFixture, Category("Manual")]
    public class Send
    {
        static Send()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("Finaltec.Communication.HartLite.UnitTest.log4net"));
        }

        [Test]
        public void Usage()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1")
                                                      {
                                                          AutomaticZeroCommand = false
                                                      };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult commandResult = communication.SendZeroCommand();
            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(0));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            commandResult = communication.Send(12);
            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(12));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }

        [Test]
        public void ShouldSendOnlyOneZeroCommand()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1")
                                                      {
                                                          AutomaticZeroCommand = true
                                                      };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult commandResult = communication.SendZeroCommand();
            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(0));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            commandResult = communication.Send(12);
            Assert.That(commandResult, Is.Not.Null);
            Assert.That(commandResult.CommandNumber, Is.EqualTo(12));
            Assert.That(commandResult.ResponseCode.FirstByte, Is.EqualTo(0));

            communication.Close();
        }

        [Test]
        public void ShouldReturnNullIfNoZeroCommandSend()
        {
            HartCommunicationLite communication = new HartCommunicationLite("COM1")
                                                      {
                                                          AutomaticZeroCommand = false
                                                      };

            OpenResult openResult = communication.Open();
            Assert.That(openResult, Is.EqualTo(OpenResult.Opened));

            CommandResult commandResult = communication.Send(12);
            Assert.That(commandResult, Is.Null);

            communication.Close();
        }
    }
}