using Build.Game.Scripts.ECS.EntityActors;

public class ActorVisitor : IActorVisitor
{
    public int AccumulatedScore { get; private set; }

    public int MaxScore { get; private set; } = 200;
    
    public void Visit(EnemyActor enemy)
    {
        AccumulatedScore += 10;
    }

    public void Visit(StoneActor stone)
    {
        AccumulatedScore += 5;
    }
}
