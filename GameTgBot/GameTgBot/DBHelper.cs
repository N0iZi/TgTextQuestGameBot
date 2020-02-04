using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTgBot.Model;

namespace GameTgBot
{
    public static class DBHelper
    {

        public static int GetChatCurrentStatus(long chatId)
        {
            using (var context = new Entities())
            {
                var user = context.Users
                    .Where(u => u.chat_id == chatId)
                    .FirstOrDefault();
                if (user == null)
                    return -1;
                else
                    return user.current_message_id;
            }
        }

        public static bool CheckIsNewUser(long chatId)
        {
            using (var context = new Entities())
            {
                var user = context.Users
                    .Where(u => u.chat_id == chatId)
                    .FirstOrDefault();
                if (user == null)
                    return true;
                else
                    return false;
            }
        }

        public static void RegisterNewUser(long chatId)
        {
            using (var context = new Entities())
            {
                var user = context.Users
                    .Where(u => u.chat_id == chatId)
                    .FirstOrDefault();
                if (user == null)
                {
                    user = new Users()
                    {
                        chat_id = chatId,
                        current_message_id = 1
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                else
                    throw new Exception("USER_EXISTS");
            }
        }

        public static void RestartGame(long chatId)
        {
            using (var context = new Entities())
            {
                var user = context.Users
                    .Where(u => u.chat_id == chatId)
                    .FirstOrDefault();
                if (user == null)
                    RegisterNewUser(chatId);
                else
                {
                    user.current_message_id = 1;
                    context.SaveChanges();
                }
            }
        }

        public static Dictionary<string, int> GetNextOptions(int messageId)
        {
            var options = new Dictionary<string, int>();
            using (var context = new Entities())
            {
                var message = context.Messages
                    .Where(msg => msg.id == messageId)
                    .FirstOrDefault();
                if (message == null)
                    throw new NullReferenceException("MESSAGE_NOT_FOUND");
                else
                {
                    foreach (var opt in message.Replies)
                        options.Add(opt.content, opt.id);
                    return options;
                }
            }
        }

        public static void GetStartMessage(out int messageId, out string messageContent)
        {
            using (var context = new Entities())
            {
                var message = context.Messages
                    .Where(msg => msg.id == 1)
                    .FirstOrDefault();
                messageId = 1;
                messageContent = message.content;
            }
        }

        public static void GetCurrentMessage(long chatId, out int messageId, out string messageContent, out bool isMessageFinal)
        {
            messageId = default; messageContent = default; isMessageFinal = default;
            using (var context = new Entities())
            {
                var user = context.Users
                    .Where(u => u.chat_id == chatId)
                    .FirstOrDefault();
                if (user == null)
                    throw new NullReferenceException("USER_NOT_FOUND");
                else
                {
                    messageId = user.Messages.id;
                    messageContent = user.Messages.content;
                    isMessageFinal = user.Messages.is_final;
                }
            }
        }

        public static void MoveToNextMessage(long chatId, int replyId, out int newMessageId, out string newMessageContent, out bool isNextMessageFinal)
        {
            newMessageId = default; newMessageContent = default; isNextMessageFinal = default;
            using (var context = new Entities())
            {
                var reply = context.Replies
                    .Where(rply => rply.id == replyId)
                    .FirstOrDefault();
                if (reply == null)
                    throw new NullReferenceException("REPLY_NOT_FOUND");
                else
                {
                    var user = context.Users
                        .Where(u => u.chat_id == chatId)
                        .FirstOrDefault();
                    if (user == null)
                        throw new NullReferenceException("USER_NOT_FOUND");
                    else
                    {
                        newMessageId = reply.next_message_id;
                        newMessageContent = reply.MessagesNext.content;
                        isNextMessageFinal = reply.MessagesNext.is_final;
                        user.current_message_id = reply.next_message_id;
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}
