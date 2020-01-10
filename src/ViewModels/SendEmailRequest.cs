using System.Collections.Generic;

namespace PlusUltra.SendPulse.ViewModels
{
    public class Template
    {
        public Template(int id, Dictionary<string, string> variables)
        {
            Id = id;
            Variables = variables;
        }
        public int Id { get; }
        public Dictionary<string, string> Variables { get; }
    }

    public class Address
    {
        public Address(string name, string email)
        {
            this.Email = email;
            this.Name = name;
        }

        public string Email { get; }
        public string Name { get; }
    }

    public class Email
    {
        public Email(string subject, Template template, string text, Address from, IEnumerable<Address> to)
        {
            Subject = subject;
            Template = template;
            Text = text;
            From = from;
            To = to;
        }

        public string Subject { get; }
        public Template Template { get; }
        public string Text { get; }
        public Address From { get; }
        public IEnumerable<Address> To { get; }
    }

    public class SendEmailRequest
    {
        public Email Email { get; private set; }

        public static SendEmailRequest CreateTextEmail(Address from, Address to, string subject, string content)
        {
            var email = new Email(subject,
                                null,
                                content,
                                new Address(from.Name, from.Email),
                                new List<Address> { new Address(to.Name, to.Email) });


            return new SendEmailRequest
            {
                Email = email
            };
        }

        public static SendEmailRequest CreateTemplateEmail(Address from, Address to, string subject, int templateId, Dictionary<string, string> variables)
        {
            var template = new Template(templateId, variables);

            var email = new Email(subject,
                    template,
                    null,
                    new Address(from.Name, from.Email),
                    new List<Address> { new Address(to.Name, to.Email) });


            return new SendEmailRequest
            {
                Email = email
            };
        }
    }
}