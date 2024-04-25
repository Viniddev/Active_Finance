using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RPA_Teste.Telegram
{
    internal class TelegramApi
    {
        private static TelegramBotClient _botClient = new TelegramBotClient("7138305614:AAF2KBe6uMKdxZxHG4aeYbSZc2n5A8hzs_Y");
        private static long chatId = -1002090888464; // Id do grupo RPA Tora

        public static async Task SendMessageAsync(string message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(chatId, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
        public static async Task SendImageAsync(string imagePath, string caption)
        {
            try
            {
                if (!System.IO.File.Exists(imagePath))
                {
                    Console.WriteLine($"Error: Image file '{imagePath}' does not exist.");
                }

                await using (Stream stream = System.IO.File.OpenRead(imagePath))
                {
                    await _botClient.SendPhotoAsync(chatId, photo: InputFile.FromStream(stream, Path.GetFileName(imagePath)), caption: caption);
                    Console.WriteLine($"Image '{Path.GetFileName(imagePath)}' sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending image: {ex.Message}");
            }
        }
    }
}
