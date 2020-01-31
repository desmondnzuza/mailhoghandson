using Emailer.IntegrationTests.Helpers.Models;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Emailer.IntegrationTests
{
    public class EmailerClientTests
    {
        [SetUp]
        public void Setup()
        {
            ClearAllMessages();
        }

        [Test]
        public void SendGreetingEmailTo_WhenSendingEmailToUser_EmailIsDeliveredToUser()
        {
            //Arrange
            var expectedEmailRecepient = $"{System.Guid.NewGuid()}@somewhere.com";
            var expectedEmailSender = "someone@example.com";
            var expectedEmailSubject = "Greetings!";
            var expectedEmailBody = "<h1>Hi</h1>This is a <b>greetings message</b>.";

            //Act
            PostToEmailService(expectedEmailRecepient);

            //Assert
            Assert.Multiple(() =>
            {
                var emailMessage = RetrieveMessagesFromMailHog();

                var actualEmailRecepient = emailMessage.Content.Headers.To.First();
                var actualEmailSender = emailMessage.Content.Headers.From.First();
                var actualEmailSubject = emailMessage.Content.Headers.Subject.First();
                var actualEmailBody = emailMessage.Content.Body;

                Assert.That(actualEmailRecepient, Is.EqualTo(expectedEmailRecepient));
                Assert.That(actualEmailSender, Is.EqualTo(expectedEmailSender));
                Assert.That(actualEmailSubject, Is.EqualTo(expectedEmailSubject));
                Assert.That(actualEmailBody, Does.Contain(expectedEmailBody));
            });
            Assert.Pass();
        }

        private void PostToEmailService(string emailAddressToUse)
        {
            var restClient = new RestClient("http://emailer-service:80");
            var request = new RestRequest("api/email", Method.POST);

            request.AddJsonBody(new { EmailAddressToUse = emailAddressToUse });

            restClient.Execute(request);
        }

        private EmailMessage RetrieveMessagesFromMailHog()
        {
            var restClient = new RestClient("http://mailhog:8025");
            var request = new RestRequest("api/v1/messages", Method.GET);

            var messages = restClient.Execute<List<EmailMessage>>(request);

            return messages.Data.First();
        }

        private void ClearAllMessages()
        {
            var restClient = new RestClient("http://mailhog:8025");
            var request = new RestRequest("api/v1/messages", Method.DELETE);

            restClient.Execute(request);
        }
    }
}