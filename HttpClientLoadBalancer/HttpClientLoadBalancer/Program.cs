using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<ClientOptions>(builder.Configuration.GetRequiredSection("ClientOptions"));
builder.Services.AddSingleton<ServiceResolver>();
builder.Services.AddSingleton<LoadBalancingHandler>();

builder.Services.AddHttpClient("balanced", client => { client.BaseAddress = new Uri("http://localhost"); })
    .AddHttpMessageHandler<LoadBalancingHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

public class ClientOptions
{
    public IEnumerable<string> Uris { get; set; } = new[] { "http://localhost:8080", "http://localhost:8081", "http://localhost:8082" };
}

class CircularIterator<T>
{
    private readonly IEnumerable<T> values;
    private int index;

    public CircularIterator(IEnumerable<T> values)
    {
        this.values = values;
    }

    public T GetNext()
    {
        var result = values.ElementAt(index);

        index++;
        if(index >= values.Count())
        {
            index = 0;
        }

        return result;
    }
}

//inspiration https://docs.steeltoe.io/api/v3/discovery/load-balancing.html
class ServiceResolver 
{
    private readonly IOptions<ClientOptions> clientOptions;
    private readonly ILogger<ServiceResolver> logger;
    private readonly CircularIterator<Uri> iterator;

    public ServiceResolver(IOptions<ClientOptions> clientOptions, ILogger<ServiceResolver> logger)
	{
        this.clientOptions = clientOptions;
        this.logger = logger;

        this.iterator = new CircularIterator<Uri>(clientOptions.Value.Uris.Select(uri => new Uri(uri)));
    }

    public Uri ResolveService(Uri uri)
    {
        return iterator.GetNext();
    }

    public void UpdateStatistics(Uri resolvedUri, HttpStatusCode statusCode)
    {
        //todo: add uri to temporary blacklist
    }

    public void UpdateStatistics(Uri resolvedUri, Exception exception)
    {
        //todo: add uri to temporary blacklist
    }
}

class LoadBalancingHandler: DelegatingHandler
{
    private readonly ServiceResolver resolver;
    private readonly ILogger<LoadBalancingHandler> logger;

    public LoadBalancingHandler(ServiceResolver resolver, ILogger<LoadBalancingHandler> logger)
    {
        this.resolver = resolver;
        this.logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var resolvedUri = resolver.ResolveService(request.RequestUri);
        request.RequestUri = new UriBuilder(resolvedUri.Scheme, resolvedUri.Host, resolvedUri.Port, request.RequestUri.AbsolutePath, request.RequestUri.Query).Uri;

        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                resolver.UpdateStatistics(resolvedUri, response.StatusCode);
            }
            return response;
        }
        catch (Exception ex)
        {
            resolver.UpdateStatistics(resolvedUri, ex);
            throw;
        }
    }
}