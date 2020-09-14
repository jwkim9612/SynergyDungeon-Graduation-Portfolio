public class ServiceManager : MonoSingleton<ServiceManager>
{
    public void  Initialize()
    {
        RuneService.Initialize();
        PotionService.Initialize();
    }
}
