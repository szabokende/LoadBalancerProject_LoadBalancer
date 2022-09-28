class LeastConnectionsLoadBalancer
{


    public async static Task<ScalableComponent> SelectServiceWithLeastConnections(List<ScalableComponent> components)
    {


        foreach (ScalableComponent component in components)
        {
            await component.callAndSetNumberOfTCPConnections();

        }
        ScalableComponent componentWithLeastConnections = components.OrderBy(p => p.NumberOfTCPConnections).FirstOrDefault();
        System.Console.WriteLine("Component with least connections: " + componentWithLeastConnections.ServiceName);

        return componentWithLeastConnections;
    }


}