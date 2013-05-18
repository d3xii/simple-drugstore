namespace SDM.Localization.Core
{
    // ReSharper disable UnusedTypeParameter

    /// <summary>
    /// Defines a contract for a localizable class.
    /// The inherited class does not to implement anything.
    /// </summary>
    public interface ILocalizable<TLocalizationScope> : ILocalizable
        where TLocalizationScope : ILocalizationScope
    {
    }

    // ReSharper restore UnusedTypeParameter

    /// <summary>
    /// Defines a contract for a localizable class.
    /// The inherited class does not to implement anything.
    /// </summary>
    public interface  ILocalizable
    {
        
    }
}