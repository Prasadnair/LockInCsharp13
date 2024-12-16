using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Define a shared lock object
    private static readonly Lock myLock = new Lock();

    // Shared resource
    private static int sharedResource = 0;

    static async Task Main(string[] args)
    {
        // Simulate multiple tasks accessing the shared resource
        var mytask1 = Task.Run(() => AccessSharedResource("My Task 1"));
        var mytask2 = Task.Run(() => AccessSharedResource("My Task 2"));

        // Wait for both tasks to complete
        await Task.WhenAll(mytask1, mytask2);
    }

    static void AccessSharedResource(string taskName)
    {
        // Use scoped lock to synchronize access
        using (myLock.EnterScope())
        {
            Console.WriteLine($"{taskName} entered the lock.");

            // Safely access and modify the shared resource
            sharedResource++;
            Console.WriteLine($"{taskName} incremented the shared resource to {sharedResource}.");

            // Simulate some work
            Thread.Sleep(1000);

            Console.WriteLine($"{taskName} leaving the lock.");
        }
    }
}