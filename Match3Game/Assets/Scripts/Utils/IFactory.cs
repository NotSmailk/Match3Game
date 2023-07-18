using System;

public interface IFactoryItem
{

}

public interface IFactory
{
    IFactoryItem CreateItem(Type type, object[] parametres);
}

public abstract class AbstractFactory : IFactory
{
    public virtual IFactoryItem CreateItem(Type type, object[] parametres)
    {
        return (IFactoryItem)Activator.CreateInstance(type, parametres);
    }
}