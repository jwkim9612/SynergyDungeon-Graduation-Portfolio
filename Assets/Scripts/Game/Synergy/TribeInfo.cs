public class TribeInfo
{
    public TribeInfo(Tribe tribe, int id)
    {
        this.tribe = tribe;
        this.id = id;
    }

    public override bool Equals(object obj)
    {
        TribeInfo tribeInfo = obj as TribeInfo;
        return (tribeInfo.tribe == tribe && tribeInfo.id == id) ? true : false;
    }

    public override int GetHashCode()
    {
        return this.id.GetHashCode();
    }

    public Tribe tribe;
    public int id;
}
