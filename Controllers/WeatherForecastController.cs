using Microsoft.AspNetCore.Mvc;


namespace LoadBalancerProject_LoadBalancer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private List<ScalableComponent> connections = new List<ScalableComponent>{
        new ScalableComponent(serviceName:"first_sevice", serviceURL:"http://192.168.1.102"),
        new ScalableComponent(serviceName:"second_sevice", serviceURL:"http://192.168.1.102"),
    };



    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }


    [HttpGet("{requestURL}")]
    public async Task<String> Get(String requestURL)
    {

        requestURL = requestURL.Replace("%2F", "/");
        System.Console.WriteLine(requestURL);
        ScalableComponent component = RoundRobinLoadBalancer.SelectRandom(connections: connections);
        System.Console.WriteLine(component.ServiceName);
        String response = await component.callService(requestURL: requestURL);
        // TODO figure out a way to forward the response from the service 
        // TODO implement load distribution choice via a settings file 

        System.Console.WriteLine(response);

        return response;
    }
}
