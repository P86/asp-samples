namespace HangfireExample.Jobs
{
    public class SomeJobWithException
    {
        public async Task Execute()
        {
            await Task.Delay(TimeSpan.FromSeconds(20));

            throw new NotImplementedException();
        }
    }
}
