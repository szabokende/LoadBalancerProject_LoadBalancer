using Microsoft.AspNetCore.Mvc;


namespace LoadBalancerProject_LoadBalancer.Controllers;

[ApiController]
[Route("[controller]")]
public class DistributeRequestController : ControllerBase
{

    private List<ScalableComponent> connections = new List<ScalableComponent>{
        new ScalableComponent(instanceName:"first_instance", instanceURL:"http://192.168.1.103:8080"),
        new ScalableComponent(instanceName:"second_instance", instanceURL:"http://192.168.1.102:8080"),
    };

    [HttpGet("{requestURL}")]
    public async Task<String> Get(String requestURL)
    {

        requestURL = requestURL.Replace("%2F", "/");
        System.Console.WriteLine(requestURL);
        //ScalableComponent component = RandomizedRoundRobinLoadBalancer.SelectRandom(connections: connections);
        ScalableComponent component = await LeastConnectionsLoadBalancer.SelectServiceWithLeastConnections(components: connections);

        System.Console.WriteLine(component.ServiceName);
        String response = await component.callListNumbers(requestURL: requestURL);
        // TODO figure out a way to forward the response from the service 
        // TODO implement load distribution choice via a settings file 

        //System.Console.WriteLine(response);

        return response;
    }
}
