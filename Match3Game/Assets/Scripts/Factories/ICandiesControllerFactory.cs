public class CandyControllerFactoryParams : IControllerFactoryParams
{
    public ICandyView View;
    public ICandyModel Model;
}

public interface ICandiesControllerFactory : IControllerFactory
{
    
}
