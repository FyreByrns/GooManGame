using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace GooManGame {
    public static class Debug {
        static BlockingCollection<Message> messages = new BlockingCollection<Message>();

        static Debug() {
            void write(Message message) {
                Console.ForegroundColor = message.colour;
                Console.WriteLine(message.message);
                Console.ResetColor();
            }

            Task.Run(() => {
                while (true)
                    write(messages.Take());
            });
        }

        public static void Raise(string message) =>
            messages.Add(new Message() { message = message, colour = ConsoleColor.White });
        public static void RaiseWarning(string message) =>
            messages.Add(new Message() { colour = ConsoleColor.Yellow, message = message });
        public static void RaiseError(string error) =>
            messages.Add(new Message() { colour = ConsoleColor.Red, message = error });

        struct Message {
            public ConsoleColor colour;
            public string message;
        }
    }
}
