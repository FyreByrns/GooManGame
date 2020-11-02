using System;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace GooManGame {
    /// <summary>
    /// Asynchronous debugging to console.
    /// </summary>
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

        /// <summary>
        /// Raise a regular message.
        /// </summary>
        public static void Raise(string message) =>
            messages.Add(new Message() { message = message, colour = ConsoleColor.White });
        /// <summary>
        /// Raise a warning message.
        /// </summary>
        public static void RaiseWarning(string message) =>
            messages.Add(new Message() { colour = ConsoleColor.Yellow, message = message });
        /// <summary>
        /// Raise an error message.
        /// </summary>
        public static void RaiseError(string error) =>
            messages.Add(new Message() { colour = ConsoleColor.Red, message = error });

        struct Message {
            public ConsoleColor colour;
            public string message;
        }
    }
}
