using Microsoft.AspNetCore.Mvc;



public class ScalableComponent

{
    private static readonly HttpClient client = new HttpClient();
    public ScalableComponent(string instanceName, string instanceURL)
    {
        this.ServiceName = instanceName;
        this.ServiceUrl = instanceURL;
        this.NumberOfTCPConnections = 0;

    }


    public string ServiceUrl { get; private set; }
    public string ServiceName { get; private set; }
    public int NumberOfTCPConnections { get; private set; }
    private static readonly HttpClient httpClient = new HttpClient();
    public async Task<String> callListNumbers(String requestURL)
    {

        String result = await httpClient.GetStringAsync(combineURLs(requestURL: requestURL));

        //TODO implement calling of containerized service
        return result;

    }

    public async Task callAndSetNumberOfTCPConnections()
    {

        String result = await httpClient.GetStringAsync(this.ServiceUrl + "/connection");

        int numberOfConnections = Int32.Parse(result);

        this.NumberOfTCPConnections = numberOfConnections;
    }


    private Uri combineURLs(string requestURL)
    {
        //combine requestURL and service URL

        Uri serviceUri = new Uri(this.ServiceUrl);
        System.Console.WriteLine(requestURL + "requestURL");
        Uri requestUri = new Uri(requestURL);
        //FIXME cant parse ip address as host i think
        UriBuilder modifiedRequestUri = new UriBuilder();
        modifiedRequestUri.Scheme = "http";
        modifiedRequestUri.Host = serviceUri.Host;
        modifiedRequestUri.Port = serviceUri.Port;
        modifiedRequestUri.Path = requestUri.PathAndQuery;
        System.Console.WriteLine(modifiedRequestUri.ToString() + "modified requesr uri");



        return new Uri(modifiedRequestUri.ToString());
    }


}