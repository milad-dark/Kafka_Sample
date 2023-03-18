// See https://aka.ms/new-console-template for more information
using KafkaConsole;
using System.Diagnostics;

Console.WriteLine("Kafka Test is running");

Stopwatch sw = new Stopwatch();

sw.Start();
//ProducerEngine.SendMessageToKafka();
//sw.Stop();

//TimeSpan timeTaken = sw.Elapsed;

//Console.WriteLine("Time Elapsed send messages to kafka" + timeTaken.ToString(@"m\:ss\.fff"));

//sw.Restart();
await ConsumerEngine.StartConsumeAsync(CancellationToken.None);
sw.Stop();

TimeSpan readTime = sw.Elapsed;
Console.WriteLine(readTime.ToString(@"m\:ss\.fff"));



