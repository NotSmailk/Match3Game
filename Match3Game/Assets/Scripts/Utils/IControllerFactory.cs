using System;

public interface IControllerFactoryParams
{

}

public interface IControllerFactory : IFactory
{
    IController CreateController(Type controllerType, IControllerFactoryParams parametres);

    TController CreateController<TController>(IControllerFactoryParams parametres) where TController : IController;
}

public abstract class AbstractControllerFactory<TControllerFactoryParams> : AbstractFactory, IControllerFactory where TControllerFactoryParams : IControllerFactoryParams
{
    public virtual IController CreateController(Type controllerType, TControllerFactoryParams parametres)
    {
        var controller = CreateController(controllerType, parametres);
        return controller;
    }

    public virtual TController CreateController<TController>(Type controllerType, TControllerFactoryParams parametres)
    {
        var controller = CreateController<TController>(controllerType, parametres);
        return controller;
    }

    public IController CreateController(Type controllerType, IControllerFactoryParams parametres)
    {
        var controller = CreateController(controllerType, (TControllerFactoryParams) parametres);
        return controller;
    }

    public TController CreateController<TController>(IControllerFactoryParams parametres) where TController : IController
    {
        var controller = CreateController(typeof(TController), parametres);
        return (TController) controller;
    }
}