namespace PebbleCode.Interview.Battleships
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class LegalTargets : IEnumerable<ITarget>
    {
        private readonly HashSet<Square> targets;

        public LegalTargets(HashSet<Square> targets)
        {
            this.targets = targets;
        }

        public MoveResult Hit(ITarget target)
        {
            var matchingSquare = targets.SingleOrDefault(square => square == target);

            if (matchingSquare == null)
            {
                return MoveResult.IllegalMove;
            }

            targets.Remove(matchingSquare);

            return matchingSquare.Hit();
        }

        public override string ToString()
        {
            return targets.Aggregate(string.Empty, (curr, next) => curr + next.Coordinates + ";");
        }

        public IEnumerator<ITarget> GetEnumerator()
        {
            return targets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}