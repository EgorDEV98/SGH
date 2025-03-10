namespace SGH.Data.Interfaces;

/// <summary>
/// Интерфейс с признаком мягкого удаления
/// </summary>
public interface ISoftDeleteEntity
{
    /// <summary>
    /// Признак мягкого удаления
    /// </summary>
    public bool IsDeleted { get; set; }
        
    /// <summary>
    /// Дата удаления
    /// </summary>
    public DateTime DeleteDate { get; set; }
}