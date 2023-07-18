using System;

public class CandiesControllerFactory : AbstractControllerFactory<CandyControllerFactoryParams>, ICandiesControllerFactory
{
    public override IController CreateController(Type controllerType, CandyControllerFactoryParams parametres)
    {
        return (IController) CreateItem(controllerType, new object[] { parametres.Model, parametres.View });
    }

    public override TController CreateController<TController>(Type controllerType, CandyControllerFactoryParams parametres)
    {
        return (TController) CreateController(typeof(TController), parametres);
    }
}
