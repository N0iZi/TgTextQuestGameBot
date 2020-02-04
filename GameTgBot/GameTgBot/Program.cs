using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

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
            /*if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You said:\n" + e.Message.Text);
            }*/

            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
                if (e.Message.Text == "/start")
                {
                    if (DBHelper.CheckIsNewUser(e.Message.Chat.Id))
                    {
                        DBHelper.RegisterNewUser(e.Message.Chat.Id);
                        DBHelper.GetStartMessage(out int messageId, out string messageContent);
                        string[] optionsTexts = DBHelper.GetNextOptions(messageId).Keys.ToArray();
                        List<KeyboardButton> buttons = new List<KeyboardButton>();
                        foreach (string option in optionsTexts)
                            buttons.Add(new KeyboardButton(option));
                        var markup = new ReplyKeyboardMarkup(buttons);
                        markup.OneTimeKeyboard = true;
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: messageContent, replyMarkup: markup);
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You are alredy registered! Use command /restart to start the game again");
                    }
                }
                else if (e.Message.Text == "/restart")
                {
                    if (DBHelper.CheckIsNewUser(e.Message.Chat.Id))
                    {
                        DBHelper.RegisterNewUser(e.Message.Chat.Id);
                        DBHelper.GetStartMessage(out int messageId, out string messageContent);
                        string[] optionsTexts = DBHelper.GetNextOptions(messageId).Keys.ToArray();
                        List<KeyboardButton> buttons = new List<KeyboardButton>();
                        foreach (string option in optionsTexts)
                            buttons.Add(new KeyboardButton(option));
                        var markup = new ReplyKeyboardMarkup(buttons);
                        markup.OneTimeKeyboard = true;
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: messageContent, replyMarkup: markup);
                    }
                    else
                    {
                        DBHelper.RestartGame(e.Message.Chat.Id);
                        DBHelper.GetStartMessage(out int messageId, out string messageContent);
                        string[] optionsTexts = DBHelper.GetNextOptions(messageId).Keys.ToArray();
                        List<KeyboardButton> buttons = new List<KeyboardButton>();
                        foreach (string option in optionsTexts)
                            buttons.Add(new KeyboardButton(option));
                        var markup = new ReplyKeyboardMarkup(buttons.ToArray());
                        markup.OneTimeKeyboard = true;
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: messageContent, replyMarkup: markup);
                    }
                }
                else if (e.Message.Text == "/help")
                {
                    string helpMessage = "Bot commands\n" +
                        "/start - Register and start new game" +
                        "/restart - Restart the game" +
                        "/repeat - Repeat last message" +
                        "/help - Help message";
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: helpMessage);
                }
                else if (e.Message.Text == "/repeat")
                {
                    if (DBHelper.CheckIsNewUser(e.Message.Chat.Id))
                    {
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You are not registered! Send /start to start the game!");
                    }
                    else
                    {
                        DBHelper.GetCurrentMessage(e.Message.Chat.Id, out int messageId, out string messageContent, out bool isMessageFinal);
                        if (isMessageFinal)
                        {
                            await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You reached the end. Your ending: \"" + messageContent + "\". To start the game again send /restart");
                        }
                        else
                        {
                            string[] optionsTexts = DBHelper.GetNextOptions(messageId).Keys.ToArray();
                            List<KeyboardButton> buttons = new List<KeyboardButton>();
                            foreach (string option in optionsTexts)
                                buttons.Add(new KeyboardButton(option));
                            var markup = new ReplyKeyboardMarkup(buttons.ToArray());
                            markup.OneTimeKeyboard = true;
                            await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: messageContent, replyMarkup: markup);
                        }
                    }
                }
                else
                {
                    if (DBHelper.CheckIsNewUser(e.Message.Chat.Id))
                    {
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You are not registered! Send /start to start the game!");
                    }
                    else
                    {
                        DBHelper.GetCurrentMessage(e.Message.Chat.Id, out int messageId, out string messageContent, out bool isMessageFinal);
                        if (isMessageFinal)
                        {
                            await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "You reached the end. Your ending: \"" + messageContent + "\". To start the game again send /restart");
                        }
                        else
                        {
                            Dictionary<string, int> options = DBHelper.GetNextOptions(messageId);
                            if (!options.Keys.Contains(e.Message.Text))
                                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "ERROR! Answer not found! Try /help to get help.");
                            else
                            {
                                if (options.TryGetValue(e.Message.Text, out int replyId))
                                {
                                    DBHelper.MoveToNextMessage(e.Message.Chat.Id, replyId, out int newMessageId, out string newMessageContent, out bool isNextMessageFinal);
                                    if (isNextMessageFinal)
                                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: newMessageContent + "\nP.S. To start the game again send /restart");
                                    else
                                    {
                                        string[] newOptions = DBHelper.GetNextOptions(newMessageId).Keys.ToArray();
                                        List<KeyboardButton> buttons = new List<KeyboardButton>();
                                        foreach (string option in newOptions)
                                            buttons.Add(new KeyboardButton(option));
                                        var markup = new ReplyKeyboardMarkup(buttons.ToArray());
                                        markup.OneTimeKeyboard = true;
                                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: newMessageContent, replyMarkup: markup);
                                    }
                                }
                                else
                                    throw new NullReferenceException("OPTION_NOT_FOUND");
                            }
                        }
                    }
                }
            }
        }
    }
}
