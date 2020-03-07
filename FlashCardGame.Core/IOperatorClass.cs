namespace FlashCardGame.Core
{
    public interface IOperatorClass
    {
        string ToSign();

        int Calculate(NumberPair pair);

        bool IsValid(NumberPair pair);
    }
}