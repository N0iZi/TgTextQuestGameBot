using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace GameTgBot
{
    class Program
    {

        static ITelegramBotClient botClient;

        static void Main(string[] args)
        {

            botClient = new TelegramBotClient("976746314:AAHXmyYNxwZGhC50grdPCpR8PnMCJ53OchQ");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You said:\n" + e.Message.Text);
            }
        }
    }
}
