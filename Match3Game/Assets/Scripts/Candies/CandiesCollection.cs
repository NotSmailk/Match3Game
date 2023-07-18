using System.Collections.Generic;
using UnityEngine;

public class CandiesCollection : ICandiesCollection
{
    private ICandiesControllerFactory _candiesControllerFactory;
    private ICandyViewFactory _viewFactory;
    private List<ICandyView> _views;
    private List<ICandyController> _controllers;

    public List<ICandyView> Views => _views;
    public List<ICandyController> Controllers => _controllers;

    public CandiesCollection(ICandyViewFactory viewFactory)
    {
        _views = new List<ICandyView>();
        _controllers = new List<ICandyController>();
        _viewFactory = viewFactory;
        _candiesControllerFactory = new CandiesControllerFactory();
    }

    public ICandyController GetControllerFromGridPos(GridPosition pos)
    {
        foreach (var controller in _controllers)
        {
            if (controller.GetModelInfo().GridPosition == pos)
                return controller;
        }

        return null;
    }

    public ICandyController GetControllerFromView(ICandyView view)
    {
        foreach (var controller in _controllers)
        {
            if (controller.Contains(view))
                return controller;
        }

        return null;
    }

    public ICandyController CreateCandy(CandyData data, Vector3 worldPos, GridPosition gridPosition)
    {
        var view = _viewFactory.CreateItem(worldPos);
        view.Init();
        view.DisplaySprite(data.Sprite);

        var model = new CandyModel(view, data, gridPosition);
        var parametres = new CandyControllerFactoryParams { Model = model, View = view };
        var controller = _candiesControllerFactory.CreateController<CandyController>(parametres);

        _views.Add(view);
        _controllers.Add(controller);
        return controller;
    }

    public void Remove(ICandyController candy)
    {
        _views.Remove(_views[_controllers.IndexOf(candy)]);
        _controllers.Remove(candy);
    }
}
