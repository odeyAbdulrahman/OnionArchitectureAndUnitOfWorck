namespace OA.Base.Helpers.GenerateRandoms
{
    public interface IGenerateRandom
    {
        /// <summary>
        /// Method generate random code
        /// </summary>
        /// <returns></returns>
        string RandomNumDigit(int num);
        /// <summary>
        ///  Method generate random code from 4digit
        /// </summary>
        /// <returns></returns>
        string Random4Digit();
    }
}