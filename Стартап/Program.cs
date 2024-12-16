using Telegram.Bot;
using Telegram.Bot.Args;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("7672302129:AAGkhkHZWnI5EfXsQ6x8fQzbdGnG4x6wWSM", cancellationToken: cts.Token);
var me = await bot.GetMeAsync();
bot.OnError += OnError;
bot.OnMessage += OnMessage;
bot.OnUpdate += OnUpdate;


Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot

// method to handle errors in polling or in your OnMessage/OnUpdate code
async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception); // just dump the exception to the console
}

// method that handle messages received by the bot:
async Task OnMessage(Message msg, UpdateType type)

{
    var replyMarkup = new ReplyKeyboardMarkup(true);
    var inlineMarkup = new InlineKeyboardMarkup();

    var sent = await bot.SendTextMessageAsync(msg.Chat, "Wake up samurai", replyMarkup: replyMarkup);
    if (msg.Text == "/start")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Что хочешь выбрать?", replyMarkup: new InlineKeyboardMarkup().AddButtons("Стартап!", "Для разработчика"));
            
    }
    if (msg.Text == "Стартап!")
    {
        var send = await bot.SendTextMessageAsync(msg.Chat, "Пожалуйста поставьте 5 :)", replyMarkup: new InlineKeyboardMarkup().AddButton("Что-же?"));
    }

}

// method that handle other types of updates received by the bot:
async Task OnUpdate(Update update)
{
    if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
    {
        await bot.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
        //await bot.SendTextMessageAsync(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
        if (query.Data == "Стартап!")
        {
            Random rnd = new Random();
            int s = rnd.Next(1, 6);
            if (s == 1)
            {
                await bot.SendTextMessageAsync(query.Message.Chat, "Умные тапочки. Сделайте производство тапочек с встроенным Bluetooth, которые будут мигать разными цветами в зависимости от вашего настроения и издавать смешные звуки при ходьбе.");
            }
            if(s == 2)
            {
                await bot.SendTextMessageAsync(query.Message.Chat, "Начните снимать курсы о ограблении банков и продавать их.");

            }
            if (s == 3)
            {
                await bot.SendTextMessageAsync(query.Message.Chat, "Заполучите шаттл SpaceX и полетите на Марс прихватив с собой всякие инструменты и колонизируйте Марс, с последующей продажей участков.");

            }
            if (s == 4)
            {
                await bot.SendTextMessageAsync(query.Message.Chat, "Купите сервер и создайте свою мемную криптовалюту, чтобы затем все в неё вложились и вы получите (или потеряете) миллионы.");

            }
            if (s == 5)
            {
                await bot.SendTextMessageAsync(query.Message.Chat, "Найдите людей с голосами похожими на голос знаменитостей и создайте Сервис по которому можно арендовать эти голоса, мол вы проводите время рядом с Илоном Маском например.");

            }
        }
        if (query.Data == "Для разработчика") {
            await bot.SendTextMessageAsync(query.Message.Chat, "1 - Напишете привет, чтобы получить ответ. 2 - Напишите Стикер чтобы получить стикер");
            async Task OnMessage(Message msg, UpdateType type)
            {
                if (msg.Text == "Привет")
                {
                    await bot.SendTextMessageAsync(query.Message.Chat, "Привет Samurai");
                }
                if (msg.Text == "Стикер")
                {
                    
                }
                if (msg.Text == "Видео")
                {
                    
                }
            }
        }
    }
   
}





