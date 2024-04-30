using Phygital.DAL;

namespace Phygital.BL.Managers;

public class PlatformManager : IPlatformManager
{
    private readonly IPlatformRepository _platformRepository;

    public PlatformManager(IPlatformRepository platformRepository)
    {
        _platformRepository = platformRepository;
    }
}