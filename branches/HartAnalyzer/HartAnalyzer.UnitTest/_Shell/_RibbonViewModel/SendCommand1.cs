﻿using FluentAssertions;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Shell._RibbonViewModel
{
    [TestFixture]
    public class SendCommand1
    {
        [Test]
        public async void ShouldSendCommandZero()
        {
            var applicationServices = new ApplicationServices();
            var hartCommunicationService = new TestHartCommunicationService();
            applicationServices.HartCommunicationService = hartCommunicationService;
            hartCommunicationService.PortState = Services.PortState.Opened;
            var viewModel = new RibbonViewModel(applicationServices, new CommonServices());

            await viewModel.SendCommand1.Execute(null);

            var request = hartCommunicationService.SentCommands.Dequeue();

            request.Item1.Should().Be(1);
            request.Item2.Length.Should().Be(0);
        }

        [Test]
        public void CanExecuteShouldBeFalseIfNotConnected()
        {
            var applicationServices = new ApplicationServices();
            var hartCommunicationService = new TestHartCommunicationService();
            applicationServices.HartCommunicationService = hartCommunicationService;
            hartCommunicationService.PortState = Services.PortState.Closing;
            var viewModel = new RibbonViewModel(applicationServices, new CommonServices());

            viewModel.SendCommand1.CanExecute(null).Should().BeFalse();
        }

        [Test]
        public void CanExecuteShouldBeTrueIfPortIsOpened()
        {
            var applicationServices = new ApplicationServices();
            var hartCommunicationService = new TestHartCommunicationService();
            applicationServices.HartCommunicationService = hartCommunicationService;
            hartCommunicationService.PortState = Services.PortState.Opened;
            var viewModel = new RibbonViewModel(applicationServices, new CommonServices());

            viewModel.SendCommand1.CanExecute(null).Should().BeTrue();
        }
    }
}