public class OriginInfo
{
    public OriginInfo(Origin origin, int id)
    {
        this.origin = origin;
        this.id = id;
    }

    public override bool Equals(object obj)
    {
        OriginInfo originInfo = obj as OriginInfo;
        return (originInfo.origin == origin && originInfo.id == id) ? true : false;
    }

    public override int GetHashCode()
    {
        return this.id.GetHashCode();
    }

    public Origin origin;
    public int id;
}
