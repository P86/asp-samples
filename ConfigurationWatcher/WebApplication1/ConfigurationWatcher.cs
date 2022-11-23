using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class ConfigurationWatcher<T> where T : class
    {
        private readonly ILogger<ConfigurationWatcher<T>> logger;

        public T? CurrentValue { get; private set; }

        public ConfigurationWatcher(IOptionsMonitor<T> options, ILogger<ConfigurationWatcher<T>> logger)
        {
            this.logger = logger;

            options.OnChange(ValidateAndUpdate);
            ValidateAndUpdate(options.CurrentValue);
        }

        private void ValidateAndUpdate(T value)
        {
            logger.LogInformation($"Configutation for {typeof(T).Name} has changed");

            try
            {
                var ctx = new ValidationContext(value);
                Validator.ValidateObject(value, ctx);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                return;
            }

            CurrentValue = value;
        }
    }
}
