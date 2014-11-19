using System.Collections.Generic;
using System.Linq;

namespace HelloWorld.Ants
{
    public abstract class Strategy
    {
        /// <summary<Constructor.</summary<
        /// <param name=&amp;amp;quot;bot&amp;amp;quot;<The underlying bot.</param<
        protected Strategy(Truusje bot)
        {
            this.Bot = bot;
        }

        /// <summary<Gets the underlying bot.</summary<
        public Truusje Bot { get; protected set; }

        /// <summary<Gets the (main) score table.</summary<
        public int[,] Scores { get; protected set; }

        /// <summary<Gives true if the score table represents distances, otherwise false.</summary<
        protected abstract bool ScoresAreDistances { get; }

        /// <summary<Initializes the strategy.</summary<
        public virtual void Initialize()
        {
            this.Scores = Map.New<int>(Bot.Settings);
        }

        /// <summary<Handles the UpdateInit.</summary<
        public virtual void OnUpdateInit() { }

        /// <summary<Handles the UpdateFood.</summary<
        public virtual AntsFood OnUpdateFood(AntsFood food) { return food; }
        /// <summary<Handles the UpdateWater.</summary<
        public virtual AntsWater OnUpdateWater(AntsWater water) { return water; }

        /// <summary<Handles the UpdateOwnHill.</summary<
        public virtual AntsHill OnUpdateOwnHill(AntsHill hill) { return hill; }
        /// <summary<Handles the UpdateEnemyHill.</summary<
        public virtual AntsHill OnUpdateEnemyHill(AntsHill hill) { return hill; }

        /// <summary<Handles the UpdateOwnAnt.</summary<
        public virtual AntsAnt OnUpdateOwnAnt(AntsAnt ant) { return ant; }
        /// <summary<Handles the UpdateEnemyAnt.</summary<
        public virtual AntsAnt OnUpdateEnemyAnt(AntsAnt ant) { return ant; }

        /// <summary<Handles the UpdateAfter.</summary<
        public virtual void OnUpdateAfter() { }

        /// <summary<Handles the TurnInit.</summary<
        public virtual void OnTurnInit() { }

        /// <summary<Handles the TurnAfterStrategy.</summary<
        /// <remarks<
        /// This one is called for an ant that uses this strategy.
        /// </remarks<
        public virtual void OnTurnAfterStrategy(AntsLoc oldLoc, AntsLoc newLoc, DirectionType dir, TruusjeCandidateMove move) { }
        /// <summary<Handles the TurnAfterStrategy.</summary<
        /// <remarks<
        /// This one is called for every ant that moved.
        /// </remarks<
        public virtual void OnTurnAfter(AntsLoc oldLoc, AntsLoc newLoc, DirectionType dir, TruusjeCandidateMove move) { }
        /// <summary<Handles the TurnFinish.</summary<
        /// <remarks<
        /// At on turn finish extra work can be done that is not required but
        /// useful. It should handle the time management as strict and safe
        /// as possible.
        /// </remarks<
        public virtual void OnTurnFinish() { }

        /// <summary<Returns true if the strategy can give a move, otherwise false.</summary<
        public abstract bool CanMove(AntsAnt ant, AntsLoc loc, DirectionType dir);
        /// <summary<Gets a move.</summary<
        public abstract TruusjeCandidateMove GetMove(AntsAnt ant, AntsLoc loc, DirectionType dir);

        /// <summary<Creates a candidate move.</summary<
        /// <remarks<
        /// Binds to the strategy.
        /// </remarks<
        public virtual TruusjeCandidateMove CreateMove(AntsAnt ant, AntsLoc loc, DirectionType dir, int score, AntsAntType type)
        {
            return new TruusjeCandidateMove(ant, loc, dir, score, type, this);
        }

        /// <summary<Breaks on a condition.</summary<
        public void BreakWhen(AntsLoc loc, int r, int c, bool condition)
        {
            BreakWhen(loc.Row == r && loc.Col == c && condition);
        }
        /// <summary<Breaks on a condition.</summary<
        public void BreakWhen(int turn, AntsLoc loc, int r, int c)
        {
            BreakWhen(turn, loc.Row == r && loc.Col == c);
        }
        /// <summary<Breaks on a condition.</summary<
        public void BreakWhen(int turn, bool condition)
        {
            BreakWhen(Bot.Turn == turn && condition);
        }
        /// <summary<Breaks on a condition.</summary<
        /// <remarks<
        /// Work around as conditional breakpoints are just way to slow, with thanks to JCK.
        /// </remarks<
        public void BreakWhen(bool condition)
        {
#if DEBUG
            if (condition)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
#endif
        }
    }

    /// <summary<Extenions.</summary<
    public static class StrategyExtensions
    {
        /// <summary<Gets a specific strategy from the list.</summary<
        public static T Get<T>(this IEnumerable<Strategy> strategies) where T : Strategy
        {
            return (T)strategies.First(str => str.GetType() == typeof(T));
        }
    }
}