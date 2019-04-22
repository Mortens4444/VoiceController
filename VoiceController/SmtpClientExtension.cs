using System;
using System.Net.Mail;
using System.Reflection;
using VoiceController.Messages;

namespace VoiceController
{
    public static class SmtpClientExtension
    {
        public static void ForceSmtpAuthentication(this SmtpClient smtpClient, SmtpAuthentication smtpAuthentication)
        {
            var transport = smtpClient.GetType().GetField("transport", BindingFlags.NonPublic | BindingFlags.Instance);
            if (transport != null)
            {
                var authenticationModules = transport.GetValue(smtpClient).GetType().GetField("authenticationModules", BindingFlags.NonPublic | BindingFlags.Instance);
                if (authenticationModules != null)
                {
                    var modulesArray = authenticationModules.GetValue(transport.GetValue(smtpClient)) as Array;
                    if (modulesArray != null)
                    {
                        var smtpAuthenticationModule = modulesArray.GetValue((byte)smtpAuthentication);
                        modulesArray.SetValue(smtpAuthenticationModule, 0);
                        modulesArray.SetValue(smtpAuthenticationModule, 1);
                        modulesArray.SetValue(smtpAuthenticationModule, 2);
                        modulesArray.SetValue(smtpAuthenticationModule, 3);
                    }
                }
            }
        }   
    }
}

