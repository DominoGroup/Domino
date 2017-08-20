using NotImplementedException = System.NotImplementedException;
public class AssetHubRelease : AssetHub
{
    public override T GetAsset<T>(string bundle, string asset)
    {
        throw new NotImplementedException();
    }
}