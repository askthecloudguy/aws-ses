using System;
using System.Configuration;
using System.Net.Mail;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace CloudGuy.SendQuickEmailWithSES
{
	class Program
	{
		static void Main( string[] args ) {

			using( var client = new AmazonSimpleEmailServiceClient( 
				ConfigurationManager.AppSettings["AWSAccessKey"], 
				ConfigurationManager.AppSettings["AWSSecretKey"], 
				ConfigurationManager.AppSettings["AWSRegionEndpoint"] ) ) {

				var mailMsg = new MailMessage();
				string emailBody = "<h1>Hello, World!</h1><p>This is my first email with AWS Simple Email Service.</p>";
				mailMsg.IsBodyHtml = true;
				mailMsg.Body = emailBody;
				mailMsg.Subject = "Hello From The Cloud Guy";
				mailMsg.To.Add( "YOUR TO EMAIL" );
				mailMsg.From = new MailAddress( ConfigurationManager.AppSettings["AWSDefaultMailFrom"], "Ask The Cloud Guy" );

				var destination = new Destination();
				destination.ToAddresses.Add( "YOUR TO EMAIL" );
				var subject = new Content( "Hello From The Cloud Guy" );
				var textBody = new Content( "<h1>Hello, World!</h1><p>This is my first email with AWS Simple Email Service.</p>" );
				var body = new Body( textBody );
				body.Html = textBody;

				var request = new SendEmailRequest(
					$"Ask The Cloud Guy <{ConfigurationManager.AppSettings["AWSDefaultMailFrom"]}>",
					destination,
					new Message( subject, body ) );

				var response = client.SendEmail( request );
				if( response != null ) {
					Console.WriteLine($"Email sent. Status: {response.HttpStatusCode}");
				} else {
					Console.WriteLine( "Problem sending email" );
				}
			}

		}
	}
}
