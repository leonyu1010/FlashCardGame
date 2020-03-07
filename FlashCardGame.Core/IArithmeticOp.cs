namespace FlashCardGame.Core
{
    public interface IArithmeticOp
    {
        Operator Name { get; }

        string ToSign();

        int Calculate(NumberPair pair);

        double Divide(double numerator, double denominator);

        bool IsValid(NumberPair pair);
    }
}