namespace FlashCardGame.Core
{
    public interface IArithmeticOp
    {
        Operator Name { get; }

        double Calculate(NumberPair pair);

        double Divide(double numerator, double denominator);

        bool IsValid(NumberPair pair);
    }
}