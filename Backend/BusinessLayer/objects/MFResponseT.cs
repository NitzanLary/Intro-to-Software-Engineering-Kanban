namespace IntroSE.Kanban.Backend.BusinessLayer
{
    ///<summary>This class extends <c>Response</c> and represents the result of a call to a non-void function. 
    ///In addition to the behavior of <c>Response</c>, the class holds the value of the returned value in the variable <c>Value</c>.</summary>
    ///<typeparam name="T">The type of the returned value of the function, stored by the list.</typeparam>
    public class MFResponse<T> : MFResponse
    {
        public readonly T Value;
        private MFResponse(T value, string msg) : base(msg)
        {
            this.Value = value;
        }

        internal static MFResponse<T> FromValue(T value)
        {
            return new MFResponse<T>(value, null);
        }

        internal static MFResponse<T> FromError(string msg)
        {
            return new MFResponse<T>(default(T), msg);
        }
    }
}

