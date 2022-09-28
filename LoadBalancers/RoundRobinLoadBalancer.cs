class RandomizedRoundRobinLoadBalancer
{

    private static readonly Random Random = new Random();

    public static ScalableComponent SelectRandom(List<ScalableComponent> connections)
    {
        int index;
        lock (Random)
        {
            index = Random.Next(0, connections.Count);
        }

        return connections[index];
    }

    //TODO implement a different load balancing strategy in another file

}