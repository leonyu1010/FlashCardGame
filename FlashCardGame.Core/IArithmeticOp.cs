namespace FlashCardGame.Core
{
    public interface IArithmeticOp
    {
        string ToSign();

        int Calculate(NumberPair pair);

        double Divide(double numerator, double denominator);

        bool IsValid(NumberPair pair);
    }
}