
namespace Game.Entity
{
    public interface IMovement
    {
        

        // configured by inspector, exposed throught property
        float SpeedWalkForward { get; }
        float SpeedRunForward { get; }
        float JumpForce { get; }


        // calculated in runtime, exposed by method
        bool IsWalking();
        bool IsJumping();

    }
}