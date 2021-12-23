using BestWebCake.Data;

namespace BestWebCake.MyService
{
    public interface ICakeBuilderService
    {
        static CakeBuilder AddCakeCrude<TCakeEntity, TCrudeEntity>() where TCakeEntity : CakeEntity where TCrudeEntity : CrudeEntity
        {
            return new CakeBuilder();
        }
    }
}
