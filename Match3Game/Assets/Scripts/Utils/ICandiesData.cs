public interface ICandiesData<ICandyData> : IData<ICandyData>
{
    new ICandyData GetData(int index);
}

public interface ICandyData : IItemData
{

}