using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace BotProject.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            var foo = context.MakeMessage();
            foo.Attachments = new List<Attachment>();
            foo.Attachments.Add(new Attachment {
                ContentUrl = "http://www.trythisforexample.com/images/example_logo.png",
                ContentType = "image/png"

            });
            await context.PostAsync(foo);
            context.Wait(MessageReceivedAsync);
        }
    }
}